using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Math
{
	// Speed in air at room temperature+standard atmospheric pressure
	public const float SpeedOfSound = 343;

	// Lowest level of sound a human ear can detect
	public const float ThresholdOfHearing = 1e-12f;

	public static float SoundPowerToIntensity(float watts, float distance = 1, float angle = FourPi)
	{
		return watts * Rcp(angle * Square(distance));
	}

	public static float SoundIntensityToSpl(float intensity)
	{
		return 10 * Log10(intensity / ThresholdOfHearing);
	}
}
