using System;
using UnityEngine;
using static Math;
using static Float3;

public struct Float4x4
{
	public Float4 c0, c1, c2, c3;

	public Float4x4(Float4 c0, Float4 c1, Float4 c2, Float4 c3)
	{
		this.c0 = c0;
		this.c1 = c1;
		this.c2 = c2;
		this.c3 = c3;
	}

	public Float4x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33) : this(new(m00, m10, m20, m30), new(m01, m11, m21, m31), new(m02, m12, m22, m32), new(m03, m13, m23, m33)) { }

    public Float4x4(Quaternion q) : this(q.Right, q.Up, q.Forward, new(0, 0, 0, 1)) { }

	public static Float4x4 Identity => new(Right, Up, Forward, new(0, 0, 0, 1));

	public static Float4x4 Ortho(float left, float right, float bottom, float top, float near, float far) => new
	(
		2 / (right - left), 0, 0, (right + left) / (left - right),
		0, 2 / (top - bottom), 0, (top + bottom) / (bottom - top),
		0, 0, 2 / (far - near), (far + near) / (near - far),
		0, 0, 0, 1
	);

	public static Float4x4 Ortho(Bounds bounds) => Ortho(bounds.Min.x, bounds.Max.x, bounds.Min.y, bounds.Max.y, bounds.Min.z, bounds.Max.z);

	public static Float4x4 OrthoReverseZ(float left, float right, float bottom, float top, float near, float far) => new
	(
		2 / (right - left), 0, 0, (right + left) / (left - right),
		0, 2 / (top - bottom), 0, (top + bottom) / (bottom - top),
		0, 0, Rcp(near - far), far / (far - near),
		0, 0, 0, 1
	);

	public static Float4x4 OrthoReverseZ(Bounds bounds) => OrthoReverseZ(bounds.Min.x, bounds.Max.x, bounds.Min.y, bounds.Max.y, bounds.Min.z, bounds.Max.z);

	public static Float4x4 OrthoReverseZSample(float left, float right, float bottom, float top, float near, float far) => new
	(
		Rcp(right - left), 0, 0, -left / (right - left),
		0, Rcp(bottom - top), 0, top / (top - bottom),
		0, 0, Rcp(near - far), far / (far - near),
		0, 0, 0, 1
	);

	public static Float4x4 OrthoReverseZSample(Bounds bounds) => OrthoReverseZSample(bounds.Min.x, bounds.Max.x, bounds.Min.y, bounds.Max.y, bounds.Min.z, bounds.Max.z);

	public static Float4x4 Perspective(float fov, float aspect, float near, float far)
	{
		// TODO: Implement properly
		var matrix = Matrix4x4.Perspective(fov, aspect, near, far);
		matrix.SetColumn(2, -matrix.GetColumn(2));
		return matrix;
	}

	public static Float4x4 Scale(Float3 scale) => new(new Float4(scale.x, 0, 0, 1), new Float4(0, scale.y, 0, 1), new Float4(0, 0, scale.z, 1), new Float4(0, 0, 0, 1));
	public static Float4x4 Translate(Float3 translation) => new(new Float4(0, 0, 0, 1), new Float4(0, 0, 0, 1), new Float4(0, 0, 0, 1), new Float4(translation, 1));
	public static Float4x4 ScaleOffset(Float3 scale, Float3 offset) => new(new Float4(scale.x, 0, 0, 1), new Float4(0, scale.y, 0, 1), new Float4(0, 0, scale.z, 1), new Float4(offset, 1));

	public readonly float m00 => c0.x;
	public readonly float m10 => c0.y;
	public readonly float m20 => c0.z;
	public readonly float m30 => c0.w;

	public readonly float m01 => c1.x;
	public readonly float m11 => c1.y;
	public readonly float m21 => c1.z;
	public readonly float m31 => c1.w;

	public float m02 { readonly get => c2.x; set => c2.x = value; }
	public float m12 { readonly get => c2.y; set => c2.y = value; }
	public float m22 { readonly get => c2.z; set => c2.z = value; }
	public float m32 { readonly get => c2.w; set => c2.w = value; }

	public readonly float m03 => c3.x;
	public readonly float m13 => c3.y;
	public readonly float m23 => c3.z;
	public readonly float m33 => c3.w;

    public readonly Float4 r0 => new(c0.x, c1.x, c2.x, c3.x);
    public readonly Float4 r1 => new(c0.y, c1.y, c2.y, c3.y);
    public readonly Float4 r2 => new(c0.z, c1.z, c2.z, c3.z);
    public readonly Float4 r3 => new(c0.w, c1.w, c2.w, c3.w);

	public readonly float Determinant =>
			c0.x * TripleProduct(c1.yzw, c2.yzw, c3.yzw) - 
			c1.x * TripleProduct(c0.yzw, c2.yzw, c3.yzw) +
			c2.x * TripleProduct(c0.yzw, c1.yzw, c3.yzw) -
			c3.x * TripleProduct(c0.yzw, c1.yzw, c2.yzw);

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

	public static Float4x4 TRS(Float3 pos, Quaternion q, Float3 s) => new(q.Right * s.x, q.Up * s.y, q.Forward * s.z, new(pos, 1));
}