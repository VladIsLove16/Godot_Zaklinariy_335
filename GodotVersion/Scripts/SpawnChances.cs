using System;
using System.Collections.Generic;
public static class SpawnChances
{
	public static Dictionary<GhostType, int> lvl1 = new Dictionary<GhostType, int>()
	{
		[GhostType.Swipe] = 0,
		[GhostType.DoubleSwipe] = 0,
		[GhostType.DoubleTap] = 0,
		[GhostType.Skip] = 1500,
		[GhostType.Spin] = 0,
	};
	public static Dictionary<GhostType, int> lvl2 = new Dictionary<GhostType, int>()
	{
		[GhostType.Swipe] = 0,
		[GhostType.DoubleSwipe] = 0,
		[GhostType.DoubleTap] = 0,
		[GhostType.Skip] = 1500,
		[GhostType.Spin] = 0,
	};
	public static Dictionary<GhostType, int> lvl3 = new Dictionary<GhostType, int>()
	{
		[GhostType.Swipe] = 0,
		[GhostType.DoubleSwipe] = 0,
		[GhostType.DoubleTap] = 0,
		[GhostType.Skip] = 1500,
		[GhostType.Spin] = 0,
	};
	public static Dictionary<GhostType, int> GetValues(Level level)
	{
		switch (level)
		{
			case Level.easy:
				return lvl1;
			case Level.normal:
				return lvl2;
			case Level.hard:
				return lvl3;
			default:
				return null;
		}


	}
}
