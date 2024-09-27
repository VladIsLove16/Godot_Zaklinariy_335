//using System;
//using Godot;

//public class SwipeDetector : Node2D
//{
//    private Vector2 _startTouchPosition;
//    private float _lastTapTime;
//    private const float DoubleTapThreshold = 300f; // миллисекунды

//    public override void _Input(InputEvent @event)
//    {
//        if (@event is InputEventScreenTouch touchEvent)
//        {
//            if (touchEvent.Pressed)
//            {
//                // Обработка начала касания
//                _startTouchPosition = touchEvent.Position;
//                _lastTapTime = OS.GetTicksMsec(); // Получаем текущее время
//            }
//            else
//            {
//                // Обработка завершения касания
//                DetectSwipe(touchEvent.Position);
//            }
//        }
//        else if (@event is InputEventScreenTouch doubleTapEvent && doubleTapEvent.Pressed)
//        {
//            // Проверка на двойное касание
//            if (OS.GetTicksMsec() - _lastTapTime < DoubleTapThreshold)
//            {
//                GD.Print("Двойное касание");
//            }
//        }
//    }

//    private void DetectSwipe(Vector2 endTouchPosition)
//    {
//        Vector2 swipeVector = endTouchPosition - _startTouchPosition;

//        if (swipeVector.Length() > 50) // минимальная длина свайпа
//        {
//            // Определяем сторону свайпа
//            if (Math.Abs(swipeVector.x) > Math.Abs(swipeVector.y))
//            {
//                if (swipeVector.x > 0)
//                    GD.Print("Свайп вправо");
//                else
//                    GD.Print("Свайп влево");
//            }
//            else
//            {
//                if (swipeVector.y > 0)
//                    GD.Print("Свайп вниз");
//                else
//                    GD.Print("Свайп вверх");
//            }
//        }
//    }
//}
