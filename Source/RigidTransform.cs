using System;
using UnityEngine;
using static Float3;
using static Quaternion;

[Serializable]
public struct RigidTransform
{
	public Float3 position;
	public Quaternion rotation;

	public RigidTransform(Float3 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}

	public RigidTransform(Transform transform) : this(transform.position, transform.rotation) { }

	public RigidTransform(DualQuaternion dualQuaternion) : this(dualQuaternion.Position, dualQuaternion.Rotation) { }

	public static explicit operator RigidTransform(Float3 position) => new(position, Quaternion.Identity);

	public static explicit operator RigidTransform(Quaternion rotation) => new(Zero, rotation);

	public static RigidTransform Identity => new(Zero, Quaternion.Identity);

	/// <summary> Creates a rigidTransform that rotates another rigidTransform around a pivot point</summary>
	public static RigidTransform RotateAroundPivot(Quaternion rotation, Float3 pivot) => new(pivot - rotation.Rotate(pivot), rotation);

	public static RigidTransform RotateAroundPivotThenTranslate(Quaternion rotation, Float3 pivot, Float3 translate) => RotateAroundPivot(rotation, pivot).Translate(translate);

	public static RigidTransform SpringDamp(RigidTransform current, RigidTransform target, ref Twist velocity, float linearStiffness, float angularStiffness, float linearOvershoot = 0, float angularOvershoot = 0)
	{
		return new(Float3.SpringDamp(current.position, target.position, ref velocity.linearVelocity, linearStiffness, linearOvershoot), Quaternion.SpringDamp(current.rotation, target.rotation, ref velocity.angularVelocity, angularStiffness, angularOvershoot));
	}

	public readonly RigidTransform Inverse => new(-rotation.InverseRotate(position), rotation.Inverse);

	/// <summary> Applies a rotation to this rigid transform </summary>
	public readonly RigidTransform Rotate(Quaternion rotation) => new(rotation.Rotate(position), rotation.Rotate(this.rotation));

	public readonly RigidTransform Translate(Float3 translation) => new(position + translation, rotation);


	public readonly RigidTransform Transform(RigidTransform a) => a.Rotate(rotation).Translate(position);

	public readonly RigidTransform Transform(Quaternion a) => Transform(new RigidTransform(Zero, a));

	public readonly RigidTransform Transform(Float3 a) => Transform(new RigidTransform(a, Quaternion.Identity));

	public readonly Float3 TransformPosition(Float3 a) => Transform(a).position;

	public readonly Quaternion TransformRotation(Quaternion a) => Transform(a).rotation;


	public readonly RigidTransform InverseTransform(RigidTransform a) => Inverse.Transform(a);

	public readonly RigidTransform InverseTransform(Float3 a) => InverseTransform(new RigidTransform(a, Quaternion.Identity));

	public readonly RigidTransform InverseTransform(Quaternion a) => InverseTransform(new RigidTransform(Zero, a));

	public readonly Float3 InverseTransformPoint(Float3 a) => InverseTransform(a).position;

	public readonly Quaternion InverseTransformRotation(Quaternion a) => InverseTransform(a).rotation;

	public readonly RigidTransform RotateAround(Quaternion rotation, Float3 pivot) => RotateAroundPivot(rotation, pivot).Transform(this);

	/// <summary> RigidTransform that when applied to this will result in a </summary>
	public readonly RigidTransform DeltaTransform(RigidTransform a) => a.Transform(Inverse);

	public readonly RigidTransform Lerp(RigidTransform a, float t) => new(Float3.Lerp(position, a.position, t), rotation.Slerp(a.rotation, t));
}
