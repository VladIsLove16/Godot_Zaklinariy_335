using Godot;
using System;
public partial class ScoreManager : Node
{
	public int Score;
	public override void _Ready()
	{
		EventBus.SubscribeGhostDied(_on_Ghost_GhostDied);
	}
	private void _on_Ghost_GhostDied()
	{
		AddScore();
		GD.Print("Ghost died and score:" + Score);
	}

	public void AddScore()
	{
		Score++;
	}
	public void Reset()
	{
		Score = 0;
	}
}



