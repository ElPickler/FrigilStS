using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class MetabolizingDiamondPower : PicklerFrigilPower 
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-metabolizing_diamond_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-metabolizing_diamond_power.png";
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {

        if (side == CombatSide.Enemy)
            return;
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
        // ReSharper disable once PossibleLossOfFraction
        await PowerCmd.ModifyAmount(choiceContext, this, -(Amount - Amount/2), Owner, null);
    }


}