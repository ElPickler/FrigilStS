using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class EndothermiaPower: PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}