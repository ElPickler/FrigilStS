using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;

namespace PicklerFrigil.PicklerFrigilCode.Commands;

public static class AccumulateCmd
{
    public static async Task<IEnumerable<Cryospear>> Accumulate(
        Decimal amount,
        Player player,
        AbstractModel? source)
    {
        if (CombatManager.Instance.IsOverOrEnding)
            return Array.Empty<Cryospear>();
        List<Cryospear> spears = GetCryospears(player, false).ToList();
        if (spears.Count == 0)
        {
            Cryospear spear = player.Creature.CombatState.CreateCard<Cryospear>(player);
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat((CardModel) spear, PileType.Hand, true);
            spears.Add(spear);
            spear = null;
        }
        else
        {
            Cryospear spear = spears[0];
            await CardPileCmd.Add(spear, PileType.Hand);
            if (spears.Count > 1)
            {
                foreach (Cryospear c in spears)
                {
                    await CardCmd.Exhaust(null, c);
                }
            }
        }
        IncreaseSpearDamage(amount, player);

        return spears;
    }
    
    private static IEnumerable<Cryospear> GetCryospears(
        Player player,
        bool includeExhausted)
    {
        return player.PlayerCombatState.AllCards.Where<CardModel>((Func<CardModel, bool>) (c =>
        {
            if (c.IsDupe)
                return false;
            if (includeExhausted)
                return true;
            CardPile pile = c.Pile;
            return pile == null || pile.Type != PileType.Exhaust;
        })).OfType<Cryospear>();
    }
    
    private static void IncreaseSpearDamage(Decimal amount, Player player)
    {
        List<Cryospear> list = GetCryospears(player, false).ToList<Cryospear>();
        foreach (Cryospear card in list)
        {
            card.AddDamage(amount);
            //card.AfterForged();
            //ForgeCmd.PlayCombatRoomForgeVfx(player, (CardModel) card);
        }
        //ForgeCmd.PreviewSovereignBlade((IReadOnlyCollection<Cryospear>) list);
    }

    public static decimal GetSpearDamage(Player player)
    {
        decimal damage = 0;
        List<Cryospear> spears = GetCryospears(player, false).ToList();
        foreach(Cryospear spear in spears)
        {
            damage += spear.GetDamage();
        }
        return damage;
    }
}