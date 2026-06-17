using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class Snowglobe : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    private bool _usedThisCombat = false;
    
    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;
        _usedThisCombat = false;
        Status = RelicStatus.Active;
        return Task.CompletedTask;
    }

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        if (_usedThisCombat)
            return;
        if (creature != Owner.Creature)
            return;
        if (!props.IsPoweredCardOrMonsterMoveBlock())
            return;
        
        foreach (Creature enemy in Owner.Creature.CombatState!.HittableEnemies)
        {
            await PowerCmd.Apply<HypothermiaPower>(new BlockingPlayerChoiceContext(), enemy, amount, Owner.Creature, null, false);
        }

        _usedThisCombat = true;
        Status = RelicStatus.Disabled;
        Flash();
    }
}