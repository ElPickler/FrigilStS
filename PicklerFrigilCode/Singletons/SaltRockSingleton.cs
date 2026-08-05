using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Singletons;

public class SaltRockSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override bool ShouldReceiveCombatHooks => true;
    
    /*
    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return Task.CompletedTask;

        
        //GemstoneCmd.GemstoneField.CreatedSaltThisCombat.Set();
        return Task.CompletedTask;
    }*/

    public override async Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.PlayerCombatState!.TurnNumber > 1)
            return;
        GemstoneCmd.GemstoneField.CreatedSaltThisCombat.Set(player.PlayerCombatState, false);
    }
}