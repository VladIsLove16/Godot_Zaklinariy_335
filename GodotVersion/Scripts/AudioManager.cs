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
    public AudioStream PlayerDie;

	public AudioManager()
	{
		Instance = this;
		// Загрузка аудиофайлов
		//GhostDie = (AudioStream)GD.Load("res://Sounds/GhostDied.mp3");
		EventBus.Instance.SubscribeOn_PlayerRight(OnPlayerRight);
		PlayerDie = (AudioStream)GD.Load("res://Sounds/CharacterDied.mp3");
		// Инициализация AudioStreamPlayer
		Debug.WriteLine("AudioManager created");

	}
	private void OnPlayerRight()
	{
		Stream = GhostDie;
		Play();

    }
	public void Initialize(Node parent)
	{
		// Добавление AudioStreamPlayer в иерархию сцены
	}

	//public void PlaySound(SoundType soundType)
	//{
	//	Debug.WriteLine("AudioManager playing" + soundType.ToString());
	//	switch (soundType)
	//	{
	//		case SoundType.GhostDie:
	//			audioPlayer.Stream = GhostDie;
	//			break;
	//		case SoundType.PlayerDie:
	//			audioPlayer.Stream = PlayerDie;
	//			break;
	//	}
	//	audioPlayer.Play();
	//}
	public void _OnReady()
	{

	}
}
