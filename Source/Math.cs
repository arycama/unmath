using System;
using UnityEngine;

public static partial class Math
{
	public const float E = MathF.E;
	public const float Log2e = (float)1.44269504088896340736;

	public const float Pi = MathF.PI;
	public const float Tau = Pi * 2;
	public const float TwoPi = Tau;
	public const float FourPi = Pi * 4;
	public const float HalfPi = Pi * 0.5f;
	public const float QuarterPi = Pi * 0.25f;

	public static float Radians(float x) => Pi / 180 * x;
	public static float Degrees(float x) => 180 / Pi * x;

	// Simple math utils
	public static float Sign(float x) => x < 0 ? -1 : 1;
	public static float Abs(float x) => x < 0 ? -x : x;
	public static int Abs(int x) => x < 0 ? -x : x;
	public static float Min(float x, float y) => x <= y ? x : y;
	public static int Min(int x, int y) => x <= y ? x : y;
	public static float Max(float x, float y) => x >= y ? x : y;
	public static int Max(int x, int y) => x >= y ? x : y;
	public static float Clamp(float x, float min = 0, float max = 1) => x < min ? min : (x > max ? max : x);
	public static int Clamp(int x, int min = 0, int max = 1) => x < min ? min : (x > max ? max : x);
	public static float Saturate(float x) => Clamp(x);
	public static float Square(float x) => x * x;
	public static int Square(int x) => x * x;
	public static float Floor(float x) => MathF.Floor(x);
	public static int FloorToInt(float x) => (int)Floor(x);
	public static float Ceil(float x) => MathF.Ceiling(x);
	public static int CeilToInt(float x) => (int)Ceil(x);
	public static float Round(float x) => MathF.Round(x);
	public static int RoundToInt(float x) => (int)Round(x);
	public static float Snap(float value, float cellSize) => Floor(value / cellSize) * cellSize;

	public static float DeltaAngle(float current, float target)
	{
		var angle = WrapAngle(target - current);
		return angle <= Pi ? angle : angle - TwoPi;
	}

	public static float Flip(float x, bool flip) => flip ? -x : x;
	public static float SignFlip(float x, float sign) => Flip(x, sign < 0);

	// Trig/inv trig
	public static void SinCos(float theta, out float sinTheta, out float cosTheta)
	{
		sinTheta = Sin(theta);
		cosTheta = Cos(theta);
	}

	public static float Sin(float x) => MathF.Sin(x);
	public static float Cos(float x) => MathF.Cos(x);
	public static float Tan(float x) => MathF.Tan(x);

	public static float Asin(float x) => MathF.Asin(x);
	public static float Acos(float x) => MathF.Acos(x);
	public static float Atan(float x) => MathF.Atan(x);
	public static float Atan2(float y, float x) => MathF.Atan2(y, x);

	public static float SinFromCos(float x) => Sqrt(1 - Square(x));

	// Computes sin(thetaA + thetaB)
	public static float SineAddition(float cosA, float sinA, float cosB, float sinB)
	{
		return sinA * cosB + cosA * sinB;
	}

	// Computes sin(thetaA + thetaB)
	public static float SineAddition(float cosA, float cosB)
	{
		return SineAddition(cosA, SinFromCos(cosA), cosB, SinFromCos(cosB));
	}

	// Computes sin(thetaA - thetaB)
	public static float SineDifference(float cosA, float sinA, float cosB, float sinB)
	{
		return sinA * cosB - cosA * sinB;
	}

	// Computes sin(thetaA - thetaB)
	public static float SineDifference(float cosA, float cosB)
	{
		return SineDifference(cosA, SinFromCos(cosA), cosB, SinFromCos(cosB));
	}

	// Computes cos(thetaA + thetaB)
	public static float CosineAddition(float cosA, float sinA, float cosB, float sinB)
	{
		return cosA * cosB - sinA * sinB;
	}

	// Computes cos(thetaA + thetaB)
	public static float CosineAddition(float cosA, float cosB)
	{
		return CosineAddition(cosA, SinFromCos(cosA), cosB, SinFromCos(cosB));
	}

	// Computes cos(thetaA - thetaB)
	public static float CosineDifference(float cosA, float sinA, float cosB, float sinB)
	{
		return cosA * cosB + sinA * sinB;
	}

	// Computes cos(thetaA - thetaB)
	public static float CosineDifference(float cosA, float cosB)
	{
		return CosineDifference(cosA, SinFromCos(cosA), cosB, SinFromCos(cosB));
	}

	// Transcendental etc
	public static float Rcp(float x) => 1.0f / x;
	public static float Sqrt(float x) => MathF.Sqrt(x);
	public static float Rsqrt(float x) => Rcp(Sqrt(x));

	public static float Log(float x) => MathF.Log(x);
	public static float Log2(float x) => MathF.Log(x, 2);
	public static float Log10(float x) => MathF.Log10(x);

