public static partial class Math
{
	public static Float3 SphericalToCartesian(float cosTheta, float sinTheta, float cosPhi, float sinPhi) => new(sinPhi * cosTheta, sinPhi * sinTheta, cosPhi);

	public static Float3 SphericalToCartesian(float cosTheta, float sinTheta, float cosPhi)
	{
		var sinPhi = SinFromCos(cosPhi);
		return SphericalToCartesian(cosTheta, sinTheta, cosPhi, sinPhi);
	}

	public static Float3 SphericalToCartesian(float theta, float phi)
    {
        SinCos(theta, out var sinTheta, out var cosTheta);
        SinCos(phi, out var sinPhi, out var cosPhi);
		return SphericalToCartesian(cosTheta, sinTheta, cosPhi, sinPhi);
	}

    public static Float3 SphericalToCartesianDerivativeThetaNormalized(float theta, float phi)
    {
        SinCos(theta, out var sinTheta, out var cosTheta);
        return new(-sinTheta, cosTheta, 0);
    }

    public static Float3 SphericalToCartesianDerivativeTheta(float theta, float phi)
    {
        SinCos(theta, out var sinTheta, out var cosTheta);
        SinCos(phi, out var sinPhi, out _);
        return new(-sinPhi * sinTheta, sinPhi * cosTheta, 0);
    }

    public static Float3 SphericalToCartesianDerivativePhi(float theta, float phi)
    {
        SinCos(theta, out var sinTheta, out var cosTheta);
        SinCos(phi, out var sinPhi, out var cosPhi);
        return new(cosPhi * cosTheta, cosPhi * sinTheta, -sinPhi);
    }

	public static Float2 QuaternionToSpherical(Quaternion a)
	{
		Quaternion.AngleAxis(a, out _, out var axis);
		return new(WrapAngle(Atan2(axis.y, axis.x)), Acos(axis.z));
	}
}