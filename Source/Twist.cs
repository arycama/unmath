using Unity.Profiling;

public struct Twist
{
	public Float3 linearVelocity, angularVelocity;

	public Twist(Float3 linearVelocity, Float3 angularVelocity)
	{
		this.linearVelocity = linearVelocity;
		this.angularVelocity = angularVelocity;
	}

	public static Twist Zero => new(Float3.Zero, Float3.Zero);

	public void AddForce(Float3 force) => linearVelocity += force;

	public void AddTorque(Float3 torque) => angularVelocity += torque;

	public void AddForceAtPoint(Float3 force, Float3 point, Float3 centerOfMass)
	{
		AddForce(force);
		AddTorque(Float3.Cross(point - centerOfMass, force));
	}
}
