using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class HyperthermiaPower : PicklerFrigilPower
{
    private const int HypothermiaLoss = 2;
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hyperthermia_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hyperthermia_power.png";
    
    public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != Owner.Player ? amount : amount + Amount;
    }

    public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource)
    {
        if(power is not HypothermiaPower)
            return base.ModifyPowerAmountGivenAdditive(power, giver, amount, target, cardSource);
        if(giver != Owner)
            return base.ModifyPowerAmountGivenAdditive(power, giver, amount, target, cardSource);
        if(cardSource is null)
            return base.ModifyPowerAmountGivenAdditive(power, giver, amount, target, cardSource);
        
        Flash();
        return base.ModifyPowerAmountGivenAdditive(power, giver, amount - HypothermiaLoss, target, cardSource);
    }
    
}