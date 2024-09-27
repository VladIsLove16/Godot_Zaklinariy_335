using System;
using System.Diagnostics;
using Godot;
using static SwipeInput;

public partial class SwipeInput : Node
{
    private const int SWIPE_MINDISTANCE = 50;
    private const float doubleTapDelayThreshold = 0.2f;
    private bool doubleTapFlag;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public event Action<SwipeArgs> OnInput;
    private bool MouseButtonDownWaitingFlag = true;
    private bool MouseButtonUpWaitingFlag = false;
    public static SwipeInput Instance;
    public bool IsTouching;
    private SwipeType lastSwipeType;
    public DateTime lastTimeSwiped { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        Debug.WriteLine("SwipeInput is Ready");
    }
    public override void _Process(float delta)
    {
        if (!GamePause.IsGamePaused)
        {
            OnMouseInput();
            OnTouchInput();
        }
        //OnKeyBoardInput();
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
        {
            switch ((KeyList)keyEvent.Scancode)
            {
                case KeyList.O:
                    OnInput(new SwipeArgs(SwipeType.down));
                    break;
                case KeyList.L:
                    OnInput(new SwipeArgs(SwipeType.right));
                    break;
                case KeyList.Y:
                    OnInput(new SwipeArgs(SwipeType.upper));
                    break;
                case KeyList.A:
                    OnInput(new SwipeArgs(SwipeType.left));
                    break;


                case KeyList.W:
                    OnInput(new SwipeArgs(SwipeType.upper));
                    break;
                case KeyList.S:
                    OnInput(new SwipeArgs(SwipeType.down));
                    break;
                case KeyList.D:
                    OnInput(new SwipeArgs(SwipeType.right));
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


            Vector2 dif = startTouchPosition - endTouchPosition;
            SwipeType swipeType = GetSwipeType(startTouchPosition, endTouchPosition);
            SwipeArgs args = new SwipeArgs(swipeType);

            if (DoubleTapCheck(swipeType))
            {
                args.isDoubleTap = true;
                Debug.Print("DoubleTap!!");
            }
            else
                Debug.Print("Tap!");
            OnInput.Invoke(args);
            lastSwipeType = swipeType;
            lastTimeSwiped = DateTime.Now;
        }
    }

    private bool DoubleTapCheck(SwipeType swipeType)
    {
        //Debug.Print("DoubleTapCHECK" + lastTimeSwiped + "  " + DateTime.Now);
        //Debug.Print( (DateTime.Now - lastTimeSwiped).TotalSeconds.ToString());
        //Debug.Print("swipeType" + swipeType);
        if (swipeType != SwipeType.tap)
            return false;
        if (lastSwipeType != SwipeType.tap)
            return false;
        if (doubleTapFlag == false || (DateTime.Now - lastTimeSwiped).TotalSeconds > doubleTapDelayThreshold)
        {

            //Debug.Print("doubleTapFlag == false || (DateTime.Now - lastTimeSwiped).TotalSeconds > doubleTapDelayThreshold");
            doubleTapFlag = true;
            return false;
        }
        else
        {
            doubleTapFlag = false;
            return true;
        }
    }

    private SwipeType GetSwipeType(Vector2 startTouchPosition,Vector2 endTouchPosition)
    {
        
        SwipeType swipeType;
        Vector2 dif = startTouchPosition - endTouchPosition;
        if(dif.Length() > SWIPE_MINDISTANCE)
            if (Mathf.Abs(dif.x) > Mathf.Abs(dif.y))
            {
                if(dif.x < 0)
                {
                    swipeType = SwipeType.right;
                }
                else
                {
                    swipeType = SwipeType.left;
                }
            }
            else
            {
                if (dif.y < 0)
                {
                    swipeType = SwipeType.down;
                }
                else
                {
                    swipeType = SwipeType.upper;
                }
            }
        else
            swipeType = SwipeType.tap;
        return swipeType;

    }
    public enum SwipeType
    {
        left = 0,
        right = 1,
        upper = 2,
        down = 3,
        tap = 4,
        none = 5
    }

}
