using UnityEngine;

public struct RotationSpring
{
	public Quaternion rotation;
	public Float3 angularVelocity;

	public RotationSpring(Quaternion rotation, Float3 angularVelocity)
	{
		this.rotation = rotation;
		this.angularVelocity = angularVelocity;
	}

	public static RotationSpring Identity => new(Quaternion.Identity, Float3.Zero);

	public void AddImpulse(Float3 torque)
	{
		angularVelocity += torque;
	}

	public void Update(float stiffness, float overshoot = 0)
	{ 
		rotation = Quaternion.SpringDampIdentity(rotation, ref angularVelocity, stiffness, overshoot);
	}
}
