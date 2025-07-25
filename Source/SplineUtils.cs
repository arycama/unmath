using System;
using UnityEngine;

public static partial class Math
{
	public static Float2 QuadraticBezier(float t, Float2 p0, Float2 p1, Float2 p2)
	{
		var a = Float2.Lerp(p0, p1, t);
		var b = Float2.Lerp(p1, p2, t);
		return Float2.Lerp(a, b, t);
	}

	public static Float3 QuadraticBezier(float t, Float3 p0, Float3 p1, Float3 p2)
	{
		var a = Float3.Lerp(p0, p1, t);
		var b = Float3.Lerp(p1, p2, t);
		return Float3.Lerp(a, b, t);
	}

	public static Float3 QuadraticBezierVelocity(float t, Float3 p0, Float3 p1, Float3 p2)
	{
		return 2 * ((1 - t) * (p1 - p0) + t * (p2 - p1));
	}

	public static Float3 QuadraticBezierStartVelocity(Float3 p0, Float3 p1) => 2 * (p1 - p0);

	public static Float3 QuadraticBezierEndVelocity(Float3 p1, Float3 p2) => 2 * (p2 - p1);

	public static Float3 QuadraticBezierAcceleration(Float3 p0, Float3 p1, Float3 p2)
	{
		return 2 * (p2 - 2 * p1 + p0);
	}

	public static int DivRoundUp(int x, int y) => (x + y - 1) / y;
}