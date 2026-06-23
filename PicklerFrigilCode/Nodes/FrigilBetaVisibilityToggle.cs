using Godot;
using System;
using PicklerFrigil.PicklerFrigilCode;

public partial class FrigilBetaVisibilityToggle : AnimatedSprite2D
{
	public override void _Ready()
	{
		if(!FrigilModConfig.BetaArt){
			Visible = false;
		}
		else{
			Visible = true;
		}
	}
}
