using Godot;
using System;

public class Animator : Godot.AnimationPlayer
{
	public override void _Ready()
	{
		EventBus.Instance.SubscribeOn_PlayerMistake(EventBus_On_WrongInput);
		GD.Print("wrong inpout anim");
	}
	private void EventBus_On_WrongInput()
	{
		GD.Print("anim play");

	  Play();
	}
}
