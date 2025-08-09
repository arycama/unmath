using UnityEngine;

public static class QuaternionExtensions
{
	public static Quaternion Rotate(this UnityEngine.Quaternion a, Quaternion b) => ((Quaternion)a).Rotate(b);

	public static Float3 Rotate(this UnityEngine.Quaternion a, Float3 b) => ((Quaternion)a).Rotate(b);

	public static Quaternion WorldToLocalRotation(this UnityEngine.Quaternion a, Quaternion b) => ((Quaternion)a).WorldToLocalRotation(b);

	//public static Quaternion DeltaRotation(this UnityEngine.Quaternion a, Quaternion b) => ((Quaternion)a).WorldToLocalRotation(b);
}