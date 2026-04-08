using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Basic;

public class DeepChill() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<WeakPower>( 2M),
        new PowerVar<HypothermiaPower>( 1M)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower < WeakPower>();
            yield return HoverTipFactory.FromPower < HypothermiaPower>();
        }
    }
    
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (play.Target == null)
            return;
        await PowerCmd.Apply<WeakPower>(play.Target, DynamicVars.Weak.BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<HypothermiaPower>(play.Target, DynamicVars["HypothermiaPower"].BaseValue , Owner.Creature, this);
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Weak.UpgradeValueBy(1M);
        
    }
}