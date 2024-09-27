using System;
using System.Diagnostics;
using Godot;
using static SwipeInput;

public partial class Ghost : Sprite3D
{
	public SwipeType SwipeType;
	public GhostType GhostType;
	private Vector3 target;
	public float speed;
	float ElapsedTime;
	private bool canBeKilled;
	public int Health { get; private set; }
	public override void _Process(float delta)
	{
		GlobalTransform = new Transform(GlobalTransform.basis, GlobalTransform.origin.MoveToward(target, speed * delta));
		EventBus.Instance.RaiseOn_Ghost_Position_Changed(GlobalTransform.origin.x, 0);

		ChangeElapsedTime(delta);
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
	private void ChangeElapsedTime(float delta)
	{
		ElapsedTime += delta;
	}

	private void TargetReached()
	{
		if (GlobalTransform.origin.DistanceTo(target) < 1f)
		{
			if (GhostType == GhostType.Skip)
			{
				Die();
				return;
			}
			EventBus.Instance.RaiseOn_PlayerMistake();
			QueueFree();
		}
	}
	public void Construct(GhostData ghostData)
	{
		Construct(ghostData.SwipeType, ghostData.where, ghostData.target, ghostData.health, ghostData.GhostType, ghostData.speed);
	}
	public void Construct(SwipeType swipeType, Vector3 where, Vector3 target, int health, GhostType ghostType, float speed = 1f)
	{
		SetSwipeType(swipeType);
		SetTarget(target);
		GlobalTransform = new Transform(Quat.Identity, where);
		this.speed = speed;
		Health = health;
		GhostType = ghostType;
	}

	public void SetSwipeType(SwipeType SwipeType)
	{
		this.SwipeType = SwipeType;
	}
	public void SetTarget(Vector3 target)
	{
		this.target = target;
	}

	public virtual void GetDamage()
	{
		Health--;
		if (Health == 0)
			Die();
		else
		{
			AudioManager.Instance.PlaySound(AudioManager.SoundType.GhostGetDamage);
			EventBus.Instance.RaiseOn_PlayerRight();
		}
	}
	public void Die()
	{
		AudioManager.Instance.PlaySound(AudioManager.SoundType.GhostDie);
		EventBus.Instance.RaiseOn_Ghost_Killed(SwipeType.ToString(), GhostType);
		GD.Print("Died" + GhostType);
		QueueFree();
	}
	public bool OnInput(SwipeArgs swipeArgs)
	{
		if (GhostType == GhostType.Skip)
		{
			EventBus.Instance.RaiseOn_PlayerMistake();
			QueueFree();
			return false;

		}

		if (SwipeType == SwipeType.tap)
		{
			if (swipeArgs.swipeType == SwipeType.tap)
			{
				if (swipeArgs.isDoubleTap)
				{
					Die();
					return true;
				}
			}
		}
		else
		{
			if (swipeArgs.swipeType == SwipeType)
			{
				GetDamage();
				return true;
			}
		}
		return false;

	}
}


