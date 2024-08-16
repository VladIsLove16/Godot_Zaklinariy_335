using Godot;
using System.Numerics;
using static SwipeInput;

public partial class GhostSpawner : Spatial
{

    [Export]
    private float FirstSpawnDelay;
    [Export]
	private float spawnDelay;
	[Export]
	private float untilSpawn;
	[Export]
	private PackedScene pfGhost;
	[Export]
	private bool SpawnByTime;
	private bool CanSpawn=true;
	[Export]
	private bool SpawnOnAwake ;
	[Export]
	private Character character;
	[Export]
	private float ElapsedTime;
	[Export]
	private float StartSpeed;
	public override void _Ready()
	{
		
		EventBus.instance.SubscribeGhostDied(Ghost_OnDie);

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
				if (untilSpawn < 0 && FirstSpawnDelay<0)
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
			   
				SwipeType swipeType = (SwipeType)(GD.Randi() % 2);
				Godot.Vector3 playerPosition =
					//GetViewport().GetCamera().GlobalTransform.origin;
					GetNode<Character>("../Character").Transform.origin;

				ghost.speed = StartSpeed;
                ghost.Construct(swipeType, GlobalTransform.origin, playerPosition);//mob.Initialize(mobSpawnLocation.Translation, playerPosition);

                EventBus.instance.RaiseOn_Ghost_Spawn(GlobalTransform.origin.x, 0,swipeType.ToString());

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



