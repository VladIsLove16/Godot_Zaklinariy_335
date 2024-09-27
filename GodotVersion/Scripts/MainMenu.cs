using Godot;
using System;

public class MainMenu : Control
{
	private void _on_Play_pressed()
	{
		GetTree().ChangeScene("res://Scenes/Game.tscn");
	}


	private void _on_Exit_pressed()
	{
		GetTree().Quit();
	}

}
