using System.Buffers;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Redirect() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    private static int flowPower = 5;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Multiplier", 1),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((Func<CardModel, Creature, Decimal>) ((card, _) => GetFlowAmount(card)))
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(play.Target), DynamicVars.CalculatedBlock.Props, play);
    }

    
    protected override void OnUpgrade()
    {

    }

    private static decimal GetFlowAmount(CardModel card)
    {
        return card.Owner.Creature.GetPowerAmount<FlowPower>();
    }
    
}