using System.Collections.Generic;

public static class SpawnChances
{
	public static Dictionary<GhostType, int> values = new Dictionary<GhostType, int>()
	{
		[GhostType.Swipe] = 0,
		[GhostType.DoubleSwipe] = 0,
		[GhostType.DoubleTap] = 0,
		[GhostType.Skip] = 1500,
		[GhostType.Spin] = 0,

	};
}
