using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Attack;


public class SlideKick() : PicklerFrigilCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get { 
            yield return HoverTipFactory.FromPower<FlowPower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(9M),
        new ExtraDamageVar(2M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier(static (card, target) => GetFlow(card))
    ];
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }

    private static decimal GetFlow(CardModel card)
    {
        if (card.Owner == null)
            return 0;
        return card.Owner.Creature.GetPowerAmount<FlowPower>();
    }
}