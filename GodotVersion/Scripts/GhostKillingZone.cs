using Godot;
using System;
public class GhostKillingZone : Area
{
	private void _on_GhostKillingZone_area_entered(object area)
	{
		GD.Print("Area type"+ area.GetType());
		try
		{
			Node node = (Node)area;
			GD.Print("node is exist");
			Ghost ghost = (Ghost)node.GetParent();
			GD.Print("Area is ghost");
			ghost.SetCanBeKilled(true);
		}
		catch(Exception e)
		{
			GD.Print(e);
		}

	}
	private void _on_GhostKillingZone_area_exited(object area)
	{
		if (area is Ghost)
		{
			Ghost ghost = (Ghost)area;
			ghost.SetCanBeKilled(false);
		}
	}

}


