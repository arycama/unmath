using System;
using static Math;

[Serializable]
public struct FloatRange
{
    public float min, max;

	public FloatRange(float min = float.PositiveInfinity, float max = float.NegativeInfinity)
	{
		this.min = min;
		this.max = max;
	}

	public float Size => max - min;

	public float Clamp(float value) => Math.Clamp(value, min, max);

	public float Lerp(float t) => Math.Lerp(min, max, t);

	public float Random() => Math.Lerp(min, max, UnityEngine.Random.value);

	public void Encapsulate(float value)
	{
		min = Min(min, value);
		max = Max(max, value);
	}

	public void Shrink(float value)
	{
		min = Max(min, value);
		max = Min(max, value);
	}
}
