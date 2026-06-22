using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class KipUp() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<FlowPower>();
            yield return HoverTipFactory.FromPower<VulnerablePower>();
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<FlowPower>(3),
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (Owner.Creature.HasPower<VulnerablePower>())
        {
            await PowerCmd.Remove<VulnerablePower>(Owner.Creature);
        }
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card == this)
            await PowerCmd.Apply<FlowPower>(context, Owner.Creature, DynamicVars["FlowPower"].BaseValue, Owner.Creature, this, false);
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars["FlowPower"].UpgradeValueBy(2);
    }
}