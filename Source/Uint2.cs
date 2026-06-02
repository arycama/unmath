using System;

namespace Unmath
{
	[Serializable]
	public struct Uint2
	{
		public uint x, y;

		public Uint2(uint x, uint y)
		{
			this.x = x;
			this.y = y;
		}
	}
}