using UnityEngine;

public static class TransformExtensions
{
	public static Float3 WorldPosition(this Transform transform) => transform.position;

	public static Float3 LocalPosition(this Transform transform) => transform.localPosition;

	public static Quaternion WorldRotation(this Transform transform) => transform.rotation;

	public static Quaternion LocalRotation(this Transform transform) => transform.localRotation;

	public static RigidTransform WorldRigidTransform(this Transform transform)
	{
		transform.GetPositionAndRotation(out var position, out var rotation);
		return new(position, rotation);
	}

	public static RigidTransform LocalRigidTransform(this Transform transform)
	{
		transform.GetLocalPositionAndRotation(out var position, out var rotation);
		return new(position, rotation);
	}

	public static void SetLocalPositionAndRotation(this Transform transform, RigidTransform rigidTransform)
	{
		transform.SetLocalPositionAndRotation(rigidTransform.position, rigidTransform.rotation);
	}

	public static void SetPositionAndRotation(this Transform transform, RigidTransform rigidTransform)
	{
		transform.SetPositionAndRotation(rigidTransform.position, rigidTransform.rotation);
	}

	public static Float3 TransformPosition(this Transform transform, Float3 position)
	{
		return transform.WorldRigidTransform().TransformPosition(position);
	}

	public static Quaternion TransformRotation(this Transform transform, Quaternion rotation)
	{
		return transform.WorldRigidTransform().TransformRotation(rotation);
	}
}