using Godot;
public class GhostKillingZone : Area
{
	private void _on_GhostKillingZone_area_entered(object area)
	{
		if(area is Ghost)
		{
            Ghost ghost = (Ghost)area;
			ghost.SetCanBeKilled(true);
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


