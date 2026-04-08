using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards.Gems;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;

namespace PicklerFrigil.PicklerFrigilCode.Commands;

public class GemstoneCmd
{
    public static async Task GenerateGemstone(
        Player player, int count)
    {
        IEnumerable<CardModel> c = CardFactory.GetDistinctForCombat(player, gems, count, player.RunState.Rng.Shuffle);
        
        await CardPileCmd.AddGeneratedCardsToCombat(c, PileType.Hand, true);
    }
    
    public static AbstractGem[] gems =
    {
        ModelDb.Card<Topaz>(),
        ModelDb.Card<Amethyst>(),
        ModelDb.Card<Diamond>(),
        ModelDb.Card<Opal>(),
        ModelDb.Card<Emerald>()
    };
}