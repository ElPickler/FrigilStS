using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class GracefulMotionPower: PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-graceful_motion_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-graceful_motion_power.png";

    public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource)
    {
        if (power is FlowPower && target == Owner)
        {
            Flash();
            return Amount;
        }
        
        return 0;
    }
}