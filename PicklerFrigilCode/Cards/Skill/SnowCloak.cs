using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;

public class SnowCloak() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(3, ValueProp.Move),
        new DynamicVar("Repeat", 2M),
        new PowerVar<HypothermiaPower>( 1M)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        for (int i = 0; i < DynamicVars["Repeat"].BaseValue; i++)
        {
            await CommonActions.CardBlock(this, play);
            
            Creature enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
            if(enemy != null) await PowerCmd.Apply<HypothermiaPower>(choiceContext, enemy, DynamicVars["HypothermiaPower"].BaseValue , Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Repeat"].UpgradeValueBy(1);
    }
}