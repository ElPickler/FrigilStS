using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class FrazilPower: PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/frazil.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/frazil.png";



    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player)
            return;
        await PowerCmd.Remove(this);
    }

    public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target != Owner || dealer == null)
            return;
        if (!props.IsPoweredAttack())
            return;
        Flash();

        decimal hypothermiaMod = dealer.GetPowerAmount<HypothermiaPower>();
        await CreatureCmd.Damage(choiceContext, dealer,  Amount + hypothermiaMod, ValueProp.Unpowered | ValueProp.SkipHurtAnim, Owner, null);
    }
}