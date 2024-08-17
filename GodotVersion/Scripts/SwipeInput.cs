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
        //OnKeyBoardInput();
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
        {
            switch ((KeyList)keyEvent.Scancode)
            {
                case KeyList.O:
                    OnSwipe(new SwipeArgs(SwipeType.upper));
                    break;
                case KeyList.L:
                    OnSwipe(new SwipeArgs(SwipeType.down));
                    break;
                case KeyList.Y:
                    OnSwipe(new SwipeArgs(SwipeType.right));
                    break;
                case KeyList.A:
                    OnSwipe(new SwipeArgs(SwipeType.left));
                    break;


                case KeyList.W:
                    OnSwipe(new SwipeArgs(SwipeType.upper));
                    break;
                case KeyList.S:
                    OnSwipe(new SwipeArgs(SwipeType.down));
                    break;
                case KeyList.D:
                    OnSwipe(new SwipeArgs(SwipeType.right));
                    break;
                //case KeyList.A:
                //    OnSwipe(new SwipeArgs(SwipeType.left));
                //    break;
            }
        }
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

            SwipeType swipetype = GetSwipeType(startTouchPosition, endTouchPosition);
            OnSwipe.Invoke(new SwipeArgs(swipetype));
        }
    }
    private SwipeType GetSwipeType(Vector2 startTouchPosition,Vector2 endTouchPosition)
    {
        SwipeType swipeType;
        Vector2 dif = startTouchPosition - endTouchPosition;
        if(Mathf.Abs(dif.x) > Mathf.Abs(dif.y))
        {
            if(dif.x < 0)
            {
                swipeType = SwipeType.left;
            }
            else
            {
                swipeType = SwipeType.right;
            }
        }
        else
        {
            if (dif.y < 0)
            {
                swipeType = SwipeType.upper;
            }
            else
            {
                swipeType = SwipeType.down;
            }
        }
        return swipeType;
    }
    public enum SwipeType
    {
        left = 0,
        right = 1,
        upper = 2,
        down = 3
    }
    
}