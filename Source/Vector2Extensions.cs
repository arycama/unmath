using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 XX(this Vector2 v) => new(v.x, v.x);
    public static Vector2 XY(this Vector2 v) => new(v.x, v.y);
    public static Vector2 YX(this Vector2 v) => new(v.y, v.x);
    public static Vector2 YY(this Vector2 v) => new(v.y, v.y);

    public static Vector2 X0(this Vector2 v) => new(0, v.y);
    public static Vector2 Y0(this Vector2 v) => new(v.x, 0);

    public static Vector2 WithX(this Vector2 v, float x) => new(x, v.y);
    public static Vector2 WithY(this Vector2 v, float y) => new(v.x, y);

    public static void SetX(ref this Vector2 v, float x) => v.x = x;
    public static void SetY(ref this Vector2 v, float y) => v.y = y;
}
