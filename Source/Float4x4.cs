using System;
using UnityEngine;
using static Math;
using static Float3;

public struct Float4x4
{
	public static readonly Float3[] lookAtList =
	{
		new(1.0f, 0.0f, 0.0f),
		new(-1.0f, 0.0f, 0.0f),
		new(0.0f, 1.0f, 0.0f),
		new(0.0f, -1.0f, 0.0f),
		new(0.0f, 0.0f, 1.0f),
		new(0.0f, 0.0f, -1.0f),
	};

	public static readonly Float3[] upVectorList =
	{
		new(0.0f, 1.0f, 0.0f),
		new(0.0f, 1.0f, 0.0f),
		new(0.0f, 0.0f, -1.0f),
		new(0.0f, 0.0f, 1.0f),
		new(0.0f, 1.0f, 0.0f),
		new(0.0f, 1.0f, 0.0f),
	};

	public Float4 c0, c1, c2, c3;

	public Float4x4(Float4 c0, Float4 c1, Float4 c2, Float4 c3)
	{
		this.c0 = c0;
		this.c1 = c1;
		this.c2 = c2;
		this.c3 = c3;
	}

	public Float4x4(float m00 = 1, float m01 = 0, float m02 = 0, float m03 = 0, float m10 = 0, float m11 = 1, float m12 = 0, float m13 = 0, float m20 = 0, float m21 = 0, float m22 = 1, float m23 = 0, float m30 = 0, float m31 = 0, float m32 = 0, float m33 = 1) : this(new(m00, m10, m20, m30), new(m01, m11, m21, m31), new(m02, m12, m22, m32), new(m03, m13, m23, m33)) { }

	public Float4x4(Quaternion q) : this(q.Right, q.Up, q.Forward, new(0, 0, 0, 1)) { }

	public float m00 { readonly get => c0.x; set => c0.x = value; }
	public float m10 { readonly get => c0.y; set => c0.y = value; }
	public float m20 { readonly get => c0.z; set => c0.z = value; }
	public float m30 { readonly get => c0.w; set => c0.w = value; }

	public float m01 { readonly get => c1.x; set => c1.x = value; }
	public float m11 { readonly get => c1.y; set => c1.y = value; }
	public float m21 { readonly get => c1.z; set => c1.z = value; }
	public float m31 { readonly get => c1.w; set => c1.w = value; }

	public float m02 { readonly get => c2.x; set => c2.x = value; }
	public float m12 { readonly get => c2.y; set => c2.y = value; }
	public float m22 { readonly get => c2.z; set => c2.z = value; }
	public float m32 { readonly get => c2.w; set => c2.w = value; }

	public float m03 { readonly get => c3.x; set => c3.x = value; }
	public float m13 { readonly get => c3.y; set => c3.y = value; }
	public float m23 { readonly get => c3.z; set => c3.z = value; }
	public float m33 { readonly get => c3.w; set => c3.w = value; }

	public Float4 r0 { readonly get => new(c0.x, c1.x, c2.x, c3.x); set => (c0.x, c1.x, c2.x, c3.x) = value; }
	public Float4 r1 { readonly get => new(c0.y, c1.y, c2.y, c3.y); set => (c0.y, c1.y, c2.y, c3.y) = value; }
	public Float4 r2 { readonly get => new(c0.z, c1.z, c2.z, c3.z); set => (c0.z, c1.z, c2.z, c3.z) = value; }
	public Float4 r3 { readonly get => new(c0.w, c1.w, c2.w, c3.w); set => (c0.w, c1.w, c2.w, c3.w) = value; }

	public readonly float Determinant =>
			c0.x * TripleProduct(c1.yzw, c2.yzw, c3.yzw) -
			c1.x * TripleProduct(c0.yzw, c2.yzw, c3.yzw) +
			c2.x * TripleProduct(c0.yzw, c1.yzw, c3.yzw) -
			c3.x * TripleProduct(c0.yzw, c1.yzw, c2.yzw);

	public readonly Float4x4 Inverse => Matrix4x4.Inverse(this);
	public readonly Float4x4 Transpose => new(r0, r1, r2, r3);

	public static implicit operator Matrix4x4(Float4x4 a) => new(a.c0, a.c1, a.c2, a.c3);

	public static implicit operator Float4x4(Matrix4x4 a) => new(a.GetColumn(0), a.GetColumn(1), a.GetColumn(2), a.GetColumn(3));

	public static Float4x4 Identity => new();

	public static Float4x4 Scale(Float3 scale) => new(m00: scale.x, m11: scale.y, m22: scale.z);

	public static Float4x4 Rotate(Quaternion rotation) => new(rotation.Right, rotation.Up, rotation.Forward, new Float4(0, 0, 0, 1));

	public static Float4x4 Translate(Float3 translation) => new(m03: translation.x, m13: translation.y, m23: translation.z);

	public static Float4x4 ScaleOffset(Float3 scale, Float3 translation) => new(m00: scale.x, m11: scale.y, m22: scale.z, m03: translation.x, m13: translation.y, m23: translation.z);

	public static Float4x4 TRS(Float3 pos, Quaternion q, Float3 s) => new(q.Right * s.x, q.Up * s.y, q.Forward * s.z, new(pos, 1));

	public static Float4x4 WorldToLocal(Float3 right, Float3 up, Float3 forward, Float3 position) => new
	(
		right.x, right.y, right.z, right.Dot(-position),
		up.x, up.y, up.z, up.Dot(-position),
		forward.x, forward.y, forward.z, forward.Dot(-position)
	);

	public static Float4x4 WorldToLocal(Float3 position, Quaternion rotation) => WorldToLocal(rotation.Right, rotation.Up, rotation.Forward, position);

