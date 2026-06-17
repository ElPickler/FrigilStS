using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class MetabolizingQuartzPower : PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath =>
        "res://PicklerFrigil/images/powers/picklerfrigil-metabolizing_quartz_power.png";
    public override string CustomBigIconPath =>
        "res://PicklerFrigil/images/powers/big/picklerfrigil-metabolizing_quartz_power.png";

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card,
        bool causedByEthereal)
    {
        if (card.Owner.Creature != Owner) 
            return;
        if (!card.Tags.Contains(PicklerFrigilCard.GemTag)) 
            return;
        await AccumulateCmd.Accumulate(choiceContext, Amount, Owner.Player!, this);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player)
            return;
        await PowerCmd.Decrement(this);
        
    }
}