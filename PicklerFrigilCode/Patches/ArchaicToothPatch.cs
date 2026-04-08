using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using PicklerFrigil.PicklerFrigilCode.Cards.Basic;

namespace PicklerFrigil.PicklerFrigilCode.Patches;

[HarmonyPatch(typeof(ArchaicTooth), "TranscendenceUpgrades", MethodType.Getter)]
public class ArchaicToothPatch
{
    [HarmonyPostfix]

    public static void Postfix(ref Dictionary<ModelId, CardModel> __result)
    {
        __result[ModelDb.Card<IcicleKick>().Id] = ModelDb.Card<BlizzardKick>();
    }
}