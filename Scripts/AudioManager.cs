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

	private AudioStream GhostDie;
	private AudioStream PlayerDie;
	private AudioStreamPlayer audioPlayer;

	public AudioManager()
	{
		Instance = this;
		// Загрузка аудиофайлов
		GhostDie = (AudioStream)GD.Load("res://Sounds/GhostDied.mp3");
		PlayerDie = (AudioStream)GD.Load("res://Sounds/CharacterDied.mp3");
		// Инициализация AudioStreamPlayer
		audioPlayer = new AudioStreamPlayer();
		Debug.WriteLine("AudioManager created");
	}

	public void Initialize(Node parent)
	{
		// Добавление AudioStreamPlayer в иерархию сцены
		parent.AddChild(audioPlayer);
	}

	public void PlaySound(SoundType soundType)
	{
		Debug.WriteLine("AudioManager playing" + soundType.ToString());
		switch (soundType)
		{
			case SoundType.GhostDie:
				audioPlayer.Stream = GhostDie;
				break;
			case SoundType.PlayerDie:
				audioPlayer.Stream = PlayerDie;
				break;
		}
		audioPlayer.Play();
	}
	public void _OnReady()
	{

	}
}
