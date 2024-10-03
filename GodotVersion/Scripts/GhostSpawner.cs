using Godot;
using System.Diagnostics;
using Zaklinariy_Godot353.Scripts;

public partial class GhostSpawner : Spatial
{
	const int ROW = 0;
	const int SWIPETYPES = 4;
	const int bpm = 135;
	const float bps = 135f / 60f;
	const float SPEED_CONST = 1f;
	private ISpawnDelayProvider spawnDelayProvider;

	private float spawnDelay;
	[Export]
	private float FirstSpawnDelay;
	[Export]
	private int spawnDelayMultiplier;
	private float untilSpawn;
	[Export]
	private PackedScene pfGhost;
	[Export]
	private bool SpawnByTime;
	private bool CanSpawn=true;
	[Export]
	private bool SpawnOnAwake ;
	private Character character;
	private float ElapsedTime;
	[Export]
	private int StartSpeedMultiplier;
	GhostDataFactory ghostDataFactory;


	public override void _Ready()
	{
		character =  GetNode<Character>("../Character");
		spawnDelay = spawnDelayMultiplier / bps;
		EventBus.Instance.SubscribeOn_PlayerRight(Ghost_OnDie);
		ghostDataFactory  = new GhostDataFactory(SWIPETYPES, SPEED_CONST,character,GlobalTransform.origin);

	}
	public override void _Process(float delta)
	{
		if (SpawnOnAwake)
		{
			if (FirstSpawnDelay > 0)
				FirstSpawnDelay -= delta;
			else
			{
				Spawn();
				SpawnOnAwake = false;
			}
			
		}
		if (CanSpawn)
		{
			ElapsedTime += delta;
			if (SpawnByTime)
			{
				untilSpawn -= delta;
				if (untilSpawn < 0 && FirstSpawnDelay<=0)
				{
					Spawn();
					untilSpawn = spawnDelay;
				}
			}
		}
	}
	public void SetSpawnDelayProvider(ISpawnDelayProvider provider)
	{
		spawnDelayProvider = provider;
		SetSpawnDelayMultiplier(spawnDelayProvider.GetSpawnDelayMultiplier());

	}
	public int GetBpm()
	{

		return bpm; 
	}
	public float GetTimeBetweenBits()
	{

		return 1f/bps; 
	}
	public void SetSpawnDelayMultiplier(int spawnDelayMultiplier)
	{
		this.spawnDelayMultiplier = spawnDelayMultiplier;
	}
	public Ghost GetCurrentGhost()
	{
		if (GetChildCount() > 0 && GetChild(0) is Ghost ghost)
		{
			return ghost;
		}
		return null;
	}

	private void Spawn()
	{
		if (CanSpawn)
		{
			if (pfGhost != null)
			{
				Ghost ghost = (Ghost)pfGhost.Instance();

				GhostData data = ghostDataFactory.GetRandomGhostData();
				//Debug.Print("SPAWMED WITH: Swipetype:" + data.SwipeType.ToString() + "ghostType:" + data.GhostType);
				ghost.Construct(data);

				EventBus.Instance.RaiseOn_Ghost_Spawn(GlobalTransform.origin.z, ROW, data.SwipeType.ToString());

				AddChild(ghost);
			}
		}
	}
	public void StopSpawning()
	{
		CanSpawn  = false;
	}
	private void Ghost_OnDie()
	{
		if (!SpawnByTime)
		{
			Spawn();
		}
	}
}
