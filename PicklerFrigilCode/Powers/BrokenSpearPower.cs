using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;

namespace PicklerFrigil.PicklerFrigilCode.Powers;


public class BrokenSpearPower: PicklerFrigilPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "res://PicklerFrigil/images/powers/brokenspear.png";
    public override string CustomBigIconPath => "res://PicklerFrigil/images/powers/big/brokenspear.png";

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (creator != Owner.Player)
            return;
        if (card is not Cryospear)
            return;
        
        card.EnergyCost.SetCustomBaseCost(1);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card is not Cryospear)
            return;
        if (cardPlay.Card is ShatteredSpear)
            return;
        if (cardPlay.Card.Owner != Owner.Player)
            return;
        
        Cryospear? cryospear = cardPlay.Card as Cryospear;
        decimal damage = cryospear?.GetDamage() ?? 0;
        
        List<CardModel> spears = new List<CardModel>();
        for (int i = 0; i < Amount; i++)
        {
            ShatteredSpear spear = CombatState.CreateCard<ShatteredSpear>(Owner.Player!);
            spear.setDamage(damage);
            spears.Add(spear);
        }

        await CardPileCmd.AddGeneratedCardsToCombat(spears, PileType.Hand, Owner.Player);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay)
    {
        if (dealer != Owner)
            return 1;
        if (cardSource is not Cryospear)
            return 1;
        return 0.5M;
    }
}