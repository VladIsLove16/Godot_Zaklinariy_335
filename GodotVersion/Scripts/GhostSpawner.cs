using Godot;
using System.Numerics;
using static SwipeInput;

public partial class GhostSpawner : Spatial
{
	const int ROW = 0;
	const int SWIPETYPES = 4;
	const int bpm = 135;
	const float bps = 135f / 60f;
	//const float SPEED_CONST = 

	private float spawnDelay;
	private float GhostStartSpeed;
	private float distanceToPlayer;

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

	public override void _Ready()
	{
		character =  GetNode<Character>("../Character");
		distanceToPlayer = Mathf.Abs( character.GlobalTransform.origin.z - GlobalTransform.origin.z);
        spawnDelay = spawnDelayMultiplier / bps;

        GhostStartSpeed = Mathf.Abs( StartSpeedMultiplier * distanceToPlayer / spawnDelay);
		EventBus.Instance.SubscribeOn_PlayerRight(Ghost_OnDie);

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
				Ghost ghost = (Ghost)pfGhost.Instance(); //Mob mob = (Mob)MobScene.Instance();
			   
				SwipeType swipeType = (SwipeType)(GD.Randi() % SWIPETYPES);
				Godot.Vector3 playerPosition =
					//GetViewport().GetCamera().GlobalTransform.origin;
					character.GlobalTransform.origin;

				ghost.Construct(swipeType, GlobalTransform.origin, playerPosition, GhostStartSpeed);//mob.Initialize(mobSpawnLocation.Translation, playerPosition);

				EventBus.Instance.RaiseOn_Ghost_Spawn(GlobalTransform.origin.x, ROW, swipeType.ToString());

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



