namespace Unmath
{
	public struct Plane
	{
		public Float3 normal;
		public float distance;

		public Plane(Float3 normal, float distance)
		{
			this.normal = normal;
			this.distance = distance;
		}

		public Plane(Float3 normal, Float3 point) : this(normal, -normal.Dot(point))
		{
		}

		public Plane(Float3 a, Float3 b, Float3 c)
		{
			normal = (b - a).Cross(c - a).Normalized;
			distance = -normal.Dot(a);
		}

		public static implicit operator UnityEngine.Plane(Plane plane) => new(plane.normal, plane.distance);

		public readonly float Distance(Float3 point) => normal.Dot(point) + distance;

		public readonly Float3 ClosestPoint(Float3 point) => -Distance(point) * normal + point;
	}
}