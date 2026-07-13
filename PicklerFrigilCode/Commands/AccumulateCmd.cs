using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Cards.Special;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Commands;

public static class AccumulateCmd
{
    public static async Task<IEnumerable<Cryospear>> Accumulate(
        PlayerChoiceContext choiceContext,
        Decimal amount,
        Player player,
        AbstractModel? source)
    {
        decimal accumulateAmount = amount;

        //Check for coldsteel spearhead
        ColdsteelSpearhead? spearhead = player.GetRelic<ColdsteelSpearhead>();
        if (spearhead != null)
        {
            if (spearhead.Status == RelicStatus.Active)
            {
                accumulateAmount *= 2;
                spearhead.InvokeFlash();
                spearhead.Status = RelicStatus.Disabled;
            }
        }
        
        //Dump spears on combat end
        if (CombatManager.Instance.IsOverOrEnding)
            return Array.Empty<Cryospear>();
        
            //Accumulate behavior on varying spear counts
        List<Cryospear> spears = GetCryospears(player, false).ToList();
        //For no spears, create one and move on
        if (spears.Count == 0)
        {
            Cryospear spear = player.Creature.CombatState!.CreateCard<Cryospear>(player);
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat((CardModel) spear, PileType.Hand, player);
            spears.Add(spear);
            spear = null!;
        }
        else
        {
            Cryospear spear = spears[0];
            if(spear.Pile!.Type != PileType.Hand)
                await CardPileCmd.Add(spear, PileType.Hand);
            if (spears.Count > 1)
            {
                FrigilMainFile.Logger.Info("Beginning accumulate");
                foreach (Cryospear c in spears)
                {
                    //Skip the first spear to keep it around
                    if (c != spear)
                    {
                        //Add the damage of other spears to the first before exhausting them
                        decimal spearDamage = c.GetDamage();
                        spear.AddDamage(spearDamage);
                        
                        await CardCmd.Exhaust(choiceContext, c);
                    }
                }
            }
        }
        
        IncreaseSpearDamage(accumulateAmount, player);

        return spears;
    }
    
    private static IEnumerable<Cryospear> GetCryospears(
        Player player,
        bool includeExhausted
        )
    {
        return player.PlayerCombatState!.AllCards.Where<CardModel>((Func<CardModel, bool>) (c =>
        {
            if (c.IsDupe)
                return false;
            if (includeExhausted)
                return true;
            if (c is ShatteredSpear)
                return false;
            CardPile? pile = c.Pile;
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

    public static decimal GetSpearDamage(Player player, AbstractModel? source)
    {
        decimal damage = 0;
        List<Cryospear> spears = GetCryospears(player, false).ToList();
        Cryospear mainspear = spears[0];
        foreach(Cryospear spear in spears)
        {
            damage += spear.GetDamage();
        }
        return damage;
    }
}