using static Math;

public struct Float2x2
{
    public Float2 c0, c1;

    public Float2x2(Float2 c0, Float2 c1)
    {
        this.c0 = c0;
        this.c1 = c1;
    }

    public Float2x2(float m00, float m01, float m10, float m11) : this(new(m00, m10), new(m01, m11))
    {
    }
}
