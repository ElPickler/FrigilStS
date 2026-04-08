using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Relics;

[Pool(typeof(PicklerFrigilRelicPool))]
public class GlisteningAmethyst() : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<HypothermiaPower>( 1M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower < HypothermiaPower>();
        }
    }

    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        CombatState combatState)
    {
        if (side != Owner.Creature.Side)
            return;
        Flash();
        
        Creature? enemy = Owner.RunState.Rng.CombatTargets.NextItem(combatState.HittableEnemies);
        if (enemy != null) await PowerCmd.Apply<HypothermiaPower>(enemy, DynamicVars["HypothermiaPower"].BaseValue, Owner.Creature, null);
    }
}