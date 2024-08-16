using Godot;
using System;

public class EventBus : Node
{
    public static event Action On_Ghost_Died;
    public static event Action On_Ghost_Reached_Character;
    public static void RaiseGhostDied()
    {
        On_Ghost_Died?.Invoke();
        GD.Print("Ghost Died");
    }
    public static void SubscribeGhostDied(Action action)
    {
        On_Ghost_Died += action;
    }
    public static void UnSubscribeGhostDied(Action action)
    {
        On_Ghost_Died -= action;
    }
    public static void RaiseOn_Ghost_Reached_Character()
    {
        On_Ghost_Reached_Character?.Invoke();
        GD.Print("Ghost Died");
    }
    public static void SubscribeOn_Ghost_Reached_Character(Action action)
    {
        On_Ghost_Reached_Character += action;
    }
    public static void UnSubscribeOn_Ghost_Reached_Character(Action action)
    {
        On_Ghost_Reached_Character -= action;
    }
}
