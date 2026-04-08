using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class CheckPocketsPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-check_pockets_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-check_pockets_power.png";

    public override async Task AfterDeath(
        PlayerChoiceContext choiceContext,
        Creature target,
        bool wasRemovalPrevented,
        float deathAnimLength)
    {
        if (target.Side == Owner.Side)
            return;
        await GemstoneCmd.GenerateGemstone(Owner.Player, Amount);
    }
}