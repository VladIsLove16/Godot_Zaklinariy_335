using Godot;
using System;
public partial class ScoreManager : Node
{
	public int Score;
	public override void _Ready()
	{
		EventBus.Instance.SubscribeOn_PlayerRight(AddScore);
	}
	public void AddScore()
	{
		Score++;
		EventBus.Instance.RaiseOn_ScoreChanged(Score);

	}
	public void Reset()
	{
		Score = 0;
	}
}



