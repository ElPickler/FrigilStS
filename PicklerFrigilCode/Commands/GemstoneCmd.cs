using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
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
    public static async Task GenerateGemstone(
        Player player, int count, PileType pileType = PileType.Hand, CardPreviewStyle previewStyle = CardPreviewStyle.HorizontalLayout)
    {
        int moddedCount = count;
        
        
        if (player.Creature.HasPower<RubyBloodPower>())
        {
            moddedCount++;
            player.Creature.GetPower<RubyBloodPower>()!.InvokeFlash();
        }

        CrystallizedSalt? salt = player.GetRelic<CrystallizedSalt>();
        if (salt == null) 
        {
            int rng = player.RunState.Rng.Niche.NextInt(0, 50); // 2% chance to get salt IF you don't already have it
            if (rng == 0)
            {
                moddedCount--;
                await RelicCmd.Obtain<CrystallizedSalt>(player);
                player.GetRelic<CrystallizedSalt>()!.InvokeFlash();
            }
        }
        
        
        IEnumerable<CardModel> c = CardFactory.GetDistinctForCombat(player, Gems, moddedCount, player.RunState.Rng.Shuffle);
        
        if(pileType == PileType.Draw)
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(c, pileType, player, CardPilePosition.Random), 0.4F, previewStyle);
        else
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(c, pileType, player), 0.4F, previewStyle);
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