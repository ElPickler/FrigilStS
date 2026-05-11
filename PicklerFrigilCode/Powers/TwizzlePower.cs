using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class TwizzlePower : PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/twizzle.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/twizzle.png";
    
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature == Owner)
        {
            await PowerCmd.Apply<FlowPower>(choiceContext, Owner, Amount, Owner, null);
        }
    }
}