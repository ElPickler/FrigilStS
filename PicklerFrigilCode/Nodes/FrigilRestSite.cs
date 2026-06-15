#region

using Godot;
using MegaCrit.Sts2.Core.Nodes.RestSite;

#endregion

namespace PicklerFrigil.PicklerFrigilCode.Nodes;

public partial class FrigilRestSite : NRestSiteCharacter
{
	/*
	public void FlipX()
	{
		
		Vector2 scale;
		foreach (Node2D childSpineNode in GetChildSpineNodes())
		{
			scale = childSpineNode.Scale;
			scale.X = 0f - childSpineNode.Scale.X;
			childSpineNode.Scale = scale;
			scale = childSpineNode.Position;
			scale.X = 0f - childSpineNode.Position.X;
			childSpineNode.Position = scale;
		}
		Control controlRoot = _controlRoot;
		scale = _controlRoot.Scale;
		scale.X = 0f - _controlRoot.Scale.X;
		controlRoot.Scale = scale;
		
		
		Sprite2D sprite = GetNode<Sprite2D>("%Sprite2D");
		sprite.FlipH = true;
		
		
		
		Vector2 scale;
		Control root = GetNode<Control>("ControlRoot");
		Control controlRoot = root;
		scale = root.Scale;
		scale.X = 0f - root.Scale.X;
		controlRoot.Scale = scale;
		
	}*/
}
