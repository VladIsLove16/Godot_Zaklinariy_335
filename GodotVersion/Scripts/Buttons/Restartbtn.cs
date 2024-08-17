using Godot;
using System;

public class Restartbtn : Button
{
	private void _on_Restart_pressed()
{
        GetTree().ReloadCurrentScene();

    }
}



