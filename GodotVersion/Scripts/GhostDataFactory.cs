using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static SwipeInput;
public class GhostDataFactory
{
	private int SWIPETYPES;
	private float SPEED_CONST;
	private Character  character;
	private Vector3  spawnPos;
	private GhostType ghostType;
	public GhostDataFactory(int SWIPETYPES, float SPEED_CONST, Character character, Vector3 spawnPos)
	{
		this.SWIPETYPES = SWIPETYPES;
		this.SPEED_CONST = SPEED_CONST;
		this.character = character;
		this.spawnPos = spawnPos;
	}
	public GhostData GetRandomGhostData()
	{
		ghostType = GetGhostType();
		SwipeType swipeType = GetSwipeType();
		Vector3 spawnPosition = GetSpawnPosition();
		Vector3 playerPos = GetPlayerPos();
		float speed = GetGhostSpeed();
		int health = GetGhostHealth();

		return new GhostData(swipeType, spawnPosition, playerPos, health, speed, ghostType);
	}

	private GhostType GetGhostType()
	{
		int totalWeight  = 0;
		Dictionary<GhostType, int> values = new Dictionary<GhostType, int>();
		foreach(var type in SpawnChances.values)
		{
			totalWeight += type.Value;
			values.Add(type.Key, totalWeight);
		}
		values.OrderBy(x => x.Value);
		int chance = (int)(GD.Randi() % totalWeight);
		//GD.Print("chance" + chance.ToString());
		foreach (var type in values)
		{
			//GD.Print("VALUE"+ type.Value);
			if (chance <= type.Value)
				return type.Key;
		}
		return values.LastOrDefault().Key;
	}

	private SwipeType GetSwipeType()
	{
		if(ghostType==GhostType.DoubleTap)
		{
			return SwipeType.tap;
		}
		int chance = (int)GD.Randi() % 100;
		return (SwipeType)(GD.Randi() % SWIPETYPES);
	}

	private int GetPoints(int health)
	{
		return health;
	}

	private Vector3 GetPlayerPos()
	{
		return //GetViewport().GetCamera().GlobalTransform.origin;
				character.GlobalTransform.origin;
	}
	private Vector3 GetSpawnPosition()
	{
		return spawnPos;
	}
	private int GetGhostHealth()
	{
		if (ghostType == GhostType.Swipe)
			return 1;
		if(ghostType == GhostType.DoubleSwipe)
			return 2;
		if(ghostType == GhostType.DoubleTap)
			return 1;
		return 1;
	}
	private float GetGhostSpeed()
	{
		return SPEED_CONST;
	}
}
