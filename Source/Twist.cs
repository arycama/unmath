using System;
using Unity.Profiling;

[Serializable]
public struct Twist
{
	public Float3 linearVelocity, angularVelocity;

	public Twist(Float3 linearVelocity, Float3 angularVelocity)
	{
		this.linearVelocity = linearVelocity;
		this.angularVelocity = angularVelocity;
	}

	public Twist AddForce(Float3 force) => new(linearVelocity + force, angularVelocity);

	public Twist AddTorque(Float3 torque) => new(linearVelocity, angularVelocity + torque);

	public Twist AddTwist(Twist twist)
	{
		var result = AddForce(twist.linearVelocity);
		return result.AddTorque(twist.angularVelocity);
	}

	public Twist AddForceAtPoint(Float3 force, Float3 point, Float3 centerOfMass)
	{
		var result = AddForce(force);
		return result.AddTorque(Float3.Cross(point - centerOfMass, force));
	}
}
