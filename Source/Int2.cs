using System;

namespace Unmath
{
	[Serializable]
	public struct Int2 : IEquatable<Int2>
	{
		public int x, y;

		public Int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override readonly string ToString() => $"({x}, {y})";

		public override readonly bool Equals(object obj) => obj is Int2 @int && Equals(@int);

		public readonly bool Equals(Int2 other) => x == other.x && y == other.y;

		public override readonly int GetHashCode() => HashCode.Combine(x, y);

		public static explicit operator Float2(Int2 a) => new(a.x, a.y);

		public static implicit operator Int2(int a) => new(a, a);

		public static bool operator ==(Int2 a, Int2 b) => a.x == b.x && a.y == b.y;
		public static bool operator !=(Int2 a, Int2 b) => a.x != b.x || a.y != b.y;

		public static Int2 operator +(Int2 a, Int2 b) => new(a.x + b.x, a.y + b.y);
		public static Int2 operator -(Int2 a, Int2 b) => new(a.x - b.x, a.y - b.y);
		public static Int2 operator *(Int2 a, Int2 b) => new(a.x * b.x, a.y * b.y);
		public static Int2 operator /(Int2 a, Int2 b) => new(a.x / b.x, a.y / b.y);
	}
}