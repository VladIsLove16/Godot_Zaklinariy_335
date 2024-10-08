using Godot;
using System;
public class GhostKillingZone : Area
{
	private void _on_GhostKillingZone_area_entered(object area)
	{
		try
		{
			Node node = (Node)area;
			Ghost ghost = (Ghost)node.GetParent();
			ghost.SetCanBeKilled(true);
			EventBus.Instance.RaiseOn_Ghost_CanBeKilled();
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


