using Godot;
using System;

public class Pausebtn : Button
{
	private void _on_Pause_pressed()
	{
		try
		{
			GamePause.Pause();
			GetTree().Paused = true;
		}
		catch (Exception e) {
			GD.Print(e);
		
		}
	}
}
