using Godot;
using System.Numerics;
using static SwipeInput;

public class GhostData
{
    public SwipeType SwipeType;
    public GhostType GhostType;
    public Godot.Vector3 where;
    public Godot.Vector3 target;
    public int health;
    public float speed = 1f;
    public GhostData(SwipeType swipeType, Godot.Vector3 where, Godot.Vector3 target, int health, float speed, GhostType type)
    {
        SwipeType = swipeType;
        this.where = where;
        this.target = target;
        this.health = health;
        this.speed = speed;
        GhostType = type;
    }   
}



