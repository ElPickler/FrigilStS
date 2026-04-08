using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards.Attack;

namespace PicklerFrigil.PicklerFrigilCode.Powers;





public class DescendingSpiralPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-descending_spiral_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-descending_spiral_power.png";
    
    public override Decimal ModifyBlockAdditive(
        Creature target,
        Decimal block,
        ValueProp props,
        CardModel? cardSource,
        CardPlay? cardPlay)
    {
        if (cardSource != null)
        {
            if (cardSource.Owner.Creature != Owner)
                return 0M;
        }
        else if (Owner != target)
            return 0M;
        //return !props.IsPoweredCardOrMonsterMoveBlock() ? 0M : Amount;
        return Amount;
    }

    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        CombatState combatState)
    {
        if (side == CombatSide.Player)
        {
            CardModel c = combatState.CreateCard<DescendingSpiral>(Owner.Player);
            await CardPileCmd.AddGeneratedCardToCombat(c, PileType.Hand, true);
            await PowerCmd.Remove(this);
        }
    }

    
}