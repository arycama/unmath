using System;
using System.ComponentModel;
using UnityEngine;
using static Math;

[Serializable]
public struct Float3 : IEquatable<Float3>
{
	public float x, y, z;

	public Float3(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Float3(Float2 xy, float z) : this(xy.x, xy.y, z) { }
	public Float3(float x, Float2 yz) : this(x, yz.x, yz.y) { }

	public float this[int index]
	{
		readonly get => index switch
		{
			0 => x,
			1 => y,
			2 => z,
			_ => throw new ArgumentOutOfRangeException(),
		};
		set
		{
			switch (index)
			{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				case 2:
					z = value;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public static Float3 Zero => new(0, 0, 0);
	public static Float3 One => new(1, 1, 1);

	public static Float3 Right => new(1, 0, 0);
	public static Float3 Left => new(-1, 0, 0);
	public static Float3 Up => new(0, 1, 0);
	public static Float3 Down => new(0, -1, 0);
	public static Float3 Forward => new(0, 0, 1);
	public static Float3 Back => new(0, 0, -1);

	public static implicit operator Float3(Vector3 a) => new(a.x, a.y, a.z);

	public static implicit operator Vector3(Float3 a) => new(a.x, a.y, a.z);

	public static implicit operator Float4(Float3 a) => new(a.x, a.y, a.z, 0);

	public static explicit operator Float3(Vector4 a) => new(a.x, a.y, a.z);

	public static Float3 operator +(Float3 a) => new(a.x, a.y, a.z);
	public static Float3 operator -(Float3 a) => new(-a.x, -a.y, -a.z);

	public static Float3 operator *(float a, Float3 b) => new(a * b.x, a * b.y, a * b.z);
	public static Float3 operator *(Float3 a, float b) => b * a;

	public static Float3 operator /(float a, Float3 b) => new(a / b.x, a / b.y, a / b.z);
	public static Float3 operator /(Float3 a, float b) => new(a.x / b, a.y / b, a.z / b);

	public static Float3 operator +(Float3 a, Float3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
	public static Float3 operator -(Float3 a, Float3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
	public static Float3 operator *(Float3 a, Float3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
	public static Float3 operator /(Float3 a, Float3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);

	public static Float3 operator +(Vector3 a, Float3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
	public static Float3 operator -(Vector3 a, Float3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
	public static Float3 operator *(Vector3 a, Float3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
	public static Float3 operator /(Vector3 a, Float3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);

	public static Float3 operator +(Float3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
	public static Float3 operator -(Float3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
	public static Float3 operator *(Float3 a, Vector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
	public static Float3 operator /(Float3 a, Vector3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);

	public static Bool3 operator <(Float3 a, int b) => new(a.x < b, a.y < b, a.z < b);
	public static Bool3 operator >(Float3 a, int b) => new(a.x > b, a.y > b, a.z > b);

	public static bool operator ==(Float3 a, Float3 b) => a.x == b.x && a.y == b.y && a.z == b.z;
	public static bool operator !=(Float3 a, Float3 b) => a.x != b.x || a.y != b.y || a.z != b.z;

	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 xx => new(x, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 xy => new(x, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 xz => new(x, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 yx => new(y, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 yy => new(y, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 yz => new(y, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 zx => new(z, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 zy => new(z, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float2 zz => new(z, z);

	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xxx => new(x, x, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xxy => new(x, x, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xxz => new(x, x, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xyx => new(x, y, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xyy => new(x, y, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xyz => new(x, y, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xzx => new(x, z, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xzy => new(x, z, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 xzz => new(x, z, z);

	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yxx => new(y, x, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yxy => new(y, x, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yxz => new(y, x, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yyx => new(y, y, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yyy => new(y, y, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yyz => new(y, y, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yzx => new(y, z, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yzy => new(y, z, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 yzz => new(y, z, z);

	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zxx => new(z, x, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zxy => new(z, x, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zxz => new(z, x, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zyx => new(z, y, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zyy => new(z, y, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zyz => new(z, y, z);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zzx => new(z, z, x);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zzy => new(z, z, y);
	[EditorBrowsable(EditorBrowsableState.Never)] public Float3 zzz => new(z, z, z);

	public static Float3 PositiveInfinity => new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

	public override readonly string ToString() => $"({x}, {y}, {z})";

	// Utility
	//public static Float3 Select(Bool3 c, Float3 a, Float3 b) => Float3(c.x ? a.x : b.x, c.y ? a.y : b.y, c.z ? a.z : b.z);

	// Common math
	public static Float3 Abs(Float3 a) => new(Math.Abs(a.x), Math.Abs(a.y), Math.Abs(a.z));
	public static Float3 Square(Float3 a) => a * a;
	public static Float3 Sqrt(Float3 a) => new(Math.Sqrt(a.x), Math.Sqrt(a.y), Math.Sqrt(a.z));
	public static Float3 Lerp(Float3 a, Float3 b, float t) => new(Math.Lerp(a.x, b.x, t), Math.Lerp(a.y, b.y, t), Math.Lerp(a.z, b.z, t));
	public static Float3 Cross(Float3 a, Float3 b) => a.yzx * b.zxy - a.zxy * b.yzx;

	public static float Angle(Float3 a, Float3 b)
	{
		var squareMagnitudeProduct = SquareMagnitude(a) * SquareMagnitude(b);
		return squareMagnitudeProduct == 0 ? 0 : Acos(Dot(a, b) * Rsqrt(squareMagnitudeProduct));
	}

	public static float SignedAngle(Float3 a, Float3 b, Float3 axis) => Angle(a, b) * Sign(Dot(axis, Cross(a, b)));

	public static float Dot(Float3 a, Float3 b) => a.x * b.x + a.y * b.y + a.z * b.z;

	public static float SquareMagnitude(Float3 a) => Dot(a, a);

	public static float Magnitude(Float3 a) => Math.Sqrt(SquareMagnitude(a));

	public static float Distance(Float3 a, Float3 b) => Magnitude(b - a);

	public static Float3 Normalize(Float3 a) => a * Rsqrt(SquareMagnitude(a));
	public static Float3 FromTo(Float3 a, Float3 b) => Normalize(b - a);

	public static Float3 ClampMagnitude(Float3 a, float maxMagnitude)
	{
		var squareMagnitude = SquareMagnitude(a);
		if (squareMagnitude <= Math.Square(maxMagnitude))
			return a;

		var rcpMagnitude = Rsqrt(squareMagnitude);
		return a * rcpMagnitude * maxMagnitude;
	}

	public static float CosAngle(Float3 a, Float3 b)
	{
		return Dot(a, b) * Rsqrt(SquareMagnitude(a) * SquareMagnitude(b));
	}

	// Projects a vector onto another vector (Assumes vectors are normalized)
	public static Float3 Project(Float3 a, Float3 b)
	{
		return Dot(a, b) * b;
	}

	// Projects a vector onto a plane defined by a normal orthongal to the plane (Assumes vectors are normalized)
	public static Float3 ProjectOnPlane(Float3 a, Float3 b)
	{
		return a - Project(a, b);
	}

	public static void SinCos(Float3 a, out Float3 sinA, out Float3 cosA)
	{
		Math.SinCos(a.x, out sinA.x, out cosA.x);
		Math.SinCos(a.y, out sinA.y, out cosA.y);
		Math.SinCos(a.z, out sinA.z, out cosA.z);
	}

	public static Float3 DeltaAngle(Float3 a, Float3 b) => new(Float2.DeltaAngle(a.xy, b.xy), Math.DeltaAngle(a.z, b.z));

	public static Float3 Radians(Float3 a) => new(Math.Radians(a.x), Math.Radians(a.y), Math.Radians(a.z));

	public static Float3 Degrees(Float3 a) => new(Math.Degrees(a.x), Math.Degrees(a.y), Math.Degrees(a.z));

	public static Float3 SpringDampVelocity(Float3 delta, Float3 velocity, float sqrtStiffness, float overshoot = 0)
	{
		return new(Float2.SpringDampVelocity(delta.xy, velocity.xy, sqrtStiffness, overshoot), Math.SpringDampVelocity(delta.z, velocity.z, sqrtStiffness, overshoot));
	}

	public static Float3 SpringDamp(Float3 current, Float3 target, ref Float3 velocity, float sqrtStiffness, float overshoot = 0)
	{
		velocity = SpringDampVelocity(target - current, velocity, sqrtStiffness, overshoot);
		return current + velocity * Time.deltaTime;
	}

	public static Float3 SpringDampAngle(Float3 current, Float3 target, ref Float3 velocity, float sqrtStiffness, float overshoot = 0)
	{
		target = current + DeltaAngle(current, target);
		return SpringDamp(current, target, ref velocity, sqrtStiffness, overshoot);
	}

	public static Float3 SlerpNormalized(Float3 a, Float3 b, float t)
	{
		// Calculate angle between vectors
		var dot = Dot(a, b);
		var theta = Acos(dot) * t;
		var relativeVec = Normalize(b - a * dot);
		return a * Cos(theta) + relativeVec * Sin(theta);

		// Alternate, not sure which is faster.
		//float dot = Dot(a, b);
		//float theta = Atan2(Math.Sqrt(1f - dot * dot), dot);
		//float sinTheta = Sin(theta);
		//float wa = Sin((1f - t) * theta) / sinTheta;
		//float wb = Sin(t * theta) / sinTheta;
		//return (a * wa + b * wb);
	}

	public static Float3 Min(Float3 a, Float3 b) => new(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));

	public static Float3 Max(Float3 a, Float3 b) => new(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));

	public override bool Equals(object obj) => obj is Float3 @float && Equals(@float);
	public bool Equals(Float3 other) => x == other.x && y == other.y && z == other.z;
	public override int GetHashCode() => HashCode.Combine(x, y, z);

	public readonly void Deconstruct(out float x, out float y, out float z)
	{
		x = this.x; y = this.y; z = this.z;
	}
}
