using System;

public struct ScrewState
{
	public RigidTransform pose;
	public Twist twist;

	public ScrewState(RigidTransform pose, Twist twist)
	{
		this.pose = pose;
		this.twist = twist;
	}

	public static ScrewState Identity => new(RigidTransform.Identity, new Twist());

	public ScrewState SpringDamp(RigidTransform target, float linearStiffness, float angularStiffness, float linearOvershoot = 0, float angularOvershoot = 0)
	{
		return new(RigidTransform.SpringDamp(pose, target, ref twist, linearStiffness, angularStiffness, linearOvershoot, angularOvershoot), twist);
	}

	public readonly Float3 Rotate(Float3 point) => pose.TransformPosition(point);

	public readonly Float3 Transform(Float3 point) => pose.TransformPosition(point);

	public readonly ScrewState AddTwist(Twist twist) => new(pose, twist.AddTwist(twist));

	public readonly ScrewState AddForce(Float3 force) => new(pose, twist.AddForce(force));

	public readonly ScrewState AddTorque(Float3 torque) => new(pose, twist.AddTorque(torque));

	public readonly ScrewState AddForceAtPoint(Float3 force, Float3 point, Float3 pivot) => new(pose, twist.AddForceAtPoint(force, point, pivot));
}
