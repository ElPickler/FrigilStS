using BaseLib.Abstracts;
using BaseLib.Extensions;
using PicklerFrigil.PicklerFrigilCode.Extensions;
using Godot;

namespace PicklerFrigil.PicklerFrigilCode.Powers;

public abstract class PicklerFrigilPower : CustomPowerModel
{
    //Loads from PicklerFrigil/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
}