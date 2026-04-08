using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using PicklerFrigil.PicklerFrigilCode.Relics;

namespace PicklerFrigil.PicklerFrigilCode.Patches;

[HarmonyPatch(typeof(TouchOfOrobas), "RefinementUpgrades", MethodType.Getter)]
public class TouchOfOrobasPatch
{
    [HarmonyPostfix]

    public static void Postfix(ref Dictionary<ModelId, RelicModel> __result)
    {
        __result[ModelDb.Relic<GlisteningAmethyst>().Id] = ModelDb.Relic<RadiantBismuth>();
    }
}