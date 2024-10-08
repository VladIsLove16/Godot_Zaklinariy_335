using System;
using Godot;

public partial class Character : Spatial
{
	[Export]
	public int Health;
	[Export]
	public bool candie=false;
	[Export]
	private int ErrorsInARowToGetDamage;
	private int CurrentErrorRow;
	public override void _Ready()
	{
		EventBus.Instance.SubscribeOn_PlayerMistake(GetDamage);
	}
	public void GetDamage()
	{
		CurrentErrorRow++;
		if(CurrentErrorRow > ErrorsInARowToGetDamage)
		{
			CurrentErrorRow = 0;
            Health--;
            EventBus.Instance.RaiseOnPlayerHealthChanged(Health);
        }
		else
		{
			return;
		}
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
