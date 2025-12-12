using System;

[Serializable]
public struct Int2
{
	public int x, y;

	public Int2(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public static explicit operator Float2(Int2 a) => new(a.x, a.y);

	public static implicit operator Int2(int a) => new(a, a);
}