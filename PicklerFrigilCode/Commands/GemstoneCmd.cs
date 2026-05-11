using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards.Gems;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Commands;

public class GemstoneCmd
{
    public static async Task GenerateGemstone(
        Player player, int count, PileType pileType = PileType.Hand)
    {
        int moddedCount = count;

        
        
        if (player.Creature.HasPower<RubyBloodPower>())
        {
            moddedCount++;
            player.Creature.GetPower<RubyBloodPower>()!.InvokeFlash();
        }
            
            
        
        IEnumerable<CardModel> c = CardFactory.GetDistinctForCombat(player, Gems, moddedCount, player.RunState.Rng.Shuffle);
        
        if(pileType == PileType.Draw)
            await CardPileCmd.AddGeneratedCardsToCombat(c, pileType, player, CardPilePosition.Random);
        else
            await CardPileCmd.AddGeneratedCardsToCombat(c, pileType, player);
    }

    private static readonly AbstractGem[] Gems =
    {
        ModelDb.Card<Topaz>(),
        ModelDb.Card<Amethyst>(),
        ModelDb.Card<Diamond>(),
        ModelDb.Card<Opal>(),
        ModelDb.Card<Emerald>(),
        ModelDb.Card<Quartz>()
    };
}