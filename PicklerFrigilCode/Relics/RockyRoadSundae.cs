using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using PicklerFrigil.PicklerFrigilCode.Character;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class RockyRoadSundae : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;

    private const int _exhaustThreshold = 4;
    private const int _vigorReward = 3;
    private int _exhaustCount = 0;

    public override int DisplayAmount => _exhaustCount;

    public override bool ShowCounter => true;
    
    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;

        _exhaustCount = 0;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (card.Owner != Owner)
            return;
        
        _exhaustCount++;
        if (_exhaustCount == _exhaustThreshold)
        {
            _exhaustCount = 0;
            await PowerCmd.Apply<VigorPower>(choiceContext, Owner.Creature, _vigorReward, Owner.Creature, null);
            Flash();
        }
        InvokeDisplayAmountChanged();
    }
}