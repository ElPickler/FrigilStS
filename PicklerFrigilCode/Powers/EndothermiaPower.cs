using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class EndothermiaPower: PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-endothermia_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-endothermia_power.png";
}