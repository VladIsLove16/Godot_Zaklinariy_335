using Godot;
using System.Diagnostics;
using System.Reflection;
using Zaklinariy_Godot353.Scripts;

public partial class GhostSpawner : Spatial
{
	const int ROW = 0;
	const int SWIPETYPES = 4;
	const int bpm = 135;
	const float bps = 135f / 60f;
	const float SPEED_CONST = 1f;
	public int Bits=0;
	public float BitsTime=0;
	private float time;
	private int previousBit=0;
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
		BitsTime += delta;
		Bits = Mathf.RoundToInt(  ElapsedTime/ GetTimeBetweenBits()); // Увеличиваем счетчик
		Debug.Print(Bits + " " + previousBit);
		if (CanSpawn)
		{
			ElapsedTime += delta;
			//if (SpawnOnAwake)
			//{
			//	if (ElapsedTime > FirstSpawnDelay)
			//	{
			//		Spawn();
			//		previousBit = Bits;
			//		SpawnOnAwake = false;
			//	}
			//	return;
			//}
			if (SpawnByTime)
			{
				if(Bits!=previousBit)
					if (((Bits - previousBit) % spawnDelayMultiplier) == 0)
					{
						Spawn();
						previousBit  = Bits;
					}
			}
		}
	}
	public void SetSpawnDelayMultiplierProvider(ISpawnDelayProvider provider)
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
