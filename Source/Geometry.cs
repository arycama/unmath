using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Math;

public static class Geometry
{
	// Assume Sphere is at the origin (i.e start = position - spherePosition)
	public static bool TryIntersectRaySphere(Float3 rayOrigin, Float3 rayDirection, float radius, out float r0, out float r1)
	{
		var a = Float3.SquareMagnitude(rayDirection);
		var b = Float3.Dot(rayDirection, rayOrigin) * 2;
		var c = Float3.SquareMagnitude(rayOrigin) - Square(radius);
		return Quadratic(a, b, c, out r0, out r1);
	}

	public static Float3 IntersectRaySphereOutside(Float3 rayOrigin, Float3 rayDirection, Float3 sphereOrigin, float sphereRadius)
	{
		_ = TryIntersectRaySphere(rayOrigin - sphereOrigin, rayDirection, sphereRadius, out var r0, out _);
		return rayOrigin + rayDirection * r0;
	}

	public static Float3 IntersectRaySphereInside(Float3 rayOrigin, Float3 rayDirection, Float3 sphereOrigin, float sphereRadius)
	{
		_ = TryIntersectRaySphere(rayOrigin - sphereOrigin, rayDirection, sphereRadius, out _, out var r1);
		return rayOrigin + rayDirection * r1;
	}

	// This simplified version assume that we care about the result only when we are inside the sphere
	// Assume Sphere is at the origin (i.e start = position - spherePosition) and dir is normalized
	// Ref: http://http.developer.nvidia.com/GPUGems/gpugems_ch19.html
	public static float IntersectRaySphereSimple(Float3 start, Float3 dir, float radius)
	{
		var b = Float3.Dot(dir, start) * 2.0f;
		var c = Float3.Dot(start, start) - radius * radius;
		var discriminant = b * b - 4.0f * c;

		return Abs(Sqrt(discriminant) - b) * 0.5f;
	}

	public static float IntersectRayPlane(Float3 rayOrigin, Float3 rayDirection, Float3 planeOrigin, Float3 planeNormal, out bool isValid)
	{
		var denominator = Float3.Dot(planeNormal, rayDirection);
		isValid = Abs(denominator) > 1e-6f;
		return Float3.Dot(planeNormal, planeOrigin - rayOrigin) / denominator;
	}

	public static float IntersectRayPlane(Float3 rayOrigin, Float3 rayDirection, Float3 planeOrigin, Float3 planeNormal)
	{
		return IntersectRayPlane(rayOrigin, rayDirection, planeOrigin, planeNormal, out _);
	}

	// Solves the quadratic equation of the form: a*t^2 + b*t + c = 0.
	// Returns 'false' if there are no real roots, 'true' otherwise.
	public static bool SolveQuadraticEquation(float a, float b, float c, out Vector2 roots)
	{
		var discriminant = b * b - 4f * a * c;
		var sqrtDet = Mathf.Sqrt(discriminant);

		roots.x = (-b - sqrtDet) / (2f * a);
		roots.y = (-b + sqrtDet) / (2f * a);

		return discriminant >= 0f;
	}

	// Assume Sphere is at the origin (i.e start = position - spherePosition)
	public static bool IntersectRaySphere(Vector3 start, Vector3 dir, float radius, out Vector2 intersections)
	{
		var a = Vector3.Dot(dir, dir);
		var b = Vector3.Dot(dir, start) * 2.0f;
		var c = Vector3.Dot(start, start) - radius * radius;

		return SolveQuadraticEquation(a, b, c, out intersections);
	}
}