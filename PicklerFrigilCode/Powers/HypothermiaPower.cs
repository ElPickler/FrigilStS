using System.Globalization;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class HypothermiaPower : PicklerFrigilPower, IHasSecondAmount
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hypothermia_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hypothermia_power.png";

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("EffDamage", 0) //Used for localization. Was more important when the damage calc was more complex, just haven't bothered to remove it.
    ];

    private double _hypoReduction;
    
    public string GetSecondAmount() => (0 - (int)_hypoReduction).ToString(CultureInfo.CurrentCulture);

    //Main functionality
   public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource,
       CardPlay? cardPlay)
   {
       if (cardSource == null)
           return 0;
       if (target != Owner)
           return 0;
       if (!cardSource.Tags.Contains(PicklerFrigilCard.IcyTag))
           return 0;
       return Amount;
   }


   public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this && applier != null)
        {
            DynamicVars["EffDamage"].BaseValue = Amount;

            await CalculateReduction();
            this.InvokeSecondAmountChanged();
            
            if (amount < 0) //Powers should not apply if hypothermia is being removed
                return;
            
            //Snow Dancer functionality
            decimal snowDancer = applier.GetPowerAmount<SnowDancerPower>();
            if (snowDancer != 0)
            {
                await CreatureCmd.GainBlock(applier, snowDancer, ValueProp.Unpowered, null);
            }
            
            //Draconic Form functionality
            if (applier.HasPower<DraconicFormPower>()) 
            {
                decimal draconicFormAmount = applier.GetPowerAmount<DraconicFormPower>();
                await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), applier.CombatState!.HittableEnemies, amount * draconicFormAmount, ValueProp.Unpowered, Owner);
            }
        }
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player)
            return;
        
        await PowerCmd.ModifyAmount(choiceContext, this, (int)(0 - _hypoReduction), Owner, null);
        
    }

    private Task CalculateReduction()
    {
        _hypoReduction = Amount * 0.33;
        
        if (_hypoReduction < 1)
            _hypoReduction = 1;
        
        return Task.CompletedTask;
    }
}