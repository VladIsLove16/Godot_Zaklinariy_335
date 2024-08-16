using System;
using Godot;

public partial class Character : Spatial
{
	[Export]
	public int Health;
	public event Action OnDie;
	public event Action OnGetDamage;
	public void GetDamage()
	{
		Health--;
		OnGetDamage.Invoke();
		if (Health < 0)
			Die();
	}
	public int GetHealth()
	{
		return Health;
	}
	private void Die()
	{
		OnDie.Invoke();
		AudioManager.Instance.PlaySound(AudioManager.SoundType.PlayerDie);
	}
}
