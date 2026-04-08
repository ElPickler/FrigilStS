using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class SnowDancerPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-snow_dancer_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-snow_dancer_power.png";
}