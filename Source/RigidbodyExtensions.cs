using UnityEngine;

public static class RigidbodyExtensions
{
	public static Float3 WorldPosition(this Rigidbody rigidbody) => rigidbody.position;

	public static Quaternion WorldRotation(this Rigidbody rigidbody) => rigidbody.rotation;

	public static RigidTransform WorldTransform(this Rigidbody rigidbody) => new(rigidbody.position, rigidbody.rotation);

	public static Float3 WorldLinearVelocity(this Rigidbody rigidbody) => rigidbody.linearVelocity;

	public static Float3 WorldAngularVelocity(this Rigidbody rigidbody) => rigidbody.angularVelocity;
}