using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Powers;

  
public class RepurposePower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-repurpose_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-repurpose_power.png";
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.Static(StaticHoverTip.Block);
        }
    }


    public override async Task AfterTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            await AccumulateCmd.Accumulate(Owner.Block * Amount, Owner.Player, this);
            await PowerCmd.Remove(this);
        }
    }
}