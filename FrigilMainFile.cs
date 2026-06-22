using System.Reflection;
using BaseLib.Audio;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace PicklerFrigil;

[ModInitializer(nameof(Initialize))]
public partial class FrigilMainFile : Node
{
    public const string ModId = "PicklerFrigil"; //Used for resource filepath
    
    public static readonly AutoModAudio Audio = new AutoModAudio($"res://PicklerFrigil/audio/");

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);
    
    public static void Initialize()
    {
        Harmony harmony = new(ModId);

        harmony.PatchAll();
        
        Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(Assembly.GetExecutingAssembly());
    }
}