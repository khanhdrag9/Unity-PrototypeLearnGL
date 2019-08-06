using UnityEngine;
public class Helper
{
    public static float RadToDegrees = 57.2957795131f;
    public static float DegreesToRad = 0.01745329251f;
    public static float Clamp(float value, float min, float max)
    {
        if(value < min)value = min;
        else if(value > max)value = max;
        return value;
    }

    public static float AngleFromToBy90D(float fromX, float fromY, float toX, float toY)
    {
        float angle = 0;
        float x = fromX - toX;
        float y = toY - fromY;
        angle = Mathf.Atan2(x, y) *  RadToDegrees;
        if(angle < 0)angle += 360;

        return angle;
    }

    public static float AngleFromToBy0D(float fromX, float fromY, float toX, float toY)
    {
        float angle = 0;
        float x = toX - fromX;
        float y = toY - fromY;
        angle = Mathf.Atan2(y, x) *  RadToDegrees;

        return angle;
    }

    public static float Sin(float angle)
    {
        return Mathf.Sin(angle * DegreesToRad);
    }

    public static float Cos(float angle)
    {
        return Mathf.Cos(angle * DegreesToRad);
    }
}
