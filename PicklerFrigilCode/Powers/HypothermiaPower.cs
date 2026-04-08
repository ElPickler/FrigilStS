using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class HypothermiaPower : CustomPowerModel
{
    //private decimal prevAmount;
    
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hypothermia_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hypothermia_power.png";

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (cardSource == null)
            return 0;
        if (target != Owner)
            return 0;
        if (!cardSource.Tags.Contains(PicklerFrigilCard.IcyTag))
            return 0;
        return Amount;

    }

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this) 
        {
            decimal SnowDancer = applier.GetPowerAmount<SnowDancerPower>();
            if (SnowDancer != 0)
            {
                await CreatureCmd.GainBlock(applier, SnowDancer, ValueProp.Unpowered, null);
            }
        }
    }
    
    /*
     Old version of Snow Dancer
    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this)
        {
            if (applier.GetPowerAmount<SnowDancerPower>() != 0)
            {
                await CreatureCmd.GainBlock(applier, (amount - prevAmount), ValueProp.Unpowered, null);
            }
        }
    }

    public override Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource) 
    {
        prevAmount = target.GetPowerAmount<HypothermiaPower>();
        return base.BeforeApplied(target, amount, applier, cardSource);
    }
    */
    
    //FIXME: reduce hupothermia at the end of every turn by 50% or something
}