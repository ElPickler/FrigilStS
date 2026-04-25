using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class FrazilPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/frazil.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/frazil.png";

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Player)
            return;
        await PowerCmd.ModifyAmount(choiceContext, Owner.GetPower<ThornsPower>(), Amount * -1, Owner, null);
        await PowerCmd.Remove(this);
    }
}