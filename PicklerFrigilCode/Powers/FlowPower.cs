using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class FlowPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-flow_power.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-flow_power.png";
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.Static(StaticHoverTip.Block);
        }
    }
    
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
        if(!props.IsPoweredCardOrMonsterMoveBlock_())
            return 0M;
        return Amount;
        
        
        //return !props.IsPoweredCardOrMonsterMoveBlock() ? 0M : Amount;
        //return Amount;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if(cardPlay.Card.Owner.Creature != Owner) //Check if owner played the card
            return;
        if (cardPlay.Card.Type != CardType.Skill) //check if card is skill
            return;
        await Cmd.Wait(0.1f);
        if (cardPlay.Card.Owner.HasPower<InMotionPower>()) //Check if owner has In Motion
        {
            PowerModel inMotion = cardPlay.Card.Owner.Creature.GetPower<InMotionPower>();
            PowerCmd.Decrement(inMotion);
            return;
        }
        await PowerCmd.Remove(this);
    }

    
}