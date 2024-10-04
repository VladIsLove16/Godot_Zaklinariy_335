using Godot;
public class TouchHandler : Node
{
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch screenTouchEvent)
        {
            if (screenTouchEvent.Pressed)
            {
                // Начало касания
                GD.Print($"Screen touched at: {screenTouchEvent.Position}");
            }
            else
            {
                // Окончание касания
                GD.Print($"Screen touch released at: {screenTouchEvent.Position}");
            }
        }

        //if (@event is InputEventScreenDrag screenDragEvent)
        //{
        //    // Перетаскивание
        //    GD.Print($"Screen drag at: {screenDragEvent.Position}");
        //}
    }
}