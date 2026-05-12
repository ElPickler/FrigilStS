using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Deployment() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllAllies)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
 
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromKeyword(AccumulateKeyword);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Accumulate", 12)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (Player player in CombatState.Players)
            await AccumulateCmd.Accumulate(DynamicVars["Accumulate"].BaseValue, player, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Accumulate"].UpgradeValueBy(3);
    }
}