using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;


public class RavenousHunger() : PicklerFrigilCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override bool CanBeGeneratedInCombat => false;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromKeyword(CardKeyword.Unplayable);
        }
    }
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Heal", 2)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<CardModel> list = PileType.Hand.GetPile(Owner).Cards.ToList();
        foreach (CardModel c in list)
        {
            await CardCmd.Exhaust(choiceContext, c);
            if (c.Keywords.Contains(CardKeyword.Unplayable))
                await CreatureCmd.Heal(Owner.Creature, DynamicVars["Heal"].BaseValue);
        }
            
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}