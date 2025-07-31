
/// <summary> Stores a 2D line in implicit form with factors a, b, c </summary>
public struct Line2D
{
	public float a, b, c;

	/// <summary> Constructs a 2D line from a, b and c factors </summary>
	public Line2D(float a, float b, float c)
	{
		this.a = a;
		this.b = b;
		this.c = c;
	}

	public Float2 aa => new(a, a);
	public Float2 ab => new(a, b);
	public Float2 ac => new(a, c);
	public Float2 ba => new(b, a);
	public Float2 bb => new(b, b);
	public Float2 bc => new(b, c);
	public Float2 ca => new(c, a);
	public Float2 cb => new(c, b);
	public Float2 cc => new(c, c);

	/// <summary> Line defined by a direction </summary>
	public Line2D(Float2 dir) : this() { }

	public Line2D(Float2 a, Float2 b) : this(b.y - a.y, a.x - b.x, Float2.Cross(b - a, a))
	{
	}

	public float DistanceToPoint(Float2 p)
	{
		return (a * p.x + b * p.y + c) * Float2.RcpMagnitude(ab);
	}

	public float DistanceToClosestPoint(Float2 p)
	{
		var closestPoint = ClosestPoint(p);
		var origin = new Float2(0, -c / b); // Or another point on the line
		var direction = Float2.Normalize(new Float2(-b, a));
		return Float2.Dot(closestPoint - origin, direction);
	}

	public Float2 ClosestPoint(Float2 p) => p - ab * DistanceToPoint(p);

	public float DistanceAlongRay(Ray2D ray)
	{
		return -(Float2.Dot(ab, ray.origin) + c) / Float2.Dot(ab, ray.direction);
	}

	public Float2 IntersectRay(Ray2D ray)
	{
		return ray.direction * DistanceAlongRay(ray) + ray.origin;
	}

	public Float2 IntersectLine(Line2D line)
	{
		// Calculate the denominator for the intersection formula
		var denominator = a * line.b - line.a * b;

		// Calculate intersection coordinates
		var x = (b * line.c - line.b * c) / denominator;
		var y = (line.a * c - a * line.c) / denominator;

		return new Float2(x, y);
	}
}

