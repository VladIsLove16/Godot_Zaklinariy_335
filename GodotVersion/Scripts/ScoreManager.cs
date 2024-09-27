using Godot;
using System;
public partial class ScoreManager : Node
{
	public int Score;
	public override void _Ready()
	{
		EventBus.Instance.SubscribeOn_PlayerRight(AddScore);
		EventBus.Instance.SubscribeOn_Ghost_Killed(AddScore);
	}
	public void AddScore()
	{
		Score++;
		EventBus.Instance.RaiseOn_ScoreChanged(Score);

	}
	public void AddScore(GhostType type)
	{
		if (type == GhostType.Skip)
			Score *= 2;
		else if (type == GhostType.DoubleTap)
			Score *= 3;
		if (type == GhostType.Swipe)
			Score += 1;
		else if (type == GhostType.DoubleSwipe)
			Score += 2;
		else if (type == GhostType.Spin)
			Score *= 0;
		else
			Score+= (int)type;

		GD.Print("Ghost " + type.ToString() + " " + (int)type + "killed " + Score);
		EventBus.Instance.RaiseOn_ScoreChanged(Score);

	}
	public void Reset()
	{
		Score = 0;
	}

}



