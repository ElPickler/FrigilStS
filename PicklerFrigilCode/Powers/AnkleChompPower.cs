using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class AnkleChompPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/anklechomp.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/anklechomp.png";
    
    public override Decimal ModifyBlockMultiplicative(
        Creature target,
        Decimal block,
        ValueProp props,
        CardModel? cardSource,
        CardPlay? cardPlay)
    {
        if(target == Owner)
            return  0.33M;
        return 1;
    }
    
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Enemy)
            return;
        await PowerCmd.TickDownDuration(this);
    }
}