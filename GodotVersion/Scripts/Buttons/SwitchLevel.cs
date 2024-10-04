using Godot;
using Zaklinariy_Godot353.Scripts;

public partial class SwitchLevel : Button
{
	[Export]
	private Level level;
	private void _on_pressed()
	{
		GameSettings.ChangeLevel(level);
		GD.Print("new level" + level);
		Label Level  = GetNode<Label>("../Level");
		Level.Text = level.ToString();
	}
}	
