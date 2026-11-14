using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Singletons;

public class SaltRockSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override bool ShouldReceiveCombatHooks => true;

    public override async Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.PlayerCombatState!.TurnNumber > 1)
            return;
        GemstoneCmd.GemstoneField.CreatedSaltThisCombat.Set(player.PlayerCombatState, false);
    }
}