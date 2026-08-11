using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class FreeSkatePower : PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/freeskate.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/freeskate.png";
    
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if(cardPlay.Card.Owner.Creature != Owner) //Check if owner played the card
            return;
        cardPlay.Card.EnergyCost.AddThisTurn(1);
    }

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (card.Owner != Owner.Player)
            return;
        if (!card.EnergyCost.CostsX)
        {
            if (card.EnergyCost.Canonical < Amount)
                card.EnergyCost.SetThisTurn(0);
            else
                card.EnergyCost.AddThisTurn(Amount * -1);
        }
    }
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        await PowerCmd.Remove(this);
    }
}