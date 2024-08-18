using Godot;
using System;

public static class GamePause 
{
	public  static bool IsGamePaused { get;  private set; }
	public static void Pause()
	{
		IsGamePaused = true;
    }
	public static void Play()
	{
        IsGamePaused = false;

    }
}
