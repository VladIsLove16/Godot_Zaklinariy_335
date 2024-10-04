using Godot;
using static SwipeInput;

namespace Zaklinariy_Godot353.Scripts
{
	public class GameManager : Spatial
	{
		//указывайте в 2 раза больше, чем хотите
		const float REACTION_TIME = 1.4f;
		//окошко между появлением стрелок(ярким горением) и первым битом, в который можно убить
		const float firstWindow = 0.03f;
		const int TimeToKillZoneInBits = 7;//за секудну примерно 2 бита, поэтому 1 бит примерно 0,44 секунды

		[Export]
		Level level;
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
            GameSettings.ChangeLevel(level);
			GameSettings.ChangeLevel(Level.hard);
			ChangeDifficulty(GameSettings.Level);
		}
		public void ChangeDifficulty(Level newDifficulty)
		{
			level = newDifficulty;
			ISpawnDelayProvider delayProvider = new DifficultyBasedSpawnDelayProvider(level);
			GhostSpawner.SetSpawnDelayMultiplierProvider(delayProvider);
		}
		public void SetTimers()
		{
			GhostKillingZone.Scale = new Vector3(REACTION_TIME/2, 1, 1);
			GhostKillingZone.GlobalTransform = new Transform(GlobalTransform.basis, new Vector3(((float) TimeToKillZoneInBits * (GhostSpawner.GetTimeBetweenBits()))- firstWindow + GhostKillingZone.Scale.x, 0, 0));
			GhostSpawner.GlobalTransform = new Transform(GlobalTransform.basis, new Vector3(0, 0, 0));
			Character.GlobalTransform = GhostKillingZone.GlobalTransform;
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
				bool result = ghost.OnInput(swipeArgs);
				if(!result)
					EventBus.Instance.RaiseOn_PlayerMistake();
			}
			else
                EventBus.Instance.RaiseOn_PlayerMistake();
        }
		public void RestartGame()
		{
			GetTree().ReloadCurrentScene();
		}
	}
}
