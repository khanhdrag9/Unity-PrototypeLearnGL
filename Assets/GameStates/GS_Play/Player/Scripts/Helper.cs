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

    public static float AngleBetweenAngle(float mine, float target)
    {
        mine = NormalizeAngle(mine);
        target = NormalizeAngle(target);
        float sub = target - mine;
        if(Mathf.Abs(sub) < 180)
        {
            // return Mathf.Abs(sub);
            return sub;
        }
        else if(mine > 180)
        {
            return sub + 360;
        }
        else if(target > 180)
        {
            return sub - 360;
        }
        return 0;
    }

    public static float NormalizeAngle(float angle)
    {
        float result = (int)angle % 360;
        if(result < 0)result += 360; 
        return result;
    }

    public static float Sin(float angle)
    {
        return Mathf.Sin(angle * DegreesToRad);
    }

    public static float Cos(float angle)
    {
        return Mathf.Cos(angle * DegreesToRad);
    }

    public static bool RollRate(int rate)
    {
        return Random.Range(0, 101) <= rate;
    }

    public static float Distance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt((x1 - x2)*(x1 - x2) + (y1 - y2)*(y1 - y2));
    }
}
