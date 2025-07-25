using UnityEngine;
using static Float3;

public struct Float3x4
{
	public Float3 column0, column1, column2, column3;

	public Float3x4(Float3 column0, Float3 column1, Float3 column2, Float3 column3)
	{
		this.column0 = column0;
		this.column1 = column1;
		this.column2 = column2;
		this.column3 = column3;
	}

	public float m00 => column0.x;
	public float m10 => column0.y;
	public float m20 => column0.z;
	public float m01 => column1.x;
	public float m11 => column1.y;
	public float m21 => column1.z;
	public float m02 => column2.x;
	public float m12 => column2.y;
	public float m22 => column2.z;
	public float m03 => column3.x;
	public float m13 => column3.y;
	public float m23 => column3.z;

	public Float3x4 Identity => new(Right, Up, Forward, Zero);

	public static implicit operator Float4x4(Float3x4 a) => new(a.column0, a.column1, a.column2, new(a.column3, 1));

	public static explicit operator Float3x4(Matrix4x4 a) => new((Float3)a.GetColumn(0), (Float3)a.GetColumn(1), (Float3)a.GetColumn(2), (Float3)a.GetColumn(3));
}
