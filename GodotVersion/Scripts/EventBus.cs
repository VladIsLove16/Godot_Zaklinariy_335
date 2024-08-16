using Godot;
using System;

public class EventBus : Node
{
	public event Action On_Ghost_Died;
	public event Action On_Character_Died;
	public event Action On_Ghost_Reached_Character;
	public event Action On_Ghost_Position_Changed;
	public event Action On_Ghost_Spawn;
	public Node node;
	public static EventBus instance;
	public override void _Ready()
	{
		instance = this;
		node = GetNode("../main");

	}
	private void SendMessage(params string[] args)
	{
		switch(args.Length)
		{
			case 0:
				node.Call("on_GodotSendMessage", "", "", "", "", "");
				break;
			case 1:
				node.Call("on_GodotSendMessage", args[0], "", "", "", "");
				break;
			case 2:
				node.Call("on_GodotSendMessage", args[0], args[1], "", "", "");
				break;
			case 3:
				node.Call("on_GodotSendMessage", args[0], args[1], args[2], "", "");
				break;
			case 4:
				node.Call("on_GodotSendMessage", args[0], args[1], args[2], args[3], "");
				break;
			case 5:
				node.Call("on_GodotSendMessage", args[0], args[1], args[2], args[3], args[4]);
				break;
		}
	}


	public void RaiseOn_Ghost_Position_Changed(float x,int y)
	{
		On_Ghost_Position_Changed?.Invoke();
		SendMessage("ghost", "position", x.ToString(), y.ToString());
	}
	public void SubscribeOn_Ghost_Position_Changedd(Action action)
	{
		On_Ghost_Position_Changed += action;
	}
	public void UnSubscribeOn_Ghost_Position_Changed(Action action)
	{
		On_Ghost_Position_Changed -= action;
	}
	


	public void RaiseOn_Character_Died()
	{
		On_Character_Died?.Invoke();
		SendMessage("character", "died");
	}
	public void SubscribeOn_Character_Died(Action action)
	{
		On_Character_Died += action;
	}
	public void UnSubscribOn_Character_Died(Action action)
	{
		On_Character_Died -= action;
	}


	public void RaiseOn_Ghost_Spawn(float x,int y,string swipeType)
	{
		On_Ghost_Spawn?.Invoke();
		SendMessage("ghost", "spawn", x.ToString(), y.ToString());
	}
	public void SubscribeOn_Ghost_Spawn(Action action)
	{
		On_Ghost_Spawn += action;
	}
	public void UnSubscribeOn_Ghost_Spawnd(Action action)
	{
		On_Ghost_Spawn -= action;
	}



	public void RaiseGhostDied()
	{
		On_Ghost_Died?.Invoke();
		SendMessage("ghost", "died");

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
