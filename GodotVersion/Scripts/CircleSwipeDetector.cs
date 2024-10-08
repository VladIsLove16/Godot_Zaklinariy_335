using Godot;
using System.Collections.Generic;

public class CircleSwipeDetector : Node
{
    [Export]
    public float MinimumSwipeRadius = 50f;   // Минимальный радиус для определения свайпа как круга
    [Export]
    public float MaximumSwipeRadius = 200f;  // Максимальный радиус для определения свайпа как круга
    [Export]
    public float AngleTolerance = 20f;       // Допустимое отклонение от угла круга

    private Vector2 _startPoint;
    private List<Vector2> _points = new List<Vector2>();
    private bool _isSwiping = false;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch eventTouch && eventTouch.Pressed)
        {
            // Начало свайпа
            _startPoint = eventTouch.Position;
            _points.Clear();
            _points.Add(_startPoint);
            _isSwiping = true;
        }

        if (@event is InputEventScreenDrag eventDrag && _isSwiping)
        {
            // Добавляем точки в список при каждом движении пальца
            Vector2 currentPoint = eventDrag.Position;
            _points.Add(currentPoint);

            // Проверяем, является ли жест круговым свайпом
            if (IsCircleSwipe(_points))
            {
                GD.Print("Круговой свайп обнаружен!");
                _isSwiping = false;
                _points.Clear(); // Сбрасываем после обнаружения
            }
        }

        if (@event is InputEventScreenTouch eventTouchEnd && !eventTouchEnd.Pressed)
        {
            // Конец свайпа
            _isSwiping = false;
        }
    }

    bool IsCircleSwipe(List<Vector2> points)
    {
        if (points.Count < 10)
            return false;

        Vector2 center = _startPoint;
        float radiusSum = 0;
        int validPointCount = 0;

        // Проходим по точкам и проверяем, формируют ли они круг
        for (int i = 0; i < points.Count; i++)
        {
            float distance = center.DistanceTo(points[i]);

            // Убедимся, что точки находятся в пределах радиуса
            if (distance > MinimumSwipeRadius && distance < MaximumSwipeRadius)
            {
                radiusSum += distance;
                validPointCount++;
            }
        }

        // Недостаточно точек для корректного определения круга
        if (validPointCount < 5)
            return false;

        float averageRadius = radiusSum / validPointCount;

        // Проверяем углы между точками, чтобы убедиться, что это круг
        float totalAngle = 0;
        for (int i = 1; i < points.Count - 1; i++)
        {
            Vector2 a = points[i - 1] - center;
            Vector2 b = points[i + 1] - center;
            float angle = Mathf.Rad2Deg(a.AngleTo(b)); // Перевод в градусы
            totalAngle += angle;
        }

        return Mathf.Abs(totalAngle - 360f) <= AngleTolerance;
    }
}
