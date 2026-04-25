using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Pirouette() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<FlowPower>(3),
        new CardsVar(0)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardCmd.DiscardAndDraw(choiceContext, PileType.Hand.GetPile(Owner).Cards.ToList(), (int) DynamicVars.Cards.BaseValue + Owner.Creature.GetPowerAmount<FlowPower>() + (int) DynamicVars["FlowPower"].BaseValue);
    }
    
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card == this)
        {
            await PowerCmd.Apply<FlowPower>(context, Owner.Creature, DynamicVars["FlowPower"].BaseValue, Owner.Creature, this, false);
        } 
    }

    protected override void OnUpgrade()
    {
        DynamicVars["FlowPower"].UpgradeValueBy(1);
    }
}