public struct LineSegment2D
{
	public Float2 a, b;

	public LineSegment2D(Float2 a, Float2 b)
	{
		this.a = a;
		this.b = b;
	}

	public float Magnitude => Float2.Distance(a, b);

	public bool DistanceAlongRay(Ray2D ray, out float distance)
	{
		distance = new Line2D(a, b).DistanceAlongRay(ray);
		var point = distance * ray.direction + ray.origin;
		var segmentDir = b - a;
		var segmentLengthSquared = Float2.SquareMagnitude(segmentDir);
		var t = Float2.Dot(point - a, segmentDir) / segmentLengthSquared;
		return t >= 0.0f && t <= 1.0f;
	}

	public bool IntersectRay(Ray2D ray, out Float2 point)
	{
		point = new Line2D(a, b).IntersectRay(ray);
		var segmentDir = b - a;
		var segmentLengthSquared = Float2.SquareMagnitude(segmentDir);
		var t = Float2.Dot(point - a, segmentDir) / segmentLengthSquared;
		return t >= 0.0f && t <= 1.0f;
	}
}

