using System;
using Godot;

public partial class Character : Spatial
{
	[Export]
	public int Health;
	public event Action OnGetDamage;
    [Export]
    public bool candie=false;

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
		if(candie)
		{

            EventBus.instance.RaiseOn_Character_Died();
            AudioManager.Instance.PlaySound(AudioManager.SoundType.PlayerDie);
        }
	}
}
