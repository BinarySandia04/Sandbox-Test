using Godot;
using System;

public class Utils
{
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(Lerp(a.x, b.x, t), Lerp(a.y, b.y, t), Lerp(a.z, b.z, t));
    }

    public static float Lerp(float a, float b, float t)
    {
        return (1 - t) * a + t * b;
    }
}
