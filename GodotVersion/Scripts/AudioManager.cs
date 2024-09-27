using Godot;
using System.Diagnostics;

public partial class AudioManager : AudioStreamPlayer
{
	public enum SoundType
	{
		GhostDie,
		GhostGetDamage,
		PlayerDie
	}

	public static AudioManager Instance { get; private set; }
	[Export]
	public AudioStream GhostDie;
	[Export]
	public AudioStream PlayerWrong;
	[Export]
	public AudioStream GhostGetDamage;

    public override void _Ready()
    {
		Instance = this;
        EventBus.Instance.SubscribeOn_PlayerRight(OnPlayerRight);
        EventBus.Instance.SubscribeOn_PlayerMistake(OnPlayerWrong);

        // Загрузка аудиофайлов
        //GhostDie = (AudioStream)GD.Load("res://Sounds/GhostDied.mp3");
        //PlayerDie = (AudioStream)GD.Load("res://Sounds/CharacterDied.mp3");
        // Инициализация AudioStreamPlayer

	}
	private void OnPlayerRight()
	{
		Stream = GhostDie;
		Play();
	}
	private void OnPlayerWrong()
	{
		Stream = PlayerWrong;
		Play();
	}
	public void Initialize(Node parent)
	{
		// Добавление AudioStreamPlayer в иерархию сцены
	}

	public void PlaySound(SoundType soundType)
	{
		switch (soundType)
		{
			case SoundType.GhostDie:
				Stream = GhostDie;
				break;
			case SoundType.PlayerDie:
				Stream = PlayerWrong;
				break;
			case SoundType.GhostGetDamage:
				Stream = GhostGetDamage;
				break;
		}
		Play();
	}
	public void _OnReady()
	{

	}
}
