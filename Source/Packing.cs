public static class Packing
{
	public static int Exp2Pow2(int x) => 1 << x;

	public static float Quantize(float x, int bits) => x * (Exp2Pow2(bits) - 1) + 0.5f;

	public static int BitPack(int data, int size, int offset) => (data & (Exp2Pow2(size) - 1)) << offset;

	public static int BitPackFloat(float data, int size, int offset) => BitPack((int)Quantize(data, size), size, offset);
}