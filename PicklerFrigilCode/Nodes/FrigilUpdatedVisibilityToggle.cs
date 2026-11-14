using Godot;

namespace PicklerFrigil.PicklerFrigilCode.Nodes;

public partial class FrigilUpdatedVisibilityToggle : AnimatedSprite2D
{
	public override void _Ready()
	{
		if(FrigilModConfig.BetaArt){
			Visible = false;
		}
		else{
			Visible = true;
		}
	}
}
