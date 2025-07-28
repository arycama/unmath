using UnityEngine;
using static Math;

public struct Float4
{
	public float x, y, z, w;

	public Float4(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	public Float4(Float2 xy, Float2 zw) : this(xy.x, xy.y, zw.x, zw.y) { }

	public Float4(Float3 xyz, float w) : this(xyz.x, xyz.y, xyz.z, w) { }

	public Float3 xyz => new(x, y, z);

	public Float4 wwww => new(w, w, w, w);
	public Float4 yzxw => new(y, z, x, w);

	public static Float4 operator +(Float4 a, Float4 b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
	public static Float4 operator *(float a, Float4 b) => new(a * b.x, a * b.y, a * b.z, a * b.w);
	public static Float4 operator *(Float4 a, float b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
	public static Float4 operator *(Float4 a, Float4 b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);

	public static implicit operator Vector4(Float4 a) => new(a.x, a.y, a.z, a.w);

	public static implicit operator Float4(Vector4 a) => new(a.x, a.y, a.z, a.w);

	public static float Csum(Float4 a) => (a.x + a.y) + (a.z + a.w);

	public Float4 PerspectiveDivide() => new(xyz * Rcp(w), w);
}