using System.Buffers;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Redirect() : PicklerFrigilCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    private const int BaseMult = 1;
    private const int UpgradeMult = 2;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Multiplier", 1),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((Func<CardModel, Creature, Decimal>) ((card, _) => GetFlowAmount(card))),
        new PowerVar<FlowPower>(0M),
        new CardsVar(1)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<FlowPower>();
        }
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(play.Target), DynamicVars.CalculatedBlock.Props, play);
        DynamicVars["FlowPower"].BaseValue = Owner.Creature.GetPowerAmount<FlowPower>();
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, Owner);
    }

    
    protected override void OnUpgrade()
    {
        //Upgrade effect is baked into GetFlowAmount
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card == this)
            await PowerCmd.Apply<FlowPower>(context, Owner.Creature, DynamicVars["FlowPower"].BaseValue, Owner.Creature, this);
    }
    
    private static decimal GetFlowAmount(CardModel card)
    {
        int Flow = card.Owner.Creature.GetPowerAmount<FlowPower>();
        if (card.IsUpgraded)
            Flow *= UpgradeMult;
        else
            Flow *= BaseMult;
        
        return Flow;
    }
}