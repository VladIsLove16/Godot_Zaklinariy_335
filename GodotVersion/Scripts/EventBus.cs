using Godot;
using System;

public class EventBus : Node
{
	public event Action On_Ghost_Died;
	public event Action On_Ghost_Reached_Character;
	public event Action On_Ghost_Position_Changed;
    public Node node;
    public static EventBus instance;
	public override void _Ready()
	{
		instance = this;
		node = GetNode("../main");

    }
	private void SendMessage(params string[] args)
	{
        node.Call("on_GodotSendMessage", args[0], args[1]);
    }
    public void RaiseGhostDied()
	{
		On_Ghost_Died?.Invoke();
		SendMessage("ghost", "spawn");

        GD.Print("Ghost Died");
	}
	public   void SubscribeGhostDied(Action action)
	{
		On_Ghost_Died += action;
	}
	public   void UnSubscribeGhostDied(Action action)
	{
		On_Ghost_Died -= action;
	}
	public   void RaiseOn_Ghost_Reached_Character()
	{
		On_Ghost_Reached_Character?.Invoke();
        SendMessage("ghost", "reachedcharacter");

        GD.Print("Ghost Died");
	}
	public   void SubscribeOn_Ghost_Reached_Character(Action action)
	{
		On_Ghost_Reached_Character += action;
	}
	public   void UnSubscribeOn_Ghost_Reached_Character(Action action)
	{
		On_Ghost_Reached_Character -= action;
	}
}
