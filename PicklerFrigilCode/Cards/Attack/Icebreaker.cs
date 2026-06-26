using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class Icebreaker() : PicklerFrigilCard(0,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    private const int BaseMult = 2;
    private const int UpgMult = 3;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromPower<HypothermiaPower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0M),
        new ExtraDamageVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, target) => IcebreakerDamage(card, target))
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);

        if (play.Target != null) await PowerCmd.Remove<HypothermiaPower>(play.Target);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Repeat"].UpgradeValueBy(1);
    }

    private static decimal IcebreakerDamage(CardModel card, Creature creature)
    {
        if (creature == null)
            return 0;
        decimal hypoAmount = creature.GetPowerAmount<HypothermiaPower>();
        if (card.IsUpgraded)
            return hypoAmount * UpgMult;
        return hypoAmount * BaseMult;
    }
}