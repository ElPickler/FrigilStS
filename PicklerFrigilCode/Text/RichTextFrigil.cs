using System.ComponentModel;
using Godot;
using Godot.Bridge;
using MegaCrit.Sts2.Core.RichTextTags;

namespace PicklerFrigil.PicklerFrigilCode.Text;


public partial class RichTextFrigil : AbstractMegaRichTextEffect
{
    public string bbcode = "frigil";
    
    protected override string Bbcode => bbcode;
    
    public override bool _ProcessCustomFX(CharFXTransform charFx)
    {
        charFx.Color = new("a7e7eb");
        return true;
    }
}