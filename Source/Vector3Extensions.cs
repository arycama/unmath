using UnityEngine;

public static class Vector3Extensions
{
    public static Float2 XY(this Vector3 v) => new(v.x, v.y);
    public static Float2 XZ(this Vector3 v) => new(v.x, v.z);

	public static Float3 X0(this Vector3 v) => new(0, v.y, v.z);
    public static Float3 Y0(this Vector3 v) => new(v.x, 0, v.z);
    public static Float3 Z0(this Vector3 v) => new(v.x, v.y, 0);

    public static Float3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);
    public static Float3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);
    public static Float3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);
}
