using System;
using Godot;

public partial class Character : Spatial
{
	[Export]
	public int Health;
	[Export]
	public bool candie=false;
	public override void _Ready()
	{
		EventBus.Instance.SubscribeOn_PlayerMistake(GetDamage);
	}
	public void GetDamage()
	{
		Health--;
		EventBus.Instance.RaiseOnPlayerHealthChanged(Health);
		if (Health < 0)
			Die();
	}
	public int GetHealth()
	{
		return Health;
	}
	private void Die()
	{
		if(candie)
		{

			EventBus.Instance.RaiseOn_Character_Died();
			AudioManager.Instance.PlaySound(AudioManager.SoundType.PlayerDie);
		}
	}
}
