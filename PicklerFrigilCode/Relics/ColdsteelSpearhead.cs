using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rooms;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class ColdsteelSpearhead() : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;


    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;

        Status = RelicStatus.Active;
        return Task.CompletedTask;
    }

    public void InvokeFlash()
    {
        Flash();
    }
}