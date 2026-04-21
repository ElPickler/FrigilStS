using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using PicklerFrigil.PicklerFrigilCode.Commands;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class DeepPierce() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Accumulate", 5M),
        new PowerVar<DeepPiercePower>(1M)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await AccumulateCmd.Accumulate(DynamicVars["Accumulate"].BaseValue, Owner, this);
        if (Owner.Creature.GetPowerAmount<DeepPiercePower>() == 0)
        {
            await PowerCmd.Apply<DeepPiercePower>(Owner.Creature, DynamicVars["DeepPiercePower"].BaseValue + 1,
                Owner.Creature, this);
        }
        else
        {
            await PowerCmd.Apply<DeepPiercePower>(Owner.Creature, DynamicVars["DeepPiercePower"].BaseValue,
                Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Accumulate"].UpgradeValueBy(3);
    }
}