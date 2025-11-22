using System;
using UnityEngine;
using static Math;

[Serializable]
public struct Float2
{
	public float x, y;

	public Float2(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public Float2 yx => new(y, x);

	public static Float2 Zero => new(0, 0);

	public static bool operator ==(Float2 a, Float2 b) => a.x == b.x && a.y == b.y;
	public static bool operator !=(Float2 a, Float2 b) => a.x != b.x || a.y != b.y;

	public static implicit operator Float3(Float2 a) => new(a.x, a.y, 0);

	public static implicit operator Float2(float a) => new(a, a);
	public static implicit operator Float2(Vector2 a) => new(a.x, a.y);
	public static implicit operator Vector2(Float2 a) => new(a.x, a.y);

	public static Float2 operator -(Float2 a) => new(-a.x, -a.y);

	public static Float2 operator +(Float2 a, Float2 b) => new(a.x + b.x, a.y + b.y);
	public static Float2 operator -(Float2 a, Float2 b) => new(a.x - b.x, a.y - b.y);
	public static Float2 operator *(Float2 a, Float2 b) => new(a.x * b.x, a.y * b.y);
	public static Float2 operator /(Float2 a, Float2 b) => new(a.x / b.x, a.y / b.y);

	public static Float2 operator *(Float2 a, float b) => new(a.x * b, a.y * b);
	public static Float2 operator *(float a, Float2 b) => new(a * b.x, a * b.y);
	public static Float2 operator *(Float2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);
	public static Float2 operator *(Vector2 a, Float2 b) => new(a.x * b.x, a.y * b.y);

	public static Float2 operator /(Float2 a, float b) => new(a.x / b, a.y / b);
	public static Float2 operator /(float a, Float2 b) => new(a / b.x, a / b.y);

	public override string ToString() => $"({x}, {y})";

	public readonly float SquareMagnitude => Dot(this);

	public readonly float RcpMagnitude => Rsqrt(SquareMagnitude);

	public readonly float Magnitude => Rcp(RcpMagnitude);

	public readonly Float2 Normalized => RcpMagnitude * this;

	public static Float2 DeltaAngle(Float2 a, Float2 b) => new(Math.DeltaAngle(a.x, b.x), Math.DeltaAngle(a.y, b.y));

	public static Float2 SpringDampVelocity(Float2 delta, Float2 velocity, float sqrtStiffness, float overshoot = 0)
	{
		return new(Math.SpringDampVelocity(delta.x, velocity.x, sqrtStiffness, overshoot), Math.SpringDampVelocity(delta.y, velocity.y, sqrtStiffness, overshoot));
	}

	public static Float2 SpringDamp(Float2 current, Float2 target, ref Float2 velocity, float sqrtStiffness, float overshoot = 0)
	{
		velocity = SpringDampVelocity(target - current, velocity, sqrtStiffness, overshoot);
		return current + velocity * Time.deltaTime;
	}

	public static Float2 SpringDampAngle(Float2 current, Float2 target, ref Float2 velocity, float sqrtStiffness, float overshoot = 0)
	{
		target = current + DeltaAngle(current, target);
		return SpringDamp(current, target, ref velocity, sqrtStiffness, overshoot);
	}

	public static float CMin(Float2 a) => Math.Min(a.x, a.y);
	public static float CMax(Float2 a) => Math.Max(a.x, a.y);

	public static Float2 Radians(Float2 a) => new(Math.Radians(a.x), Math.Radians(a.y));
	public static Float2 Lerp(Float2 a, Float2 b, float t) => new(Math.Lerp(a.x, b.x, t), Math.Lerp(a.y, b.y, t));
	
	public readonly float Dot(Float2 a) => x * a.x + y * a.y;

	public static Float2 Abs(Float2 a) => new(Math.Abs(a.x), Math.Abs(a.y));
	public static Float2 Square(Float2 a) => a * a;
	public static Float2 Sqrt(Float2 a) => new(Math.Sqrt(a.x), Math.Sqrt(a.y));

	public static float Angle(Float2 a, Float2 b)
	{
		var squareMagnitudeProduct = a.SquareMagnitude * b.SquareMagnitude;
		return squareMagnitudeProduct == 0 ? 0 : Acos(a.Dot(b) * Rsqrt(squareMagnitudeProduct));
	}

	public static Float2 ClampMagnitude(Float2 a, float maxMagnitude)
	{
		var squareMagnitude = a.SquareMagnitude;
		if (squareMagnitude <= Math.Square(maxMagnitude))
			return a;

		var rcpMagnitude = Rsqrt(squareMagnitude);
		return a * rcpMagnitude * maxMagnitude;
	}

	public static float Cross(Float2 a, Float2 b)
	{
		return a.x * b.y - a.y * b.x;
	}

	public static float Distance(Float2 p0, Float2 p1) => (p1 - p0).Magnitude;

	public static float SignedAngle(Float2 a, Float2 b) => Atan2(Cross(a, b), a.Dot(b));

	public static Float2 Min(Float2 min, Float2 a) => new(Math.Min(min.x, a.x), Math.Min(min.y, a.y));
	public static Float2 Max(Float2 max, Float2 a) => new(Math.Max(max.x, a.x), Math.Max(max.y, a.y));

	public static Float2 Ceil(Float2 a) => new(Math.Ceil(a.x), Math.Ceil(a.y));
	public static Float2 Floor(Float2 a) => new(Math.Floor(a.x), Math.Floor(a.y));

	public Float2 Clamp(Float2 min, Float2 max) => new(Math.Clamp(x, min.x, max.x), Math.Clamp(y, min.y, max.y));
}
