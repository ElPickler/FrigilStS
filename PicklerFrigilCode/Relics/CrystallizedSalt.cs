using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class CrystallizedSalt() : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.None;

    private bool _usedThisCombat = false;

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (card.Owner != Owner)
            return;
        if (!card.Tags.Contains(PicklerFrigilCard.GemTag))
            return;
        if (_usedThisCombat)
            return;
        
        await card.AfterCardExhausted(choiceContext, card, false);
        _usedThisCombat = true;
        Status = RelicStatus.Disabled;
    }
    
    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;
        _usedThisCombat = false;
        Status = RelicStatus.Active;
        return Task.CompletedTask;
    }

    public void InvokeFlash()
    {
        Flash();
    }

}