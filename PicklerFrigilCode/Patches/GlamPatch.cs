using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode.Character;

namespace PicklerFrigil.PicklerFrigilCode.Patches;

[HarmonyPatch(typeof(EnchantmentModel), methodName: "CanEnchant")]
public class GlamPatch
{
    [HarmonyPostfix]
    public static void Postfix(EnchantmentModel __instance, ref bool __result, CardModel card)
    {
        if (!card.Keywords.Contains(CardKeyword.Unplayable))
            return;
        if (card.Pool is not PicklerFrigilCardPool)
            return;
        
        __result = false;
    }
}