using System;
using System.Diagnostics;
using Godot;

public partial class SwipeInput : Node
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public event Action<SwipeArgs> OnSwipe;
    private bool MouseButtonDownWaitingFlag = true;
    private bool MouseButtonUpWaitingFlag = false;
    public static SwipeInput Instance;
    public override void _Ready()
    {
        Instance = this;
        Debug.WriteLine("SwipeInput is Ready");
    }
    public override void _Process(float delta)
    {
        OnMouseInput();
        OnTouchInput();
    }
    private void OnTouchInput()
    {
       
    }

    private void OnMouseInput()
    {
        if (Input.IsMouseButtonPressed(1) && MouseButtonDownWaitingFlag == true)
        {
            startTouchPosition = GetViewport().GetMousePosition();
            MouseButtonUpWaitingFlag = true;
            MouseButtonDownWaitingFlag = false;
        }
        if (!Input.IsMouseButtonPressed(1) && MouseButtonUpWaitingFlag == true)
        {
            endTouchPosition = GetViewport().GetMousePosition();
            MouseButtonUpWaitingFlag = false;
            MouseButtonDownWaitingFlag = true;
            if (endTouchPosition.x > startTouchPosition.x)
            {
                OnRightSwipe();
            }
            if (endTouchPosition.x < startTouchPosition.x)
            {
                OnLeftSwipe();
            }
        }
    }

    private void OnRightSwipe()
    {
        OnSwipe.Invoke(new SwipeArgs(SwipeType.right));
    }
    private void OnLeftSwipe()
    {
        OnSwipe.Invoke(new SwipeArgs(SwipeType.left));

    }
    public class SwipeArgs
    {
        public SwipeType swipeType;
        public SwipeArgs(SwipeType swipeType)
        {
            this.swipeType = swipeType;
        }
        public bool IsLeftSwipe()
        {
            return swipeType == SwipeType.left;
        }
    }
    public enum SwipeType
    {
        left = 0,
        right = 1,
        upper,
        down
    }
    
}
public class InputSystem
{

}