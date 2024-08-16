using Godot;
using System;
using System.Diagnostics;

public partial class PlayBtn : Button
{
	private void _on_pressed()
	{
		Debug.WriteLine("preessssed");
		GetTree().ChangeScene("res://Scenes/Game.tscn");
	}
	private void _on_button_up()
	{
	}
}	





