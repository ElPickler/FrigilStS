using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using PicklerFrigil.PicklerFrigilCode.Cards.Gems;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Powers;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Commands;

public class GemstoneCmd
{
    private const int SaltOdds = 50; //1 in X chance of getting salt
    
    public class GemstoneField
    {
        public static readonly SpireField<PlayerCombatState, bool> CreatedSaltThisCombat = new(() => false);
    }
    
    public static async Task GenerateGemstone(
        Player player, int count, PileType pileType = PileType.Hand, CardPreviewStyle previewStyle = CardPreviewStyle.HorizontalLayout)
    {
        int moddedCount = count;
        IEnumerable<CardModel> c = new List<CardModel>();
        
        if (player.Creature.HasPower<RubyBloodPower>())
        {
            moddedCount++;
            player.Creature.GetPower<RubyBloodPower>()!.InvokeFlash();
        }

        
        if (TrySalt(player))
        {
            CardModel salt = player.Creature.CombatState!.CreateCard<SaltRock>(player);
            c = c.Append(salt);
            moddedCount--;
        }
        
        
        IEnumerable<CardModel> generatedGems = new List<CardModel>(CardFactory.GetDistinctForCombat(player, Gems, moddedCount, player.RunState.Rng.CombatCardGeneration));
        foreach (CardModel card in generatedGems)
            c = c.Append(card);
        
        
        if(pileType == PileType.Draw)
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(c, pileType, player, CardPilePosition.Random), 0.4F, previewStyle);
        else
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(c, pileType, player), 0.4F, previewStyle);
    }

    private static readonly AbstractGem[] Gems =
    {
        ModelDb.Card<Amethyst>(),
        ModelDb.Card<Diamond>(),
        ModelDb.Card<Opal>(),
        ModelDb.Card<Emerald>(),
        ModelDb.Card<Quartz>(),
        ModelDb.Card<Serpentine>(),
        ModelDb.Card<Ruby>()
    };

    private static bool TrySalt(Player player, PileType pileType = PileType.Hand, CardPreviewStyle previewStyle = CardPreviewStyle.HorizontalLayout)
    {
        CrystallizedSalt? salt = player.GetRelic<CrystallizedSalt>();
        if (salt == null) 
        {
            if (GemstoneField.CreatedSaltThisCombat.Get(player.PlayerCombatState!))
            {
                return false;
            }
            
            int rng = player.RunState.Rng.Niche.NextInt(0, SaltOdds - 1);
            if (rng == 0)
            {
                GemstoneField.CreatedSaltThisCombat.Set(player.PlayerCombatState!, true);
                return true;
            }
        }
        
        return false;
    }

    public static IEnumerable<CardModel> GetGemstone(int count, Player player)
    {
        IEnumerable<CardModel> gemstones = new List<CardModel>(CardFactory.GetDistinctForCombat(player, Gems, count, player.RunState.Rng.CombatCardGeneration));
        
        return  gemstones;
    }
}