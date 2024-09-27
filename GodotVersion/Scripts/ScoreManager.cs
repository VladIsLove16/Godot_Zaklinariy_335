using Godot;
using System;
public partial class ScoreManager : Node
{
	public int CurrentScore { get;private set; }
	public int GlobalScore { get; private set; }
    public override void _Ready()
	{
		EventBus.Instance.SubscribeOn_Ghost_Killed(AddScore);
		EventBus.Instance.SubscribeOn_PlayerMistake(On_Mistake);
	}

	private void On_Mistake()
	{
		GlobalScore += CurrentScore;
		CurrentScore = 0;
		EventBus.Instance.RaiseOn_ScoreChanged(CurrentScore);
	}
	public void AddScore(GhostType type)
	{
        switch (type)
        {

            case GhostType.Swipe:
                CurrentScore += 1;
				break;
            case GhostType.DoubleSwipe:
                CurrentScore += 2;
                break;
            case GhostType.DoubleTap:
                CurrentScore *= 3;
                break;
            case GhostType.Skip:
                CurrentScore *= 2;
                break;
            case GhostType.Spin:
                CurrentScore *= 0;
                break;
        }
		GD.Print("Ghost " + type.ToString() + " " + (int)type + "killed " + CurrentScore);
		EventBus.Instance.RaiseOn_ScoreChanged(CurrentScore);

	}
	public void Reset()
	{
		CurrentScore = 0;
        EventBus.Instance.RaiseOn_ScoreChanged(CurrentScore);
    }

}



