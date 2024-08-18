using Godot;
using System;

public class Particles : Particles2D
{

	public override void _Process(float delta)
	{
		Emitting = Input.IsMouseButtonPressed(1);
		SetPosition(GetViewport().GetMousePosition()); 
	}
}
