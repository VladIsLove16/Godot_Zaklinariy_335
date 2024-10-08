using Godot;
using System;
using static SwipeInput;
using System.Diagnostics;

public partial class UI : Control
{
	[Export] private Label CurrentScore;
	[Export] private Label GlobalScore;
	[Export] private Label Bits;
	[Export] private Label HealthText;
	[Export] private Label DoubleTap;
	[Export] private Label UDiedText;
	[Export] private Label ScoreAdd;

	[Export] private Sprite LeftSwipeIcon;
	[Export] private Sprite RightSwipeIcon;
	[Export] private Sprite UpperSwipeIcon;
	[Export] private Sprite DownSwipeIcon;

	[Export] private GhostSpawner GhostSpawner;
	[Export] private Character Character;
	[Export] private ScoreManager ScoreManager;
	public override void _Ready()
	{
		LeftSwipeIcon = GetChild<Sprite>(0);
		RightSwipeIcon = GetChild<Sprite>(1);
		UpperSwipeIcon = GetChild<Sprite>(2);
		DownSwipeIcon = GetChild<Sprite>(3);

		GlobalScore = GetNode<Label>("GlobalScore");
        CurrentScore = GetNode<Label>("CurrentScore");
        Bits = GetNode<Label>("Bits");
        HealthText = GetNode<Label>("health");
        DoubleTap = GetNode<Label>("DoubleTap");
		UDiedText = GetNode<Label>("UDied");
        ScoreAdd = GetNode<Label>("ScoreAdd");

		GhostSpawner = GetNode<GhostSpawner>("../GhostSpawner");
		Character = GetNode<Character>("../Character");
		ScoreManager = GetNode<ScoreManager>("../ScoreManager");

		// Подписка на события
		EventBus.Instance.SubscribeOn_Character_Died(Character_OnDie);
		EventBus.Instance.SubscribeOn_ScoreChanged(UpdateScoreText);
		EventBus.Instance.SubscribeOnPlayerHealthChanged(PlayerHealthChanged);
		EventBus.Instance.SubscribeOn_ScoreChangedBy(UpdateScoreAdd);
		// Инициализация начальных значений
		PlayerHealthChanged();
		HideAllIcons();
	}
	public void UpdateScoreText()
	{
		GlobalScore.Text = ScoreManager.GlobalScore.ToString();
        CurrentScore.Text = ScoreManager.CurrentScore.ToString();
	}
	private void UpdateScoreAdd(int score)
	{
        ScoreAdd.Text ="+" + score.ToString();

    }
	public override void _Process(float delta)
	{
		ShowSwipeHelp();
		Bits.Text = GhostSpawner.Bits.ToString();
    }

	private void ShowSwipeHelp()
    {
		Ghost ghost = GhostSpawner.GetCurrentGhost();
        //Debug.Print(ghost. SwipeType.ToString());
        if (ghost != null )
		{

			if( ghost.CanBeKilled())
				ShowSwipeIcon(ghost.SwipeType, ghost.GhostType);
			else
			{
				ShowSwipeIcon(ghost.SwipeType, ghost.GhostType,0.3f);
			}
		}
		else
		{
			HideAllIcons();
		}
	}

	private void ShowSwipeIcon(SwipeInput.SwipeType swipeType,GhostType ghostType, float alpha = 1f)
	{
		if(ghostType == GhostType.Skip)
		{
			HideAllIcons();
			return;
        }
		if(ghostType==GhostType.DoubleSwipe)
		{
			DoubleTap.Visible = true;
		}	
		else
            DoubleTap.Visible = false;

        if (swipeType == SwipeType.tap)
		{
			ShowAllIcons(alpha);
			return;

        }
		HideAllIcons();

		if (swipeType == SwipeInput.SwipeType.left)
		{
			LeftSwipeIcon.Visible = true;
			LeftSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
		}

		else if (swipeType == SwipeInput.SwipeType.right)
		{
			RightSwipeIcon.Visible = true;
			RightSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
		}

		if (swipeType == SwipeInput.SwipeType.upper)
		{
			UpperSwipeIcon.Visible = true;
			UpperSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
		}

		else if (swipeType == SwipeInput.SwipeType.down)
		{
			DownSwipeIcon.Visible = true;
			DownSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
		}
	}

	private void HideAllIcons()
	{
		LeftSwipeIcon.Visible = false;
		RightSwipeIcon.Visible = false;
		UpperSwipeIcon.Visible = false;
		DownSwipeIcon.Visible = false;
	}
	private void ShowAllIcons(float alpha = 1f)
	{
		LeftSwipeIcon.Visible = true;
		RightSwipeIcon.Visible = true;
        UpperSwipeIcon.Visible = true;
        DownSwipeIcon.Visible = true;

        LeftSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
        RightSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
        UpperSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
        DownSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, alpha);
	}

	private void Character_OnDie()
	{
		UDiedText.Visible = true;
	}

	private void PlayerHealthChanged()
	{
		HealthText.Text = Character.GetHealth().ToString();
	}
}



