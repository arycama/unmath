using System;
using static Math;

[Serializable]
public struct FloatRange
{
    public float min, max;

	public FloatRange(float min, float max)
	{
		this.min = min;
		this.max = max;
	}

	public float Clamp(float value) => Math.Clamp(value, min, max);

	public float Lerp(float t) => Math.Lerp(min, max, t);

	public float Random() => Math.Lerp(min, max, UnityEngine.Random.value);
}
