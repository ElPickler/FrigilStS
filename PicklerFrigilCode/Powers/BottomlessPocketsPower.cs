using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Powers;



public class BottomlessPocketsPower: PicklerFrigilPower
{
    public class BottomlessPocketsField
    {
        public static readonly SpireField<CardModel, bool> CreatedByBottomless = new(() => false);
    }
    
    public int CardCounter = 0;

    protected virtual int CardTrigger
    {
        get => 4;
    }
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override int DisplayAmount => CardCounter;

    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/big/deeppockets.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/deeppockets.png";

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        CardCounter = CardTrigger;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (creator != Owner.Player)
            return;
        if (BottomlessPocketsField.CreatedByBottomless.Get(card))
            return;
        
        
        CardCounter--;
        InvokeDisplayAmountChanged();
        
        if (CardCounter > 0)
            return;
        
        CardCounter = CardTrigger;
        
        CardModel newCardBase = card.CreateClone();
        newCardBase.AddKeyword(CardKeyword.Ethereal);
        BottomlessPocketsField.CreatedByBottomless.Set(newCardBase, true);
        
        await CardPileCmd.AddGeneratedCardToCombat(newCardBase, PileType.Hand, Owner.Player);
        
        InvokeDisplayAmountChanged();
        Flash();
    }
}