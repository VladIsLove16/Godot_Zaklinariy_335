using Godot;
using System;

public class Exitbtn : Button
{
	private void _on_Exit_pressed()
	{
		GetTree().Quit();
	}
}



