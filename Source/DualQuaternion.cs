using static Float3;
using static Math;
using static Quaternion;

public struct DualQuaternion
{
	public Quaternion real;
	public Quaternion dual;

	public DualQuaternion(Quaternion real, Quaternion dual)
	{
		this.real = real;
		this.dual = dual;
	}

	public DualQuaternion(RigidTransform rigidTransform) : this(rigidTransform.rotation, new Quaternion(0.5f * rigidTransform.position, 0f).Rotate(rigidTransform.rotation)) { }

	public DualQuaternion(Float3 position, Quaternion rotation) : this(new RigidTransform(position, rotation)) { }

	public static DualQuaternion Identity => new(Quaternion.Identity, Quaternion.Identity);

	public Float3 Position => Rotate(real.Inverse).xyz * 2;

	public Quaternion Rotation => real;

	public Quaternion Rotate(Quaternion a) => dual.Rotate(a);

	public Float3 Rotate(Float3 point) => Rotation.Rotate(point);

	public Float3 Transform(Float3 point) => Rotate(point) + Position;

	public static DualQuaternion Normalize(DualQuaternion a)
	{
		var realNormed = Quaternion.Normalize(a.real);
		var dot = Dot(a.real, a.dual);
		return new(a.dual - a.real * dot, realNormed);
	}

	public static DualQuaternion Dlerp(DualQuaternion a, DualQuaternion b, float t)
	{
		var dot = Dot(a.real, b.real);
		if (dot < 0)
		{
			b.real = -b.real;
			b.dual = -b.dual;
		}

		// TODO: Not sure if normalize is needed
		return Normalize(new DualQuaternion(Slerp(a.real, b.real, t), a.dual + t * (b.dual - a.dual)));
	}

	public static DualQuaternion Multiply(DualQuaternion a, DualQuaternion b)
	{
		return new(a.real.Rotate(b.real), a.real.Rotate(b.dual) + a.dual.Rotate(b.real));
	}

	public static DualQuaternion Conjugate(DualQuaternion a)
	{
		return new DualQuaternion(a.real.Inverse, new(-a.dual.xyz, a.dual.w));
	}

	public static DualQuaternion SpringDamp(DualQuaternion current, DualQuaternion target, ref Twist velocity, float linearStiffness, float angularStiffness, float linearOvershoot = 0, float angularOvershoot = 0)
	{
		return new(Float3.SpringDamp(current.Position, target.Position, ref velocity.linearVelocity, linearStiffness, linearOvershoot), Quaternion.SpringDamp(current.real, target.real, ref velocity.angularVelocity, angularStiffness, angularOvershoot));
	}

	public static DualQuaternion SpringDampDlerp(DualQuaternion current, DualQuaternion target, ref float lerpVelocity, float stiffness, float damping, float deltaTime)
	{
		// Compute error between current and target (as a scalar)
		var error = 1f - Dot(current.real, target.real);
		error = Saturate(error);

		// Spring-damp the interpolation velocity
		var springForce = stiffness * error;
		var dampingForce = -damping * lerpVelocity;
		lerpVelocity += (springForce + dampingForce) * deltaTime;

		// Clamp velocity to avoid overshooting
		var t = lerpVelocity * deltaTime;

		return Dlerp(current, target, t);
	}

	//public static DualQuaternion AddForceAtPoint(DualQuaternion a, ref Wrench velocity, Float3 force, Float3 worldPoint, float deltaTime, Float3 centerOfMass = default)
	//{
	//	// 1. Compute torque due to offset from center of mass
	//	Float3 r = worldPoint - (a.Position + centerOfMass); // Offset vector
	//	Float3 torque = Float3.Cross(r, force); // τ = r × F

	//	// 2. Apply force to linear velocity (F = ma)
	//	linearVelocity += force * deltaTime;

	//	// 3. Apply torque to angular velocity (τ = Iα, assuming I = identity)
	//	angularVelocity += torque * deltaTime;

	//	// 4. Integrate velocities into the dual quaternion
	//	Float3 newPos = a.position + linearVelocity * deltaTime;
	//	Quaternion newRot = IntegrateAngularVelocity(rotation, angularVelocity, deltaTime);
	//	dq = DualQuat.FromTransform(newPos, newRot);
	//}

}