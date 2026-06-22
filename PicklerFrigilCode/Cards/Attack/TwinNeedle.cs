using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Basic;


public class TwinNeedle() : PicklerFrigilCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromKeyword(IcyKeyword);
            yield return HoverTipFactory.FromPower<HypothermiaPower>();
        }
    }
    
    protected override HashSet<CardTag> CanonicalTags => [IcyTag];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4, ValueProp.Move),
        new ("Repeat", 2M),
        new PowerVar<HypothermiaPower>(2)
    ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).WithHitCount(DynamicVars["Repeat"].IntValue).Execute(choiceContext);
        
        //for (int i = 0; i < DynamicVars["Repeat"].BaseValue; i++)
        //{
        if(play.Target != null)
            await PowerCmd.Apply<HypothermiaPower>(choiceContext, play.Target, DynamicVars["HypothermiaPower"].BaseValue, Owner.Creature, this);
        //}
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}