using Godot;
using System;

public class GoToMenu : Button
{
	private void _on_Menu_pressed()
	{
		GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
	}
		
}



