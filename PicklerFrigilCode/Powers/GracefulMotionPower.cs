using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class GracefulMotionPower: PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource)
    {
        if(power is FlowPower && target == Owner)
            return base.ModifyPowerAmountGiven(power, giver, amount + Amount, target, cardSource);
        
        return base.ModifyPowerAmountGiven(power, giver, amount, target, cardSource);
    }
}