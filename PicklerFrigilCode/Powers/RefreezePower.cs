using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class RefreezePower : PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/picklerfrigil-refreeze_power.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/picklerfrigil-refreeze_power.png";

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        if (dealer != Owner)
            return;
        if (cardSource == null)
            return;
        if(!cardSource.Tags.Contains(PicklerFrigilCard.IcyTag))
            return;
        if (cardSource.Type != CardType.Attack)
            return;
        
        
        int hypothermia = target.GetPowerAmount<HypothermiaPower>();
        //await AccumulateCmd.Accumulate(hypothermia * Amount, Owner.Player!, this);
        await PowerCmd.Apply<AccumulateNextTurnPower>(choiceContext, Owner, hypothermia * Amount, Owner, null);
    }
}