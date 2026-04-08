using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace PicklerFrigil.PicklerFrigilCode.Powers;

 
public class HoardingInstinctsPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-hoarding_instincts_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-hoarding_instincts_power.png";
    
    public override Task AfterCombatEnd(CombatRoom room)
    {
        room.AddExtraReward(Owner.Player, new GoldReward(Amount, Owner.Player));
        return Task.CompletedTask;
    }
}