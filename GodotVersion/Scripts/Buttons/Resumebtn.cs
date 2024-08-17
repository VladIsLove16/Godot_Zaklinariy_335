using Godot;
using System;

public class Resumebtn : Button
{
	private void _on_Resume_pressed()
	{
		GetTree().Paused = false;
		GamePause.Play();
	}
}

