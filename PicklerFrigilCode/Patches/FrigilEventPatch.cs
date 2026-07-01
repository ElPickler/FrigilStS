using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using PicklerFrigil.PicklerFrigilCode.Character;

namespace PicklerFrigil.PicklerFrigilCode.Patches;

[HarmonyPatch]
public class FrigilEventPatch
{
    [HarmonyPatch(typeof(ColorfulPhilosophers))]
    public static class FrigilColorfulPhilosophers
    {
        [HarmonyPatch("CardPoolColorOrder", MethodType.Getter)]
        [HarmonyPostfix]
        public static void Postfix(ref IEnumerable<CardPoolModel> __result)
        {
            __result = __result.Append(ModelDb.CardPool<PicklerFrigilCardPool>());
        }
    }
}