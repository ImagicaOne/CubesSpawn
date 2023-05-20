using UnityEngine;

public static class Calculator
{
    private static float _speed = 8;
    public static float CalculateInitialSpeed(Vector2 direction)
    {
        Vector2 initialVectorSpeed = CalculateInitialSpeedVector(direction);
        float initialSpeed = Mathf.Sqrt(initialVectorSpeed.x * initialVectorSpeed.x + initialVectorSpeed.y * initialVectorSpeed.y);
        return initialSpeed;
    }

    public static Vector2 CalculateInitialSpeedVector(Vector2 direction)
    {
        Vector2 initialVectorSpeed = new Vector2(direction.x * _speed, direction.y * _speed);
        return initialVectorSpeed;
    }
}
