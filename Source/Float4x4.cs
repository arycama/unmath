using System;
using UnityEngine;
using static Math;
using static Float3;

public struct Float4x4
{
	public Float4 column0, column1, column2, column3;

	public Float4x4(Float4 column0, Float4 column1, Float4 column2, Float4 column3)
	{
		this.column0 = column0;
		this.column1 = column1;
		this.column2 = column2;
		this.column3 = column3;
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

	public float m00 => column0.x;
	public float m10 => column0.y;
	public float m20 => column0.z;
	public float m30 => column0.w;
	public float m01 => column1.x;
	public float m11 => column1.y;
	public float m21 => column1.z;
	public float m31 => column1.w;
	public float m02 => column2.x;
	public float m12 => column2.y;
	public float m22 => column2.z;
	public float m32 => column2.w;
	public float m03 => column3.x;
	public float m13 => column3.y;
	public float m23 => column3.z;
	public float m33 => column3.w;

	public Float4x4 Identity => new(Right, Up, Forward, new(0, 0, 0, 1));

	public static implicit operator Matrix4x4(Float4x4 a) => new(a.column0, a.column1, a.column2, a.column3);

	public static implicit operator Float4x4(Matrix4x4 a) => new(a.GetColumn(0), a.GetColumn(1), a.GetColumn(2), a.GetColumn(3));

	public Float3 MultiplyVector(Float3 vector)
	{
		Float3 result;
		result.x = m00 * vector.x + m01 * vector.y + m02 * vector.z;
		result.y = m10 * vector.x + m11 * vector.y + m12 * vector.z;
		result.z = m20 * vector.x + m21 * vector.y + m22 * vector.z;
		return result;
	}

	public Float3 MultiplyPoint3x4(Float3 point)
	{
		Float3 result;
		result.x = m00 * point.x + m01 * point.y + m02 * point.z + m03;
		result.y = m10 * point.x + m11 * point.y + m12 * point.z + m13;
		result.z = m20 * point.x + m21 * point.y + m22 * point.z + m23;
		return result;
	}

	public Float3 MultiplyPoint(Float3 point)
	{
		Float3 result;
		result.x = m00 * point.x + m01 * point.y + m02 * point.z + m03;
		result.y = m10 * point.x + m11 * point.y + m12 * point.z + m13;
		result.z = m20 * point.x + m21 * point.y + m22 * point.z + m23;
		var num = m30 * point.x + m31 * point.y + m32 * point.z + m33;
		return result * Rcp(num);
	}
}