using System;

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
}
