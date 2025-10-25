using System;
using UnityEngine;
using static Math;
using static Float3;

[Serializable]
public struct Quaternion
{
	public float x, y, z, w;

	public Quaternion(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	/// <summary> Construct a Quaternion from Spherical Coordinates </summary>
	public Quaternion(float theta, float phi)
	{
		SinCos(theta * 0.5f, out var sinTheta, out var cosTheta);
		SinCos(phi * 0.5f, out var sinPhi, out var cosPhi);
		x = cosTheta * sinPhi;
		y = sinTheta * cosPhi;
		z = -sinTheta * sinPhi;
		w = cosTheta * cosPhi;
	}

	/// <summary> Construct a Quaternion from a 3x3 matrix </summary>
	public Quaternion(Float3x3 m)
	{
		if (m.Trace > 0f)
		{
			var s = 2 * Sqrt(m.Trace + 1);
			(x, y, z, w) = ((m.m21 - m.m12) / s, (m.m02 - m.m20) / s, (m.m10 - m.m01) / s, s * 0.25f);
		}
		else if (m.m00 > m.m11 && m.m00 > m.m22)
		{
			var s = 2 * Sqrt(1 + m.m00 - m.m11 - m.m22);
			(x, y, z, w) = (s * 0.25f, (m.m10 + m.m01) / s, (m.m02 + m.m20) / s, (m.m21 - m.m12) / s);
		}
		else if (m.m11 > m.m22)
		{
			var s = 2 * Sqrt(1f + m.m11 - m.m00 - m.m22);
			(x, y, z, w) = ((m.m10 + m.m01) / s, s * 0.25f, (m.m21 + m.m12) / s, (m.m02 - m.m20) / s);
		}
		else
		{
			var s = 2 * Sqrt(1f + m.m22 - m.m00 - m.m11);
			(x, y, z, w) = ((m.m02 + m.m20) / s, (m.m21 + m.m12) / s, s * 0.25f, (m.m10 - m.m01) / s);
		}
	}

	public Quaternion(Float3 xyz, float w) : this(xyz.x, xyz.y, xyz.z, w) { }

	public static Quaternion operator +(Quaternion a) => new(a.x, a.y, a.z, a.w);

	public static Quaternion operator -(Quaternion a) => new(-a.x, -a.y, -a.z, -a.w);

	public static Quaternion operator +(Quaternion a, Quaternion b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

	public static Quaternion operator -(Quaternion a, Quaternion b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);

	//public static Quaternion operator *(Quaternion a, Quaternion b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);

	public static Quaternion operator /(Quaternion a, Quaternion b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);

	public static Quaternion operator *(Quaternion a, float b) => new(a.x * b, a.y * b, a.z * b, a.w * b);

	public static Quaternion operator *(float a, Quaternion b) => b * a;

	public static Quaternion operator /(Quaternion a, float b) => new(a.x / b, a.y / b, a.z / b, a.w / b);

	public static implicit operator UnityEngine.Quaternion(Quaternion quaternion) => new(quaternion.x, quaternion.y, quaternion.z, quaternion.w);

	public static implicit operator Quaternion(UnityEngine.Quaternion quaternion) => new(quaternion.x, quaternion.y, quaternion.z, quaternion.w);

	public override readonly string ToString() => $"({x}, {y}, {z}, {w}) ({EulerAngles})";

	public static Quaternion Identity => new(0, 0, 0, 1);

	public Float2 xx => new(x, x);
	public Float2 xy => new(x, y);
	public Float2 xz => new(x, z);
	public Float2 xw => new(x, w);
	public Float2 yx => new(y, x);
	public Float2 yy => new(y, y);
	public Float2 yz => new(y, z);
	public Float2 yw => new(y, w);
	public Float2 zx => new(z, x);
	public Float2 zy => new(z, y);
	public Float2 zz => new(z, z);
	public Float2 zw => new(z, w);
	public Float2 wx => new(w, x);
	public Float2 wy => new(w, y);
	public Float2 wz => new(w, z);
	public Float2 ww => new(w, w);

	// TODO: w variants
	public Float3 xxx => new(x, x, x);
	public Float3 xxy => new(x, x, y);
	public Float3 xxz => new(x, x, z);
	public Float3 xyx => new(x, y, x);
	public Float3 xyy => new(x, y, y);
	public readonly Float3 xyz => new(x, y, z);
	public Float3 xzx => new(x, z, x);
	public Float3 xzy => new(x, z, y);
	public Float3 xzz => new(x, z, z);

	public Float3 yxx => new(y, x, x);
	public Float3 yxy => new(y, x, y);
	public Float3 yxz => new(y, x, z);
	public Float3 yyx => new(y, y, x);
	public Float3 yyy => new(y, y, y);
	public Float3 yyz => new(y, y, z);
	public Float3 yzx => new(y, z, x);
	public Float3 yzy => new(y, z, y);
	public Float3 yzz => new(y, z, z);

	public Float3 zxx => new(z, x, x);
	public Float3 zxy => new(z, x, y);
	public Float3 zxz => new(z, x, z);
	public Float3 zyx => new(z, y, x);
	public Float3 zyy => new(z, y, y);
	public Float3 zyz => new(z, y, z);
	public Float3 zzx => new(z, z, x);
	public Float3 zzy => new(z, z, y);
	public Float3 zzz => new(z, z, z);

	public readonly Float3 Right => new(1 - 2 * (y * y + z * z), 2 * (x * y + z * w), 2 * (x * z - y * w));

	public readonly Float3 Up => new(2 * (x * y - z * w), 1 - 2 * (x * x + z * z), 2 * (x * w + y * z));

	public readonly Float3 Forward => new(2 * (x * z + y * w), 2 * (y * z - x * w), 1 - 2 * (x * x + y * y));

	/// <summary> Half Angle in radians to another quaternion </summary>
	public readonly float HalfAngle(Quaternion a)
	{
		var cosTheta = Dot(this, a);
		var sinTheta = SinFromCos(cosTheta);
		return Atan2(sinTheta, cosTheta);
	}

	/// <summary> Angle in radians to another quaternion </summary>
	public readonly float Angle(Quaternion a)
	{
		return 2.0f * HalfAngle(a);
	}

	/// <summary> Calculates the angles in degrees that a quaternion would rotate by </summary>
	public readonly Float3 EulerAngles
	{
		get
		{
			var epsilon = 1e-6f;
			var CUTOFF = (1.0f - 2.0f * epsilon) * (1.0f - 2.0f * epsilon);
			var qv = new Float4(x, y, z, w);
			var d1 = qv * qv.wwww * 2.0f;
			var d2 = qv * qv.yzxw * 2.0f;
			var d3 = qv * qv;
			Float3 euler;
			var y1 = d2.y - d1.x;
			if (y1 * y1 < CUTOFF)
			{
				var x1 = d2.x + d1.z;
				var x2 = d3.y + d3.w - d3.x - d3.z;
				var z1 = d2.z + d1.y;
				var z2 = d3.z + d3.w - d3.x - d3.y;
				euler = new Float3(Atan2(x1, x2), -Asin(y1), Atan2(z1, z2));
			}
			else
			{
				y1 = Clamp(y1, -1.0f, 1.0f);
				var abcd = new Float4(d2.z, d1.y, d2.y, d1.x);
				var x1 = 2.0f * (abcd.x * abcd.w + abcd.y * abcd.z);
				var x2 = Float4.Csum(abcd * abcd * new Float4(-1f, 1f, -1f, 1f));
				euler = new Float3(Atan2(x1, x2), -Asin(y1), 0.0f);
			}
			return Degrees(euler.yzx);
		}
	}

	/// <summary> Calculates a quaternion that produces the opposite rotation to a quaternion </summary>
	public readonly Quaternion Inverse => new(-xyz, w);

	/// <summary> Calculates the squared magnitude of a quaternion, should be 1 for rotations, mostly used for normalizing quaternions </summary>
	public readonly float SquareMagnitude => Dot(this, this);

	public static Quaternion AngleAxis(float angle, Float3 axis)
	{
		SinCos(0.5f * angle, out var sinHalfAngle, out var cosHalfAngle);
		return new(axis * sinHalfAngle, cosHalfAngle);
	}

	/// <summary> Calculates the axis quaternion rotates around, and the angle it rotates around that axis in radians </summary>
	public static void AngleAxis(Quaternion a, out float angle, out Float3 axis)
	{
		var sinHalfThetaSq = SquareMagnitude(a.xyz);
		var sinHalfTheta = Sqrt(sinHalfThetaSq);
		angle = 2f * Atan2(sinHalfTheta, a.w);
		axis = sinHalfTheta == 0.0f ? Float3.Right : a.xyz / sinHalfTheta;
	}

	/// <summary> Calculates the angular difference to reach quaternion a from quaternion b </summary>
	public static Float3 AngularErrorIdentity(Quaternion a)
	{
		var deltaRotation = a.Inverse;

		// Ensure shortest path
		if (deltaRotation.w < 0)
			deltaRotation = -deltaRotation;

		AngleAxis(deltaRotation, out var angle, out var axis);
		return axis * angle;
	}

	/// <summary> Calculates the angular difference to reach a quaternion from this quaternion </summary>
	public readonly Float3 AngularError(Quaternion a, out float angle)
	{
		var deltaRotation = DeltaRotation(a);

		// Ensure shortest path
		if (deltaRotation.w < 0)
			deltaRotation = -deltaRotation;

		AngleAxis(deltaRotation, out angle, out var axis);
		return axis * angle;
	}

	/// <summary> Calculates the angular difference to reach a quaternion from this quaternion </summary>
	public readonly Float3 AngularError(Quaternion a)
	{
		return AngularError(a, out _);
	}

	public readonly Float3 AngularError1(Quaternion a)
	{
		var deltaRotation = WorldToLocalRotation(a);

		// Ensure shortest path
		if (deltaRotation.w < 0)
			deltaRotation = -deltaRotation;

		AngleAxis(deltaRotation, out var angle, out var axis);
		return axis * angle;
	}

	/// <summary> Calculate how quickly quaternion a rotates towards quaternion b </summary>
	public readonly Float3 AngularVelocity(Quaternion a, out float angle)
	{
		if (Time.deltaTime == 0)
		{
			angle = 0;
			return Zero;
		}
		else
		{
			return AngularError(a, out angle) / Time.deltaTime;
		}
	}

	public readonly Float3 AngularVelocity(Quaternion a) => AngularVelocity(a, out _);

	/// <summary> Rotation that goes from this to a </summary>
	public readonly Quaternion DeltaRotation(Quaternion a) => a.Rotate(Inverse);

	public readonly Quaternion WorldToLocalRotation(Quaternion a) => InverseRotate(a);

	/// <summary> Calculates the cosine of the half-angle between two quaternions </summary>
	public static float Dot(Quaternion a, Quaternion b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;

	/// <summary> Calculates a quaternion that rotates by degrees around the Z, X and then Y axes in that order </summary>
	public static Quaternion Euler(Float3 a)
	{
		SinCos(0.5f * Radians(a), out var sinHalfAngle, out var cosHalfAngle);
		var pitch = new Quaternion(sinHalfAngle.x, 0, 0, cosHalfAngle.x);
		var yaw = new Quaternion(0, sinHalfAngle.y, 0, cosHalfAngle.y);
		var roll = new Quaternion(0, 0, sinHalfAngle.z, cosHalfAngle.z);
		return yaw.Rotate(pitch).Rotate(roll);
	}

	/// <summary> Calculates a quaternion that rotates by degrees around the Z, Y and then X axes in that order </summary>
	public static Quaternion Euler(float x, float y, float z) => Euler(new(x, y, z));

	/// <summary> Calculates a quaternion that rotates from a to b </summary>
	public static Quaternion FromToRotation(Float3 a, Float3 b)
	{
		return FromToRotationNormalized(Float3.Normalize(a), Float3.Normalize(b));
	}

	/// <summary> Calculates a quaternion that rotates from a to b, optimized for normalized inputs </summary>
	public static Quaternion FromToRotationNormalized(Float3 a, Float3 b)
	{
		return UnityEngine.Quaternion.FromToRotation(a, b);
		var discriminant = 0.5f + 0.5f * Float3.Dot(a, b);
		var cosHalfAngle = Sqrt(discriminant);
		return new(Cross(a, b) / (2f * cosHalfAngle), cosHalfAngle);
	}

	/// <summary> Rotates a by the inverse of b </summary>
	public readonly Quaternion InverseRotate(Quaternion a) => Inverse.Rotate(a);

	public readonly Float3 InverseRotate(Float3 a) => Inverse.Rotate(a);

	/// <summary> Calculates the future rotation of a quaternion with an angular velocity in radians </summary>
	public static Quaternion IntegrateVelocity(Quaternion current, Float3 velocity)
	{
		return Normalize(current.Rotate(new Quaternion(0.5f * Time.deltaTime * velocity, 1)));

		var w = SquareMagnitude(velocity);

        if (w == 0.0f)
            return current;

        w = Sqrt(w);
        var v = Time.deltaTime * w * 0.5f;
        var s = Sin(v) / w;
        var q = Cos(v);
        var pqr = velocity * s;
        var quatVel = new Quaternion(pqr, 0);
        return Normalize(quatVel.Rotate(current) + current * q);
	}

	/// <summary> Calculates a quaternion with the specified forward and up directions </summary>
	public static Quaternion LookRotation(Float3 forward, Float3 up)
	{
		// TODO: FIx issues with forward == up
		return UnityEngine.Quaternion.LookRotation(forward, up);
		var right = Float3.Normalize(Cross(up, forward));
		return new(new Float3x3(right, Cross(forward, right), forward));
	}

	/// <summary> Calculates a quaternion with the specified forward direction, with world up </summary>
	public static Quaternion LookRotation(Float3 forward) => LookRotation(forward, new(0, 1, 0));

	/// <summary> Calculates a quaternion partway between a and b, by amount t. Result may need normalization or special handling </summary>
	public readonly Quaternion Lerp(Quaternion a, float t) => Normalize(this + (Dot(this, a) < 0 ? -a : a - this) * t);

	/// <summary> Ensures a quaternion has a unit length of 1 so that the rotation is valid an does not give incorrect results </summary>
	public static Quaternion Normalize(Quaternion a)
	{
		var squareMagnitude = a.SquareMagnitude;
		return squareMagnitude == 0 ? Identity : a * Rsqrt(squareMagnitude);
	}

	/// <summary> Rotates a point by this quaternion </summary>
	public readonly Float3 Rotate(Float3 a)
	{
		var t = 2 * Cross(xyz, a);
		return a + w * t + Cross(xyz, t);
	}

	/// <summary> Rotates a quaternion by this quaternion </summary>
	public readonly Quaternion Rotate(Quaternion a) => new(xyz * a.w + a.xyz * w + Cross(xyz, a.xyz), w * a.w - Float3.Dot(xyz, a.xyz));

	/// <summary> Rotates a point a around point b by this quaternion </summary>
	public readonly Float3 RotateAround(Float3 a, Float3 b) => Rotate(a - b) + b;

	/// <summary> Spherically interpolates from this quaternion to another </summary>
	public readonly Quaternion Slerp(Quaternion b, float t)
	{
		var cosTheta = Dot(this, b);
		if (cosTheta < 0.0f)
		{
			b = -b;
			cosTheta = -cosTheta;
		}

		var sinTheta = SinFromCos(cosTheta);
		var theta = Atan2(sinTheta, cosTheta);
		return sinTheta > 0.0f ? (this * Sin((1 - t) * theta) + b * Sin(t * theta)) / sinTheta : this;
	}

	/// <summary> Calculates a rotation that rotates from a towards b by a max amount </summary>
	public static Quaternion RotateTowards(Quaternion a, Quaternion b, float maxDelta)
	{
		return UnityEngine.Quaternion.RotateTowards(a, b, maxDelta);
		//var angle = Angle(a, b);
		//return angle == 0f ? b : Slerp(a, b, Min(1f, maxDelta / angle));
	}

	public static Quaternion SpringDampIdentity(Quaternion current, ref Float3 velocity, float sqrtStiffness, float overshoot = 0)
	{
		var angularError = AngularErrorIdentity(current);
		velocity = SpringDampVelocity(angularError, velocity, sqrtStiffness, overshoot);
		return IntegrateVelocity(current, velocity);
	}

	/// <summary> Calculates a spring-damping force that transitions from current to target, accounting for current velocity and a stiffness value </summary>
	public static Quaternion SpringDamp(Quaternion current, Quaternion target, ref Float3 velocity, float sqrtStiffness, float overshoot = 0)
	{
		var angularError = current.AngularError(target);
		velocity = SpringDampVelocity(angularError, velocity, sqrtStiffness, overshoot);
		return IntegrateVelocity(current, velocity);
	}
}
