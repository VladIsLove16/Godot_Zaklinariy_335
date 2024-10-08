using Godot;
using System;

public class EventBus : Node
{
	//ПЕРЕДАЮТСЯ В ARE
	//создан+направление свайпа
	private event Action On_Ghost_Spawn;

	private event Action<GhostType> On_GhostKilled;
	//можно убить
	private event Action On_Ghost_CanBeKilled;
	//позиция
	private event Action On_Ghost_Position_Changed;
	//убит(+балл)
	//private event Action On_Ghost_Died;
	private event Action On_PlayerRight;
	//не убит(-жизнь) и колво жизней
	private event Action On_PlayerMistake;
	//очки
	private event Action On_ScoreChanged;
	private event Action<int> On_ScoreChangedBy;
	//жизни
	private event Action OnPlayerHealthChanged;
	//остаток жизней
	//private event Action On_Ghost_Reached_Character;

	//НЕ ПЕРЕДАЮТСЯ В ARE
	//
	private event Action On_Character_Died;
	private event Action<Level> On_GameLevelChanged;
	//начало игры
	//private event Action StartedHealth;



	private Node node;
	public static EventBus Instance;
	public override void _Ready()
	{
		Instance = this;
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


	public void RaiseOnPlayerHealthChanged(int healthLeft)
	{
		OnPlayerHealthChanged?.Invoke();
		SendMessage("health", healthLeft.ToString());

	}
	public void SubscribeOnPlayerHealthChanged(Action action)
	{
		OnPlayerHealthChanged += action;
	}
	public void UnSubscribeOnPlayerHealthChanged(Action action)
	{
		OnPlayerHealthChanged -= action;
	}


	public void RaiseOn_Ghost_CanBeKilled()
	{
		On_Ghost_CanBeKilled?.Invoke();
		SendMessage("ghost", "CanBeKilled");
	}
	public void SubscribeOn_Ghost_CanBeKilled(Action action)
	{
		On_Ghost_CanBeKilled += action;
	}
	public void UnSubscribeOn_Ghost_CanBeKilled(Action action)
	{
		On_Ghost_CanBeKilled -= action;
	}

	public void RaiseOn_PlayerMistake()
	{
		On_PlayerMistake?.Invoke();
		SendMessage("input", "wrong");
	}
	public void SubscribeOn_PlayerMistake(Action action)
	{
		On_PlayerMistake += action;
	}
	public void UnSubscribeOn_PlayerMistake(Action action)
	{
		On_PlayerMistake -= action;
	}

	public void RaiseOn_PlayerRight()
	{
		On_PlayerRight?.Invoke();
		SendMessage("input", "right");
	}
	public void SubscribeOn_PlayerRight(Action action)
	{
		On_PlayerRight += action;
	}
	public void UnSubscribeOn_PlayerRight(Action action)
	{
		On_PlayerRight -= action;
	}


	public void RaiseOn_ScoreChanged(int score)
	{
		On_ScoreChanged?.Invoke();
		SendMessage("score", score.ToString());
	}
	public void SubscribeOn_ScoreChanged(Action action)
	{
		On_ScoreChanged += action;
	}
	public void UnSubscribeOn_ScoreChanged(Action action)
	{
		On_ScoreChanged -= action;
	}
	public void RaiseOn_ScoreChangedBy(int score)
	{
		On_ScoreChangedBy?.Invoke(score);
		SendMessage("score", score.ToString());
	}
	public void SubscribeOn_ScoreChangedBy(Action<int> action)
	{
		On_ScoreChangedBy += action;
	}
	public void UnSubscribeOn_ScoreChangedBy(Action<int> action)
	{
		On_ScoreChangedBy -= action;
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
		SendMessage("ghost", "spawn", x.ToString(), y.ToString(), swipeType);
	}
	public void SubscribeOn_Ghost_Spawn(Action action)
	{
		On_Ghost_Spawn += action;
	}
	public void UnSubscribeOn_Ghost_Spawnd(Action action)
	{
		On_Ghost_Spawn -= action;
	}

	public void RaiseOn_Ghost_Killed(string swipeType,GhostType type)
	{
		On_GhostKilled?.Invoke(type);
		SendMessage("ghost", "killed", swipeType);
	}
	public void SubscribeOn_Ghost_Killed(Action<GhostType> action)
	{
		On_GhostKilled += action;
	}
	public void UnSubscribeOn_Ghost_Killed(Action<GhostType> action)
	{
		On_GhostKilled -= action;
	}
	public void RaiseOn_GameLevelChanged(Level level)
	{
		On_GameLevelChanged?.Invoke(level);
		SendMessage("GameLevelChanged", level.ToString());
	}
	public void SubscribeOn_GameLevelChanged(Action<Level> action)
	{
		On_GameLevelChanged += action;
	}
	public void UnSubscribeOn_GameLevelChanged(Action<Level> action)
	{
		On_GameLevelChanged -= action;
	}
	
}
