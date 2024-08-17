using System;
using Godot;
using static SwipeInput;

public partial class Ghost : Sprite3D
{
	public SwipeType SwipeType;
	private Vector3 target;
	public float speed;
	float ElapsedTime;
	private bool canBeKilled;

	public override void _Process(float delta)
	{
		GlobalTransform = new Transform(GlobalTransform.basis, GlobalTransform.origin.MoveToward(target, speed * delta));
		EventBus.instance.RaiseOn_Ghost_Position_Changed(GlobalTransform.origin.z, 0);

		ChangeSpeed(delta);
		TargetReached();
	}
	public void SetCanBeKilled(bool flag)
	{
		canBeKilled = flag;

	}
	public bool CanBeKilled()
	{
		return canBeKilled;
	}
	private void ChangeSpeed(float delta)
	{
		ElapsedTime += delta;
	}

	private void TargetReached()
	{
		if(GlobalTransform.origin.DistanceTo(target) < 1f )
		{
			EventBus.instance.RaiseOn_Ghost_Reached_Character();
			QueueFree();
		}
	}

	public void Construct(SwipeType SwipeType, Vector3 where, Vector3 target,float speed = 1f)
	{
		SetSwipeType(SwipeType);
		SetTarget(target);
		GlobalTransform = new Transform(Quat.Identity, where);
		this.speed = speed;

	}
	public void SetSwipeType(SwipeType SwipeType)
	{
	  this.SwipeType = SwipeType;
	}
	public void SetTarget(Vector3 target)
	{
		this.target = target;
	}
	
	public void Die()
	{
		AudioManager.Instance.PlaySound(AudioManager.SoundType.GhostDie);
		EventBus.instance.RaiseGhostDied();
		QueueFree();
	}
}



