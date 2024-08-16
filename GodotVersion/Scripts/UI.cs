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

	[Export] private GhostSpawner GhostSpawner;
	[Export] private Character Character;
	[Export] private ScoreManager ScoreManager;
	public override void _Ready()
	{
		LeftSwipeIcon = GetChild<Sprite>(0);
		RightSwipeIcon = GetChild<Sprite>(1);
		Score = GetChild<Label>(2);
		HealthText = GetChild<Label>(3);
		UDiedText = GetChild<Label>(4);
		RestartBtn = GetChild<Button>(5);

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
		if (ghost != null)
		{
			ShowSwipeIcon(ghost.SwipeType);
		}
		else
		{
			HideAllIcons();
		}
	}

	private void ShowSwipeIcon(SwipeInput.SwipeType swipeType)
	{
		HideAllIcons();

		if (swipeType == SwipeInput.SwipeType.left)
		{
			LeftSwipeIcon.Visible = true;
		}
		else if (swipeType == SwipeInput.SwipeType.right)
		{
			RightSwipeIcon.Visible = true;
		}
	}

	private void HideAllIcons()
	{
		LeftSwipeIcon.Visible = false;
		RightSwipeIcon.Visible = false;
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



