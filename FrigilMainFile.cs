using System.Reflection;
using BaseLib.Audio;
using BaseLib.Config;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using PicklerFrigil.PicklerFrigilCode;

namespace PicklerFrigil;

[ModInitializer(nameof(Initialize))]
public partial class FrigilMainFile : Node
{
    public const string ModId = "PicklerFrigil"; //Used for resource filepath

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);
    
    public static void Initialize()
    {
        ModConfigRegistry.Register(ModId, new FrigilModConfig());
       
        Harmony harmony = new(ModId);
        harmony.PatchAll();
        
        Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(Assembly.GetExecutingAssembly());
    }
}