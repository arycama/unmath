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

	public static ScrewState Identity => new(RigidTransform.Identity, Twist.Zero);

	public void SpringDamp(RigidTransform target, float linearStiffness, float angularStiffness, float linearOvershoot = 0, float angularOvershoot = 0)
	{
		pose = RigidTransform.SpringDamp(pose, target, ref twist, linearStiffness, angularStiffness, linearOvershoot, angularOvershoot);
	}

	public Float3 Rotate(Float3 point) => pose.TransformPosition(point);
	
	public Float3 Transform(Float3 point) => pose.TransformPosition(point);

	public void AddForce(Float3 force) => twist.AddForce(force);

	public void AddTorque(Float3 torque) => twist.AddTorque(torque);

	public void AddForceAtPoint(Float3 force, Float3 point, Float3 pivot) => twist.AddForceAtPoint(force, point, pivot);
}