	public static Float4x4 Ortho(float left, float right, float bottom, float top, float near, float far) => new
	(
		m00: 2 / (right - left), m03: (right + left) / (left - right),
		m11: 2 / (top - bottom), m13: (top + bottom) / (bottom - top),
		m22: 2 / (far - near), m23: (far + near) / (near - far)
	);

	public static Float4x4 Ortho(Bounds bounds) => Ortho(bounds.Min.x, bounds.Max.x, bounds.Min.y, bounds.Max.y, bounds.Min.z, bounds.Max.z);

	public static Float4x4 OrthoReverseZ(float left, float right, float bottom, float top, float near, float far) => new
	(
		m00: 2 / (right - left), m03: (right + left) / (left - right),
		m11: 2 / (top - bottom), m13: (top + bottom) / (bottom - top),
		m22: Rcp(near - far), m23: far / (far - near)
	);

	public static Float4x4 OrthoReverseZ(Bounds bounds) => OrthoReverseZ(bounds.Min.x, bounds.Max.x, bounds.Min.y, bounds.Max.y, bounds.Min.z, bounds.Max.z);

	public static Float4x4 OrthoReverseZSample(float left, float right, float bottom, float top, float near, float far) => new
	(
		m00: Rcp(right - left), m03: -left / (right - left),
		m11: Rcp(bottom - top), m13: top / (top - bottom),
		m22: Rcp(near - far), m23: far / (far - near)
	);

	// TODO: I think this is just an ortho matrix? Remove or refactor
	public static Float4x4 OrthoOffCenterNormalized(float left, float right, float bottom, float top, float near, float far) => new
	(
		m00: 1.0f / (right - left),
		m03: left / (left - right),
		m11: 1.0f / (top - bottom),
		m13: bottom / (bottom - top),
		m22: 1.0f / (far - near),
		m23: near / (near - far)
	);

	public static Float4x4 OrthoReverseZSample(Bounds bounds) => OrthoReverseZSample(bounds.Min.x, bounds.Max.x, bounds.Min.y, bounds.Max.y, bounds.Min.z, bounds.Max.z);

	public static Float4x4 PerspectiveReverseZ(Float2 tanHalfFov, float near, float far) => new
	(
		m00: 1.0f / tanHalfFov.x,
		m11: 1.0f / tanHalfFov.y,
		m22: near / (near - far),
		m23: far * near / (far - near),
		m32: 1.0f,
		m33: 0
	);

	public static Float4x4 PerspectiveReverseZInverse(Float2 tanHalfFov, float near, float far) => new
	(
		m00: tanHalfFov.x,
		m11: tanHalfFov.y,
		m22: 0.0f,
		m23: 1.0f,
		m32: (far - near) / (near * far),
		m33: 1.0f / far
	);

	public static Float4x4 Perspective(float fov, float aspect, float near, float far)
	{
		// TODO: Implement properly
		var matrix = Matrix4x4.Perspective(fov, aspect, near, far);
		matrix.SetColumn(2, -matrix.GetColumn(2));
		return matrix;
	}

	public static Float4x4 PixelToNearClip(Int2 size, Float2 jitter, Float2 tanHalfFov, bool flip = false, bool halfTexel = false)
	{
		var m00 = 2.0f * tanHalfFov.x / size.x;
		var m11 = 2.0f * tanHalfFov.y / size.y;
		var m12 = -(tanHalfFov.y + tanHalfFov.y * jitter.y);

		return new
		(
			m00: m00,
			m11: flip ? -m11 : m11,
			m02: -(tanHalfFov.x + tanHalfFov.x * jitter.x) + (halfTexel ? (0.5f * m00) : 0.0f),
			m12: (flip ? -m12 : m12) + (halfTexel ? 0.5f * (flip ? -m11 : m11) : 0.0f)
		);
	}

	public static Float4x4 PixelToWorldViewDirectionMatrix(Int2 size, Float2 jitter, Float2 tanHalfFov, Float4x4 viewToWorld, bool flip = false, bool halfTexel = false)
	{
		return viewToWorld.Mul(PixelToNearClip(size, jitter, tanHalfFov, flip, halfTexel));
	}

	public readonly Float3 MultiplyVector(Float3 a) => a.x * c0.xyz + a.y * c1.xyz + a.z * c2.xyz;

	public readonly Float3 MultiplyPoint3x4(Float3 a) => MultiplyVector(a) + c3.xyz;

	public readonly Float4 MultiplyPoint(Float3 a) => a.x * c0 + a.y * c1 + a.z * c2 + c3;

	public readonly Float3 MultiplyPointProj(Float3 a) => MultiplyPoint(a).PerspectiveDivide.xyz;

	public readonly Float4x4 Mul(Float4x4 b) => new
	(
		c0 * b.m00 + c1 * b.m10 + c2 * b.m20 + c3 * b.m30,
		c0 * b.m01 + c1 * b.m11 + c2 * b.m21 + c3 * b.m31,
		c0 * b.m02 + c1 * b.m12 + c2 * b.m22 + c3 * b.m32,
		c0 * b.m03 + c1 * b.m13 + c2 * b.m23 + c3 * b.m33
	);

	public readonly Float4 GetFrustumPlane(FrustumPlane plane)
	{
		var result = plane switch
		{
			FrustumPlane.Left => r3 + r0,
			FrustumPlane.Right => r3 - r0,
			FrustumPlane.Down => r3 + r1,
			FrustumPlane.Up => r3 - r1,
			FrustumPlane.Near => r3 + r2,
			FrustumPlane.Far => r3 - r2,
			_ => throw new ArgumentOutOfRangeException(nameof(plane), "Index must be between 0 and 5"),
		};

		return result * result.xyz.RcpMagnitude;
	}
}
