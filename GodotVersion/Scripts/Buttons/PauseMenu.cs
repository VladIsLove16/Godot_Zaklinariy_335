using Godot;
using System;

public class PauseMenu : TextureRect
{
	private void _on_Pause_pressed()
	{
		Show();
	}
	private void _on_Resume_pressed()
	{
		Hide();
	}
}






