using Godot;
using System;
using System.Diagnostics;

public partial class DieSoundButton : Button
{
	public void _On_Pressed()
	{
        Debug.WriteLine("Pressed");
		AudioManager.Instance.PlaySound(AudioManager.SoundType.PlayerDie);
    }
}
