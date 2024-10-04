using Godot;
using static Godot.GD;
using System.Diagnostics;
using static SwipeInput;

namespace Zaklinariy_Godot353.Scripts
{
	public class GameManager : Spatial
	{
		
		//���������� � 2 ���� ������, ��� ������
		const float REACTION_TIME = 0.7f;
		//������ ����� ���������� �������(����� ��������) � ������ �����, � ������� ����� �����
		const float firstWindow = 0f;
		const int TimeToKillZoneInBits = 7;//�� ������� �������� 2 ����, ������� 1 ��� �������� 0,44 �������

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
            GhostKillingZone.Scale = new Vector3(REACTION_TIME / 2, 1, 1);
            GhostKillingZone.GlobalTransform = new Transform(GlobalTransform.basis, new Vector3((TimeToKillZoneInBits * GhostSpawner.GetTimeBetweenBits())- firstWindow + GhostKillingZone.Scale.x / 2, 0, 0));
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
		}
		public void RestartGame()
		{
			GetTree().ReloadCurrentScene();
		}
	}
}
