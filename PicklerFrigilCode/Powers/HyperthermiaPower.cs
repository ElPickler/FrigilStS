using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class HyperthermiaPower : PicklerFrigilPower
{
    private const int HypothermiaLoss = 1;
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hyperthermia_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hyperthermia_power.png";
    
    public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != Owner.Player ? amount : amount + Amount;
    }

    public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource)
    {
        if (power is not HypothermiaPower)
            return 0;
        if(giver != Owner)
            return 0;
        if(cardSource is null)
            return 0;
        
        return 0 - HypothermiaLoss * Amount;
    }
    
}