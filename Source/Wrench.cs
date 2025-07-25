
public struct Wrench
{
	public Float3 force, torque;

	public Wrench(Float3 force, Float3 torque)
	{
		this.force = force;
		this.torque = torque;
	}
}