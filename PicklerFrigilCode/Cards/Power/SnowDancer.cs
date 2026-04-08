using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Power;


public class SnowDancer() : PicklerFrigilCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<SnowDancerPower>(3M)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<SnowDancerPower>(Owner.Creature, DynamicVars["SnowDancerPower"].BaseValue , Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["SnowDancerPower"].UpgradeValueBy(1);
    }
}