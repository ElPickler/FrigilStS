using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Power;


public class InMotion() : PicklerFrigilCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<InMotionPower>(3M)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<InMotionPower>(choiceContext, Owner.Creature, DynamicVars["InMotionPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["InMotionPower"].UpgradeValueBy(1);
    }
}