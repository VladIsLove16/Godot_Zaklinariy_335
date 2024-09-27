using Godot;
using System;
using static SwipeInput;
using System.Diagnostics;

public partial class UI : Control
{
	[Export] private Label Score;
	[Export] private Label HealthText;
	[Export] private Label DoubleTap;
	[Export] private Label UDiedText;

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

		Score = GetChild<Label>(4);
		HealthText = GetChild<Label>(5);
        DoubleTap = GetChild<Label>(6);
		UDiedText = GetChild<Label>(7);

		GhostSpawner = GetNode<GhostSpawner>("../GhostSpawner");
		Character = GetNode<Character>("../Character");
		ScoreManager = GetNode<ScoreManager>("../ScoreManager");

		// Подписка на события
		EventBus.Instance.SubscribeOn_Character_Died(Character_OnDie);
		EventBus.Instance.SubscribeOn_ScoreChanged(UpdateScoreText);
		EventBus.Instance.SubscribeOnPlayerHealthChanged(PlayerHealthChanged);
		// Инициализация начальных значений
		PlayerHealthChanged();
		HideAllIcons();
	}
	public void UpdateScoreText()
	{
		Score.Text = ScoreManager.Score.ToString();
	}
	public override void _Process(float delta)
	{
		ShowSwipeHelp();
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
			ShowAllIcons();
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
		DoubleTap.Visible = false;
	}
	private void ShowAllIcons()
	{
		LeftSwipeIcon.Visible = true;
		RightSwipeIcon.Visible = true;
        UpperSwipeIcon.Visible = true;
        DownSwipeIcon.Visible = true;

        LeftSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, 1f);
        RightSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, 1f);
        UpperSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, 1f);
        DownSwipeIcon.Modulate = new Color(LeftSwipeIcon.Modulate.r, LeftSwipeIcon.Modulate.g, LeftSwipeIcon.Modulate.b, 1f);
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



