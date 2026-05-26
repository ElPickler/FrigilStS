using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class Gemsense() : PicklerFrigilCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromKeyword(GemstoneKeyword);
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Gems", 2),
        new ("Exhaust", 3)
    ];
    
    //public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 0, DynamicVars["Exhaust"].IntValue);
        List<CardModel> select = (await CardSelectCmd.FromCombatPile(choiceContext, PileType.Draw.GetPile(Owner), Owner, prefs, Filter)).ToList();
        foreach (CardModel gem in select)
        {
            await CardCmd.Exhaust(choiceContext, gem);
        }
        
        bool Filter(CardModel c)
        {
            return c.Tags.Contains(GemTag);
        }
        
        await GemstoneCmd.GenerateGemstone(Owner, (int) DynamicVars["Gems"].BaseValue, PileType.Draw);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Exhaust"].UpgradeValueBy(1);
    }
}