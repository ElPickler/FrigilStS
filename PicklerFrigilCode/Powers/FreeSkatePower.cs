using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class FreeSkatePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string? CustomPackedIconPath => "res://PicklerFrigil/images/powers/freeskate.png";
    public override string? CustomBigIconPath => "res://PicklerFrigil/images/powers/big/freeskate.png";
    
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if(cardPlay.Card.Owner.Creature != Owner) //Check if owner played the card
            return;
        cardPlay.Card.EnergyCost.AddThisTurn(Amount);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        await PowerCmd.Remove(this);
    }
}