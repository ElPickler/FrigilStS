using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Bite() : PicklerFrigilCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CardModel card = (await CardSelectCmd.FromHand(choiceContext, Owner, new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1), null, this)).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}