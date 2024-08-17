using Godot;
using System;

public class Pausebtn : Button
{
	private void _on_Pause_pressed()
	{
		GamePause.Pause();
	}
}



