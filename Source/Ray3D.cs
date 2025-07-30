public struct Ray3D
{
	public Float3 origin, direction;

	public Ray3D(Float3 origin, Float3 direction)
	{
		this.origin = origin;
		this.direction = direction;
	}
}