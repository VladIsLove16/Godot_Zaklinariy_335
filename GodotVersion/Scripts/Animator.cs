using Godot;
using System;

public class Animator : Godot.AnimationPlayer
{
    public override void _Ready()
    {
        EventBus.instance.SubscribeOn_WrongInput(EventBus_On_WrongInput);
        GD.Print("wrong inpout anim");
    }
    private void EventBus_On_WrongInput()
    {
        GD.Print("anim play");

      Play();
    }
}
