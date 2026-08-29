using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Commands;

namespace PicklerFrigil.PicklerFrigilCode.Potions;


public class BagOfRocks : PicklerFrigilPotion
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;
    
    public override string CustomPackedImagePath => "res://PicklerFrigil/images/potions/bag_of_rocks.png";

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Gems", 3)
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        //await GemstoneCmd.GenerateGemstone(Owner, (int) DynamicVars["Gems"].BaseValue);

        IEnumerable<CardModel> cards = GemstoneCmd.GetGemstone(3, Owner);
        
        CardModel ExhaustGem = (await CardSelectCmd.FromChooseACardScreen(choiceContext, cards.ToList(), Owner))!;

        IEnumerable<CardModel> c = new List<CardModel>();
        foreach (CardModel card in cards)
        {
            if (card != ExhaustGem)
                c = c.Append(card);
            else
                await CardCmd.Exhaust(choiceContext, ExhaustGem);
        }
        
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(c, PileType.Discard, Owner));
    }
}