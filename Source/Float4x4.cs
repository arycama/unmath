using System;
using UnityEngine;
using static Math;
using static Float3;

public struct Float4x4
{
	public Float4 c0, c1, c2, c3;

	public Float4x4(Float4 column0, Float4 column1, Float4 column2, Float4 column3)
	{
		c0 = column0;
		c1 = column1;
		c2 = column2;
		c3 = column3;
	}

	public Float4x4(Quaternion q) : this(q.Right, q.Up, q.Forward, new(0, 0, 0, 1)) { }

	public static Float4x4 Ortho(float left, float right, float bottom, float top, float near, float far)
	{
		// TODO: Implement properly
		var matrix = Matrix4x4.Ortho(left, right, bottom, top, near, far);
		matrix.SetColumn(2, -matrix.GetColumn(2));
		return matrix;
	}

	public static Float4x4 Ortho(Bounds bounds)
	{
		return Ortho(bounds.Min.x, bounds.Max.x, bounds.Min.y, bounds.Max.y, bounds.Min.z, bounds.Max.z);
	}

	public static Float4x4 Perspective(float fov, float aspect, float near, float far)
	{
		// TODO: Implement properly
		var matrix = Matrix4x4.Perspective(fov, aspect, near, far);
		matrix.SetColumn(2, -matrix.GetColumn(2));
		return matrix;
	}

	public readonly float m00 => c0.x;
	public readonly float m10 => c0.y;
	public readonly float m20 => c0.z;
	public readonly float m30 => c0.w;

	public readonly float m01 => c1.x;
	public readonly float m11 => c1.y;
	public readonly float m21 => c1.z;
	public readonly float m31 => c1.w;

	public readonly float m02 => c2.x;
	public readonly float m12 => c2.y;
	public readonly float m22 => c2.z;
	public readonly float m32 => c2.w;

	public readonly float m03 => c3.x;
	public readonly float m13 => c3.y;
	public readonly float m23 => c3.z;
	public readonly float m33 => c3.w;

	public readonly float Determinant
	{
		get
		{
			return Matrix4x4.Determinant(this);
		}
	}

	public readonly Float4x4 Identity => new(Right, Up, Forward, new(0, 0, 0, 1));

	public readonly Float4x4 Inverse
	{
		get
		{
			return Matrix4x4.Inverse(this);
		}
	}

	public static implicit operator Matrix4x4(Float4x4 a) => new(a.c0, a.c1, a.c2, a.c3);

	public static implicit operator Float4x4(Matrix4x4 a) => new(a.GetColumn(0), a.GetColumn(1), a.GetColumn(2), a.GetColumn(3));

	public readonly Float3 MultiplyVector(Float3 a) => a.x * c0.xyz + a.y * c1.xyz + a.z * c2.xyz;

	public readonly Float3 MultiplyPoint3x4(Float3 a) => MultiplyVector(a) + c3.xyz;

	public readonly Float4 MultiplyPoint(Float3 a) => a.x * c0 + a.y * c1 + a.z * c2 + c3;

	public readonly Float3 MultiplyPointProj(Float3 a) => MultiplyPoint(a).PerspectiveDivide().xyz;

	public readonly Float4x4 Mul(Float4x4 b) => new
	(
		c0 * b.m00 + c1 * b.m10 + c2 * b.m20 + c3 * b.m30,
		c0 * b.m01 + c1 * b.m11 + c2 * b.m21 + c3 * b.m31,
		c0 * b.m02 + c1 * b.m12 + c2 * b.m22 + c3 * b.m32,
		c0 * b.m03 + c1 * b.m13 + c2 * b.m23 + c3 * b.m33
	);

	public static Float4x4 TRS(Float3 pos, Quaternion q, Float3 s) => Matrix4x4.TRS(pos, q, s);
}