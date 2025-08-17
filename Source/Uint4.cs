using System;

[Serializable]
public struct Uint4
{
	public uint x, y, z, w;

	public Uint4(uint x, uint y, uint z, uint w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}
}