	public static float Exp(float x) => MathF.Exp(x);
	public static float Exp2(float x) => Exp(x * Log(2));
	public static float Exp10(float x) => Exp(x * Log(10));

	public static float Pow(float x, float y) => MathF.Pow(x, y);

	public static float Mod(float x, float y)
	{
		var remainder = x % y;
		return remainder >= 0 ? remainder : remainder + y;
	}

	public static float WrapAngle(float x) => Mod(x, TwoPi);

	// Utility
	public static float Lerp(float a, float b, float t) => a + t * (b - a);
	public static float InvLerp(float t, float a, float b) => (t - a) * Rcp(b - a);
	public static float RemapClamped(float x, float prevMin, float prevMax, float newMin, float newMax) => Lerp(newMin, newMax, Saturate(InvLerp(x, prevMin, prevMax)));
	public static float Damp(float a, float b, float t, float dt) => Lerp(a, b, 1 - Exp(-t * dt));
	public static float Damp(float a, float b, float t) => Damp(a, b, t, Time.deltaTime);

	public static Float2 RemapScaleOffset(float prevMin, float prevMax, float newMin, float newMax)
	{
		var scale = Rcp(prevMax - prevMin) * (newMax - newMin);
		var offset = newMin - prevMin * scale;
		return new Float2(scale, offset);
	}

	public static float Remap(float x, float prevMin, float prevMax, float newMin, float newMax)
	{
		var scaleOffset = RemapScaleOffset(prevMin, prevMax, newMin, newMax);
		return x * scaleOffset.x + scaleOffset.y;
	}

	public static bool Quadratic(float a, float b, float c, out float r0, out float r1)
	{
		var discriminant = Square(b) - 4 * a * c;
		var sqrtDiscriminant = Sqrt(discriminant);
		r0 = (-b - sqrtDiscriminant) / (2 * a);
		r1 = (-b + sqrtDiscriminant) / (2 * a);
		return discriminant >= 0;
	}

	// Increments A towards B by speed without overshooting
	public static float MoveTowards(float a, float b, float speed)
	{
		var delta = b - a;
		return Abs(delta) <= speed ? b : a + Sign(delta) * speed;
	}

	public static float MoveTowardsAngle(float a, float b, float speed)
	{
		float num = DeltaAngle(a, b);
		if (0f - speed < num && num < speed)
		{
			return b;
		}

		b = a + num;
		return MoveTowards(a, b, speed);
	}

	// Vector/matrix
	public static float Magnitude(float a) => Abs(a);

	public static float SpringDampVelocity(float delta, float velocity, float sqrtStiffness, float overshoot = 0)
	{
		var dt = Time.deltaTime;
		var k = Square(sqrtStiffness);
		var c = 2f * (1 - overshoot) * sqrtStiffness;
		return (velocity + k * delta * dt) / (1 + c * dt + k * Square(dt));
	}

	public static float SpringDampVelocity(float current, float target, float velocity, float sqrtStiffness, float overshoot = 0)
	{
		return SpringDampVelocity(target - current, velocity, sqrtStiffness, overshoot);
	}

	public static float SpringDamp(float current, float target, ref float velocity, float sqrtStiffness, float overshoot = 0)
	{
		velocity = SpringDampVelocity(current, target, velocity, sqrtStiffness, overshoot);
		return current + velocity * Time.deltaTime;
	}

	public static float SpringDampAngle(float current, float target, ref float velocity, float sqrtStiffness, float overshoot = 0)
	{
		target = current + DeltaAngle(current, target);
		return SpringDamp(current, target, ref velocity, sqrtStiffness, overshoot);
	}

	// Todo: Move to appropriate classes
	public static Float2 Mul(Float2x2 m, Float2 p)
	{
		return p.x * m.c0 + p.y * m.c1;
	}

	public static Float2 Rotate(Float2 p, float theta)
	{
		SinCos(theta, out var sinTheta, out var cosTheta);
		var matrix = new Float2x2(cosTheta, sinTheta, -sinTheta, cosTheta);
		return Mul(matrix, p);
	}

	public static Float3 ClosestPointOnLine(Float3 linePoint, Float3 lineDirection, Float3 point)
	{
		var AP = point - linePoint;
		var t = Float3.Dot(AP, lineDirection); // If lineDir is normalized, If lineDir is not normalized: t = Float3.Dot(AP, lineDir) / lineDir.sqrMagnitude;
		var closestPoint = linePoint + t * lineDirection;
		return closestPoint;
	}

	public static float AngularDiameterToConeCosAngle(float angularDiameter) => Cos(0.5f * angularDiameter);

	public static float AngularDiameterToSolidAngle(float angularDiameter) => TwoPi * (1 - AngularDiameterToConeCosAngle(angularDiameter));

	public static float ConeCosAngleToSolidAngle(float coneCosAngle) => TwoPi * (1 - coneCosAngle);
}
