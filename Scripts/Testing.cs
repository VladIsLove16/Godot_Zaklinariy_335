using Godot;
using System;
using System.Diagnostics;

public partial class Testing : Node
{
	public override void _Ready()
	{
		Debug.WriteLine("Testing is Ready");
		SwipeInput.Instance.OnSwipe += SwipeInput_Instance_OnSwipe;
	}
	private void SwipeInput_Instance_OnSwipe(SwipeInput.SwipeArgs swipeArgs)
	{
		if(swipeArgs.swipeType == SwipeInput.SwipeType.left)
		{
			Debug.WriteLine("left swipe");
		}
		else
			Debug.WriteLine("Right swipe");
	}
}