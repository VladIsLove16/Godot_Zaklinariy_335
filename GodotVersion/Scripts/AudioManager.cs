using Godot;
using System.Diagnostics;

public partial class AudioManager : AudioStreamPlayer
{
	public enum SoundType
	{
		GhostDie,
		PlayerDie
	}

	public static AudioManager Instance { get; private set; }
	[Export]
	public AudioStream GhostDie;
	[Export]
	public AudioStream PlayerWrong;

    public override void _Ready()
    {
		Instance = this;
        EventBus.Instance.SubscribeOn_PlayerRight(OnPlayerRight);
        EventBus.Instance.SubscribeOn_PlayerMistake(OnPlayerWrong);

        // Загрузка аудиофайлов
        //GhostDie = (AudioStream)GD.Load("res://Sounds/GhostDied.mp3");
        //PlayerDie = (AudioStream)GD.Load("res://Sounds/CharacterDied.mp3");
        // Инициализация AudioStreamPlayer
        Debug.WriteLine("AudioManager created");

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
		GD.Print("audio m play player right");
	}
	public void Initialize(Node parent)
	{
		// Добавление AudioStreamPlayer в иерархию сцены
	}

	public void PlaySound(SoundType soundType)
	{
		Debug.WriteLine("AudioManager playing" + soundType.ToString());
		switch (soundType)
		{
			case SoundType.GhostDie:
				Stream = GhostDie;
				break;
			case SoundType.PlayerDie:
				Stream = PlayerWrong;
				break;
		}
		Play();
	}
	public void _OnReady()
	{

	}
}
