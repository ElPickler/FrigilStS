
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using PicklerFrigil.PicklerFrigilCode.Cards.Skill;

namespace PicklerFrigil.PicklerFrigilCode.Powers;

public class SlipUnderPower: TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<SlipUnder>();
    protected override bool IsPositive => false;
    
    //public override PowerType Type => PowerType.Debuff;
    //public override PowerStackType StackType => PowerStackType.Counter;
    
    //public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-sleet_storm_power.png";
    //public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-sleet_storm_power.png";
}
