using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class MetabolizingEmeraldPower : PicklerFrigilPower 
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-metabolizing_emerald_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-metabolizing_emerald_power.png";
    
    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        return player != Owner.Player || AmountOnTurnStart == 0 ? count : count + Amount;
    }

    public override async Task AfterSideTurnStart(CombatSide side, ICombatState combatState)
    {
        if (side != Owner.Side || AmountOnTurnStart == 0)
            return;
        await PowerCmd.Decrement(this);
    }
}