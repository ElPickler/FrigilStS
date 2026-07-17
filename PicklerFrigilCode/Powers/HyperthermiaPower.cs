using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class HyperthermiaPower : PicklerFrigilPower
{
    public int _hypothermiaLoss = 1;
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hyperthermia_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hyperthermia_power.png";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("HypothermiaLoss", 0)
    ];
    
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
        
        return 0 - DynamicVars["HypothermiaLoss"].BaseValue;
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power != this)
            return;
        
        IncrementHypothermiaLoss();
    }

    private void IncrementHypothermiaLoss()
    {
        DynamicVars["HypothermiaLoss"].BaseValue += 1;
    }
}