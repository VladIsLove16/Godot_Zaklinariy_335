using Godot;
using System;

public partial class UI : CanvasLayer
{
	[Export] private Label Score;
	[Export] private Label HealthText;
	[Export] private Label UDiedText;
	private Button RestartBtn;

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
		UDiedText = GetChild<Label>(6);
		RestartBtn = GetChild<Button>(7);

		GhostSpawner = GetNode<GhostSpawner>("../GhostSpawner");
		Character = GetNode<Character>("../Character");
		ScoreManager = GetNode<ScoreManager>("../ScoreManager");

		// Подписка на события
		Character.OnGetDamage += Character_OnGetDamage;
		EventBus.instance.SubscribeOn_Character_Died(Character_OnDie);
		EventBus.instance.SubscribeGhostDied(UpdateScoreText);
		// Инициализация начальных значений
		Character_OnGetDamage();
		HideAllIcons();
	}
	public void UpdateScoreText()
	{
		Score.Text ="Score: " + ScoreManager.Score.ToString();
	}
	public override void _Process(float delta)
	{
		ShowSwipeHelp();
	}

	private void ShowSwipeHelp()
	{
		Ghost ghost = GhostSpawner.GetCurrentGhost();
		if (ghost != null )
		{

			if( ghost.CanBeKilled())
				ShowSwipeIcon(ghost.SwipeType);
			else
			{
				ShowSwipeIcon(ghost.SwipeType,0.3f);
			}
		}
		else
		{
			HideAllIcons();
		}
	}

	private void ShowSwipeIcon(SwipeInput.SwipeType swipeType,float alpha = 1f)
	{
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

	private void Character_OnDie()
	{
		UDiedText.Visible = true;
		RestartBtn.Visible = true;
	}

	private void Character_OnGetDamage()
	{
		HealthText.Text = Character.GetHealth().ToString();
	}
	private void _on_Restart_pressed()
	{
		GetTree().ReloadCurrentScene();
	}
}



