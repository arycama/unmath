using System;
using System.Runtime.CompilerServices;
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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Radians(float x) => Pi / 180 * x;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Degrees(float x) => 180 / Pi * x;

	// Simple math utils
	public static float Sign(float x) => x < 0 ? -1 : 1;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Abs(float x) => x < 0 ? -x : x;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Abs(int x) => x < 0 ? -x : x;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Min(float x, float y) => x <= y ? x : y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Min(int x, int y) => x <= y ? x : y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Max(float x, float y) => x >= y ? x : y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Max(int x, int y) => x >= y ? x : y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Clamp(float x, float min = 0, float max = 1) => x < min ? min : (x > max ? max : x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Clamp(int x, int min = 0, int max = 1) => x < min ? min : (x > max ? max : x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Saturate(float x) => Clamp(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Square(float x) => x * x;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Sq(float x) => Square(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Square(int x) => x * x;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Sq(int x) => Square(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Floor(float x) => MathF.Floor(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int FloorToInt(float x) => (int)Floor(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Ceil(float x) => MathF.Ceiling(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int CeilToInt(float x) => (int)Ceil(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Round(float x) => MathF.Round(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int RoundToInt(float x) => (int)Round(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Snap(float value, float cellSize) => Floor(value / cellSize) * cellSize;


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DeltaAngle(float current, float target)
	{
		var angle = WrapAngle(target - current);
		return angle <= Pi ? angle : angle - TwoPi;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Flip(float x, bool flip) => flip ? -x : x;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float SignFlip(float x, float sign) => Flip(x, sign < 0);

	// Trig/inv trig

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SinCos(float theta, out float sinTheta, out float cosTheta)
	{
		sinTheta = Sin(theta);
		cosTheta = Cos(theta);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Sin(float x) => MathF.Sin(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Cos(float x) => MathF.Cos(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Tan(float x) => MathF.Tan(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Asin(float x) => MathF.Asin(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Acos(float x) => MathF.Acos(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Atan(float x) => MathF.Atan(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Atan2(float y, float x) => MathF.Atan2(y, x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Rcp(float x) => 1.0f / x;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Sqrt(float x) => MathF.Sqrt(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Rsqrt(float x) => Rcp(Sqrt(x));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Log(float x) => MathF.Log(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Log2(float x) => MathF.Log(x, 2);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Log10(float x) => MathF.Log10(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Exp(float x) => MathF.Exp(x);

	[MethodImpl(MethodImplOptions.AggressiveInlining)] 
	public static float Exp2(float x) => Exp(x * Log(2));

	[MethodImpl(MethodImplOptions.AggressiveInlining)] 
	public static float Exp10(float x) => Exp(x * Log(10));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Pow(float x, float y) => MathF.Pow(x, y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Mod(float x, float y)
	{
		var remainder = x % y;
		return remainder < 0.0f ? remainder + y : remainder;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Mod(int x, int y)
	{
		var remainder = x % y;
		return remainder < 0 ? remainder + y : remainder;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float WrapAngle(float x) => Mod(x, TwoPi);

	// Utility
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Lerp(float a, float b, float t) => a + t * (b - a);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float InvLerp(float t, float a, float b) => (t - a) * Rcp(b - a);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float RemapClamped(float x, float prevMin, float prevMax, float newMin, float newMax) => Lerp(newMin, newMax, Saturate(InvLerp(x, prevMin, prevMax)));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Damp(float a, float b, float t, float dt) => Lerp(a, b, 1 - Exp(-t * dt));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
		var t = AP.Dot(lineDirection); // If lineDir is normalized, If lineDir is not normalized: t = Float3.Dot(AP, lineDir) / lineDir.sqrMagnitude;
		var closestPoint = linePoint + t * lineDirection;
		return closestPoint;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float AngularDiameterToConeCosAngle(float angularDiameter) => Cos(0.5f * angularDiameter);

	public static float AngularDiameterToSolidAngle(float angularDiameter) => TwoPi * (1 - AngularDiameterToConeCosAngle(angularDiameter));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float ConeCosAngleToSolidAngle(float coneCosAngle) => TwoPi * (1 - coneCosAngle);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int BitPack(int data, int size, int offset)
	{
		return (data & ((1 << size) - 1)) << offset;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int BitUnpack(int data, int size, int offset)
	{
		return (data >> offset) & ((1 << size) - 1);
	}

	// Unit conversions (TODO: Put in seperate Include?)
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float CentimeterToMeter(float a)
	{
		return a / 100;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float MillimeterToCentimeter(float a)
	{
		return a / 10;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float MillimeterToMeter(float a) 
	{
		return CentimeterToMeter(MillimeterToCentimeter(a));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float MicrometerToMillimeter(float a)
	{
		return a / 1000;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float MicrometerToMeter(float a)
	{
		return MillimeterToMeter(MicrometerToMillimeter(a));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float NanometerToMicrometer(float a)
	{
		return a / 1000;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float NanometerToMeter(float a)
	{
		return MicrometerToMeter(NanometerToMicrometer(a));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GramToKilogram(float a)
	{
		return a / 1000;
	}
}