using UnityEngine;

public static class ColorExtensions
{
	public static Color WithAlpha(this Color color, float alpha) => new(color.r, color.g, color.b, alpha);

	public static Float3 Float3(this Color color) => new(color.r, color.g, color.b);

	public static Float3 LinearFloat3(this Color color) => Float3(color.linear);
}