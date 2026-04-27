using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using PicklerFrigil.PicklerFrigilCode.Cards;

namespace PicklerFrigil.PicklerFrigilCode.Cards.Skill;

public class Stumble() : PicklerFrigilCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{ 
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VulnerablePower>(1)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<VulnerablePower>(choiceContext, Owner.Creature, DynamicVars["VulnerablePower"].BaseValue,
            Owner.Creature, this);
        
        List<CardModel> list = GetGemstones(Owner).ToList();
        foreach (CardModel card in list)
            await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
    
    private static IEnumerable<CardModel> GetGemstones(Player owner)
    {
        return owner.PlayerCombatState!.AllCards.Where(c => c.Tags.Contains(GemTag) && c.Pile.Type != PileType.Exhaust);
    }
}