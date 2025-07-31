public struct LineSegment2D
{
	public Float2 a, b;

	public LineSegment2D(Float2 a, Float2 b)
	{
		this.a = a;
		this.b = b;
	}

	public float Magnitude => Float2.Distance(a, b);

	public bool DistanceToPoint(Float2 p, out float distance)
	{
		var delta = b - a;
		var segmentLengthSquared = Float2.SquareMagnitude(delta);
		var t = Float2.Dot(p - a, delta) / segmentLengthSquared;

		// Convert segmet to A B C coefficients and calculate distance
		var A = b.y - a.y;
		var B = a.x - b.x;
		var C = Float2.Cross(b - a, a);

		distance = (A * p.x + B * p.y + C) * Float2.RcpMagnitude(new Float2(A, B));

		return t >= 0.0f && t <= 1.0f;
	}

	public bool DistanceAlongRay(Ray2D ray, out float distance)
	{
		distance = new Line2D(a, b).DistanceAlongRay(ray);
		var point = distance * ray.direction + ray.origin;
		var delta = b - a;
		var segmentLengthSquared = Float2.SquareMagnitude(delta);
		var t = Float2.Dot(point - a, delta) / segmentLengthSquared;
		return t >= 0.0f && t <= 1.0f;
	}

	public bool IntersectRay(Ray2D ray, out Float2 point)
	{
		point = new Line2D(a, b).IntersectRay(ray);
		var delta = b - a;
		var segmentLengthSquared = Float2.SquareMagnitude(delta);
		var t = Float2.Dot(point - a, delta) / segmentLengthSquared;
		return t >= 0.0f && t <= 1.0f;
	}

	public bool IntersectLine(Line2D line, out Float2 point)
	{
		// Represent the segment as a line (Ax + By + C = 0)
		var segmentDir = b - a;
		var segmentLine = new Line2D(a, b);

		// Find intersection between the two lines
		point = segmentLine.IntersectLine(line);

		// Check if the intersection point lies within the segment bounds
		var delta = b - a;
		var segmentLengthSquared = Float2.SquareMagnitude(delta);
		var t = Float2.Dot(point - a, delta) / segmentLengthSquared;

		return t >= 0.0f && t <= 1.0f;
	}
}

