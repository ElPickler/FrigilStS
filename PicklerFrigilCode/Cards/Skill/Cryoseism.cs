using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Cryoseism() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.None)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromKeyword(GemstoneKeyword);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Exhaust", 1),
        new ("Gemstones", 1)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        for (int i = 0; i < DynamicVars["Exhaust"].BaseValue; i++)
        {
        
            IEnumerable<CardModel> cards = new List<CardModel>();

            foreach (CardModel card in PileType.Hand.GetPile(Owner).Cards)
            {
                if (card.Type == CardType.Status)
                    cards = cards.Append(card);
            }
        
            foreach (CardModel card in PileType.Draw.GetPile(Owner).Cards)
            {
                if (card.Type == CardType.Status)
                    cards = cards.Append(card);
            }
        
            foreach (CardModel card in PileType.Discard.GetPile(Owner).Cards)
            {
                if (card.Type == CardType.Status)
                    cards = cards.Append(card);
            }

            CardModel? exhaust = Owner.RunState.Rng.CombatCardSelection.NextItem(cards);
            
            if (exhaust != null)
                await CardCmd.Exhaust(choiceContext, exhaust);
        }
        
        await GemstoneCmd.GenerateGemstone(Owner, DynamicVars["Gemstones"].IntValue, PileType.Discard);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Exhaust"].UpgradeValueBy(1);
        DynamicVars["Gemstones"].UpgradeValueBy(1);
    }
}