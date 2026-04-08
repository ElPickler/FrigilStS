using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using PicklerFrigil.PicklerFrigilCode.Character;
using PicklerFrigil.PicklerFrigilCode.Extensions;
using Godot;

namespace PicklerFrigil.PicklerFrigilCode.Relics;

[Pool(typeof(PicklerFrigilRelicPool))]
public abstract class PicklerFrigilRelic : CustomRelicModel
{
    public override string PackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".RelicImagePath();
        }
    }

    protected override string PackedIconOutlinePath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic_outline.png".RelicImagePath();
        }
    }

    protected override string BigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".BigRelicImagePath();
        }
    }
}