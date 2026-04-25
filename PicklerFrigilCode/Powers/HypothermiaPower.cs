using BaseLib.Abstracts;
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


public class HypothermiaPower : CustomPowerModel
{
    //private decimal prevAmount;
    private decimal applied;
    
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hypothermia_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hypothermia_power.png";

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("EffDamage", 0)
    ];
    
    //Main functionality
    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (cardSource == null)
            return 0;
        if (target != Owner)
            return 0;
        if (!cardSource.Tags.Contains(PicklerFrigilCard.IcyTag))
            return 0;
        return Math.Ceiling(Amount / 2M);

    }


    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this && applier != null)
        {
            //Snow Dancer functionality
            DynamicVars["EffDamage"].BaseValue = Math.Ceiling(Amount / 2M);
            decimal snowDancer = applier.GetPowerAmount<SnowDancerPower>();
            if (snowDancer != 0)
            {
                await CreatureCmd.GainBlock(applier, snowDancer, ValueProp.Unpowered, null);
            }
            //Draconic Form functionality
            if (applier.HasPower<DraconicFormPower>()) 
            {
                decimal draconicFormAmount = applier.GetPowerAmount<DraconicFormPower>();
                await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), applier.CombatState.HittableEnemies, amount * draconicFormAmount, ValueProp.Unpowered, Owner);
            }
        }
    }
    
}