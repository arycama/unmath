using System;
using System.Collections.Generic;

[Serializable]
public struct Int3
{
	public int x, y, z;

	public readonly Int2 xy => new(x, y);
	public readonly Int2 xz => new(x, z);

    public Int3(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Int3(Int2 xy, int z) : this(xy.x, xy.y, z) { }

	public static implicit operator Int3(int x) => new(x, x, x);

	public static bool operator ==(Int3 left, Int3 right) => left.x == right.x && left.y == right.y && left.z == right.z;
	public static bool operator !=(Int3 left, Int3 right) => left.x != right.x || left.y != right.y || left.z != right.z;
}