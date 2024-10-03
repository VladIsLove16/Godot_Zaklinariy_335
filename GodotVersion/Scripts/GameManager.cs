using Godot;
using static Godot.GD;
using System.Diagnostics;
using static SwipeInput;

namespace Zaklinariy_Godot353.Scripts
{
	public class GameManager : Spatial
	{
		Level level;
		const float REACTION_TIME = 0.7f;
        const int MovingTimeInBits = 7;
        private float MovingTime;
        GhostSpawner GhostSpawner;
		Character Character;
		SwipeInput SwipeInput;
		GhostKillingZone GhostKillingZone;

		public override void _Ready()
		{
			GhostSpawner = GetNode<GhostSpawner>("../GhostSpawner");
			Character = GetNode<Character>("../Character");
			SwipeInput = GetNode<SwipeInput>("../SwipeInput");
			GhostKillingZone = GetNode<GhostKillingZone>("../GhostKillingZone");

			SwipeInput.OnInput += SwipeInput_OnSwipe;
			EventBus.Instance.SubscribeOn_Character_Died(Character_OnDie);
			SetTimers();
			ChangeDifficulty(GameSettings.Level);

		}
		public void ChangeDifficulty(Level newDifficulty)
		{
			level = newDifficulty;
			ISpawnDelayProvider delayProvider = new DifficultyBasedSpawnDelayProvider(level);
			GhostSpawner.SetSpawnDelayProvider(delayProvider);
		}
		public void SetTimers()
		{
			MovingTime = MovingTimeInBits * GhostSpawner.GetTimeBetweenBits()+0.05f;
            GhostSpawner.GlobalTransform = new Transform(GlobalTransform.basis, new Vector3(0, 0, 0));
			GhostKillingZone.GlobalTransform = new Transform(GlobalTransform.basis, new Vector3(MovingTime - REACTION_TIME / 2, 0, 0));
			GhostKillingZone.Scale = new Vector3(REACTION_TIME / 2, 1, 1);
			Character.GlobalTransform = new Transform(GlobalTransform.basis, new Vector3(MovingTime + Character.Scale.x / 2, 0, 0));
		}
		private void Character_OnDie()
		{
			GhostSpawner.StopSpawning();
		}

		private void SwipeInput_OnSwipe(SwipeArgs swipeArgs)
		{
			Ghost ghost = GhostSpawner.GetCurrentGhost();
			if (ghost == null)
			{
				return;
			}
			if (ghost.CanBeKilled())
			{
				ghost.OnInput(swipeArgs);
			}
		}
		public void RestartGame()
		{
			GetTree().ReloadCurrentScene();
		}
	}
}
