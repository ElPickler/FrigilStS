using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards.Holders;
using PicklerFrigil.PicklerFrigilCode.Powers;

namespace PicklerFrigil.PicklerFrigilCode.Patches;

[HarmonyPatch]
public class FlowGlowOverridePatch
{
    [HarmonyPatch(typeof(NHandCardHolder), nameof(NHandCardHolder.UpdateCard))]
    public class FlowGlowPatch
    {
        [HarmonyPostfix]
        private static void Postfix(NHandCardHolder __instance)
        {
            if (__instance.CardNode == null)
                return;
            CardModel? card = __instance.CardNode.Model;
            if (card == null || !card.CanPlay() || card.ShouldGlowGold || card.ShouldGlowRed)
                return;
            if (!card.Owner.Creature.HasPower<FlowPower>())
                return;
            if (card.Type != CardType.Skill)
                return;
            if (card.GainsBlock)
                __instance.CardNode.CardHighlight.Modulate = new Color(0.09f, 0.77f, 0.43f);
            else
                __instance.CardNode.CardHighlight.Modulate = new Color(0.22f, 0.44f, 0.33f);
        }
    }
}