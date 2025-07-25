using static Float3;

public struct Float3x3
{
	public Float3 column0, column1, column2;

	public Float3x3(Float3 column0, Float3 column1, Float3 column2)
	{
		this.column0 = column0;
		this.column1 = column1;
		this.column2 = column2;
	}

	public Float3x3(Quaternion q) : this(q.Right, q.Up, q.Forward) { }

	public float m00 => column0.x;
	public float m10 => column0.y;
	public float m20 => column0.z;
	public float m01 => column1.x;
	public float m11 => column1.y;
	public float m21 => column1.z;
	public float m02 => column2.x;
	public float m12 => column2.y;
	public float m22 => column2.z;

	public Float3x3 Identity => new(Right, Up, Forward);

	public float Trace => m00 + m11 + m22;
}
