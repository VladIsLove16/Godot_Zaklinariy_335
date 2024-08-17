using Godot;
using System;
using System.Diagnostics;

public partial class PlayBtn : Button
{
	private void _on_pressed()
	{
		GetTree().ChangeScene("res://Scenes/Game.tscn");
	}
}	





