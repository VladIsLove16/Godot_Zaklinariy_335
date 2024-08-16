using System;
using Godot;
using static SwipeInput;

public partial class Ghost : Sprite3D
{
	public SwipeType SwipeType;
	private Vector3 target;
	public float speed;
	float ElapsedTime;
	float additionSpeed;
	public override void _Process(float delta)
	{
		GlobalTransform = new Transform(GlobalTransform.basis, GlobalTransform.origin.MoveToward(target, (speed + additionSpeed) * (float)delta));
		GD.Print(GlobalTransform.origin);
		ChangeSpeed(delta);
		TargetReached();
	}

	private void ChangeSpeed(float delta)
	{
		ElapsedTime += delta;
		additionSpeed = ElapsedTime / 2;
	}

	private void TargetReached()
	{
		if(GlobalTransform.origin.DistanceTo(target) < 1f )
		{
			EventBus.RaiseOn_Ghost_Reached_Character();
			Die();
			QueueFree();
		}
	}

	public void Construct(SwipeType SwipeType, Vector3 where, Vector3 target)
	{
		SetSwipeType(SwipeType);
		SetTarget(target);
		GlobalTransform = new Transform(Quat.Identity, where);
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
		EventBus.RaiseGhostDied();
		QueueFree();
	}
}

