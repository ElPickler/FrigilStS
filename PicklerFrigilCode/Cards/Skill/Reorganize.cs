using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Reorganize() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> cards = new List<CardModel>();

        foreach (CardModel card in PileType.Draw.GetPile(Owner).Cards)
        {
            if (card.Type == CardType.Status)
                cards = cards.Append(card);
        }

        foreach (CardModel card in cards)
            await CardPileCmd.Add(card, PileType.Discard);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}