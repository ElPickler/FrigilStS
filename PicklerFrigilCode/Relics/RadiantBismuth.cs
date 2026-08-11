using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Relics;


[Pool(typeof(PicklerFrigilRelicPool))]
public class RadiantBismuth : PicklerFrigilRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower < HypothermiaPower>();
        }
    }
    
    public override decimal ModifyPowerAmountGivenMultiplicative(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource) //One function to multiply the power amount when calculating the hypothermia amount
    {

        if (target == null)
            return 1;
        if (giver != Owner.Creature)
            return 1;
        if (power is not HypothermiaPower)
            return 1;
        if (target.HasPower<HypothermiaPower>())
            return 1;
        
        return 2;
    }
}