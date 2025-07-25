using System;
using UnityEngine;

public static partial class Math
{
	//public const float E = MathF.E;
	//public const float Log2e = (float)1.44269504088896340736;

	public const double PiD = System.Math.PI;
	//public const double Tau = Pi * 2;
	//public const double TwoPi = Tau;
	//public const double FourPi = Pi * 4;
	//public const double HalfPi = Pi * 0.5f;
	//public const double QuarterPi = Pi * 0.25f;

	public static double Radians(double x) => PiD / 180 * x;
	public static double Degrees(double x) => 180 / PiD * x;

	public static double Square(double x) => x * x;
	public static double Sin(double x) => System.Math.Sin(x);
}
