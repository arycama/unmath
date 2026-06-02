using static Unmath.Float3;

namespace Unmath
{
	public struct Bounds
	{
		public Float3 center, extents;

		public Bounds(Float3 center, Float3 extents)
		{
			this.center = center;
			this.extents = extents;
		}

		public static implicit operator Bounds(UnityEngine.Bounds bounds) => new(bounds.center, bounds.extents);

		public static implicit operator UnityEngine.Bounds(Bounds bounds) => new(bounds.center, bounds.extents);

		public static Bounds MinMax(Float3 min, Float3 max) => new(0.5f * (max + min), 0.5f * (max - min));

		public Float3 Min { readonly get => center - extents; set => (center, extents) = (0.5f * (Max + value), 0.5f * (Max - value)); }

		public Float3 Max { readonly get => center + extents; set => (center, extents) = (0.5f * (value + Min), 0.5f * (value - Min)); }

		public readonly Float3 Size => 2.0f * extents;

		public readonly Bounds Encapsulate(Float3 point)
		{
			return MinMax(Min(Min, point), Max(Max, point));
		}

		public readonly Bounds Encapsulate(Bounds bounds)
		{
			var result = Encapsulate(bounds.center - bounds.extents);
			return result.Encapsulate(bounds.center + bounds.extents);
		}

		/// <summary> Shrinks a bounds to the minimum of it and another bounds </summary>
		public readonly Bounds Shrink(Bounds bounds)
		{
			return MinMax(Max(Min, bounds.Min), Min(Max, bounds.Max));
		}

		public readonly Float3 GetCorner(int i)
		{
			return i switch
			{
				0 => new(Min.x, Min.y, Min.z),
				1 => new(Min.x, Min.y, Max.z),
				2 => new(Min.x, Max.y, Min.z),
				3 => new(Min.x, Max.y, Max.z),
				4 => new(Max.x, Min.y, Min.z),
				5 => new(Max.x, Min.y, Max.z),
				6 => new(Max.x, Max.y, Min.z),
				_ => new(Max.x, Max.y, Max.z),
			};
		}

		/// <summary> Intersects a ray against this bounding box and returns whether it hits, and the distance to the first and second hit </summary>
		public readonly bool IntersectRay(Ray3D ray, out float t0, out float t1)
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
		public readonly Bounds Transform(Float4x4 matrix)
		{
			Float3 min = default, max = default;
			for(var i = 0; i < 8; i++)
			{
				var point = GetCorner(i);
				var projectedPoint = matrix.MultiplyPointProj(point);
				min = i == 0 ? projectedPoint : Min(min, projectedPoint);
				max = i == 0 ? projectedPoint : Max(max, projectedPoint);
			}

			return MinMax(min, max);
		}

		public readonly Bounds Transform3x4(Float4x4 matrix)
		{
			Float3 min = default, max = default;
			for (var i = 0; i < 8; i++)
			{
				var point = GetCorner(i);
				var projectedPoint = matrix.MultiplyPoint3x4(point);
				min = i == 0 ? projectedPoint : Min(min, projectedPoint);
				max = i == 0 ? projectedPoint : Max(max, projectedPoint);
			}

			return MinMax(min, max);
		}
	}
}