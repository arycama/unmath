using UnityEngine;
using static Math;

public struct Bounds
{
	public Float3 center, extents;

	public Bounds(Float3 center, Float3 extents)
	{
		this.center = center;
		this.extents = extents;
	}

	public static implicit operator Bounds(UnityEngine.Bounds bounds)
	{
		return new Bounds(bounds.center, bounds.extents);
	}

	public static implicit operator UnityEngine.Bounds(Bounds bounds) => new(bounds.center, bounds.extents);

	public static Bounds MinMax(Float3 min, Float3 max) => new(0.5f * (max + min), 0.5f * (max - min));

	public Float3 Min
	{
		readonly get
		{
			return center - extents;
		}
		set
		{
			var max = Max;
			center = 0.5f * (max + value);
			extents = 0.5f * (max - value);
		}
	}

	public Float3 Max
	{
		get
		{
			return center + extents;
		}
		set
		{
			var min = Min;
			center = 0.5f * (value + min);
			extents = 0.5f * (value - min);
		}
	}

	public readonly Float3 Size => 2.0f * extents;

	public Bounds Encapsulate(Float3 point)
	{
		return MinMax(Float3.Min(Min, point), Float3.Max(Max, point));
	}

	public Bounds Encapsulate(Bounds bounds)
	{
		var result = Encapsulate(bounds.center - bounds.extents);
		return result.Encapsulate(bounds.center + bounds.extents);
	}

	/// <summary> Shrinks a bounds to the minimum of it and another bounds </summary>
	public Bounds Shrink(Bounds bounds)
	{
		return MinMax(Float3.Max(Min, bounds.Min), Float3.Min(Max, bounds.Max));
	}

	/// <summary> Intersects a ray against this bounding box and returns whether it hits, and the distance to the first and second hit </summary>
	public bool IntersectRay(Ray3D ray, out float t0, out float t1)
	{
		var invdir = ray.direction.Rcp;

		var sign0 = (invdir.x < 0) ? 1 : 0;
		var sign1 = (invdir.y < 0) ? 1 : 0;
		var sign2 = (invdir.z < 0) ? 1 : 0;

		// TODO: Optimize
		var bounds = new Float3[2]
		{
			Min, Max
		};

		float tymin, tymax, tzmin, tzmax;

		t0 = (bounds[sign0].x - ray.origin.x) * invdir.x;
		t1 = (bounds[1 - sign0].x - ray.origin.x) * invdir.x;
		tymin = (bounds[sign1].y - ray.origin.y) * invdir.y;
		tymax = (bounds[1 - sign1].y - ray.origin.y) * invdir.y;

		if ((t0 > tymax) || (tymin > t1))
			return false;

		if (tymin > t0)
			t0 = tymin;
		if (tymax < t1)
			t1 = tymax;

		tzmin = (bounds[sign2].z - ray.origin.z) * invdir.z;
		tzmax = (bounds[1 - sign2].z - ray.origin.z) * invdir.z;

		if ((t0 > tzmax) || (tzmin > t1))
			return false;

		if (tzmin > t0)
			t0 = tzmin;
		if (tzmax < t1)
			t1 = tzmax;

		return true;
	}

	/// <summary> Transforms the corners of this bounds by a matrix </summary>
	public readonly Bounds Transform(Matrix4x4 matrix)
	{
		Float3 min = default, max = default;
		for (int z = 0, k = 0; z < 2; z++, k++)
		{
			for (var y = 0; y < 2; y++, k++)
			{
				for (var x = 0; x < 2; x++, k++)
				{
					var point = Min + Size * new Float3(x, y, z);
					var transformedPoint = (Float4)(matrix * new Float4(point.x, point.y, point.z, 1));
					var projectedPoint = transformedPoint.xyz / transformedPoint.w;
					min = k == 0 ? projectedPoint : Float3.Min(min, projectedPoint);
					max = k == 0 ? projectedPoint : Float3.Max(max, projectedPoint);
				}
			}
		}

		return MinMax(min, max);
	}

	public readonly Bounds Transform3x4(Matrix4x4 matrix)
	{
		Float3 min = default, max = default;
		for (int z = 0, k = 0; z < 2; z++, k++)
		{
			for (var y = 0; y < 2; y++, k++)
			{
				for (var x = 0; x < 2; x++, k++)
				{
					var point = Min + Size * new Float3(x, y, z);
					var viewPoint = matrix.MultiplyPoint3x4(point);
					min = k == 0 ? viewPoint : Float3.Min(min, viewPoint);
					max = k == 0 ? viewPoint : Float3.Max(max, viewPoint);
				}
			}
		}

		return MinMax(min, max);
	}
}
