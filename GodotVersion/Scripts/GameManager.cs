using Godot;
using System.Diagnostics;
using static SwipeInput;

public class GameManager : Spatial
{
	GhostSpawner GhostSpawner;
	Character Character;
	SwipeInput SwipeInput;
	public override void _Ready()
	{
		GhostSpawner =  GetNode<GhostSpawner>("../GhostSpawner");
		Character =  GetNode<Character>("../Character");
		SwipeInput =  GetNode<SwipeInput>("../SwipeInput");

		SwipeInput.OnSwipe += SwipeInput_OnSwipe;
		EventBus.instance.SubscribeOn_Character_Died(Character_OnDie);
		EventBus.instance.Sub(CharacterGetDamage);

	}
	private void CharacterGetDamage()
	{
		Character.GetDamage();
	}
	private void Character_OnDie()
	{
		GhostSpawner.StopSpawning();
	}

	private void SwipeInput_OnSwipe(SwipeArgs swipeArgs)
	{
		Ghost ghost = GhostSpawner.GetCurrentGhost();
		if (ghost==null)
		{
			GD.Print("Ghost is null");
			return;
		}
		if (swipeArgs.swipeType == ghost.SwipeType && ghost.CanBeKilled())
		{
			EventBus.instance.RaiseOn_PlayerRight();
			ghost.Die();
		}
		else
		{
			GD.Print("wronginput");
			EventBus.instance.RaiseOn_PlayerMistake();
		}
	}
	public void RestartGame()
	{
		GetTree().ReloadCurrentScene();
	}
}
