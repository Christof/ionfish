using Machine.Specifications;

namespace Math
{
    public static class MatrixSpecExtensions
    {
        public static void ShouldEqualWithDelta(this Matrix matrix, Matrix other, float delta = 0.001f)
        {
            matrix.R1C1.ShouldBeCloseTo(other.R1C1, delta);
            matrix.R1C2.ShouldBeCloseTo(other.R1C2, delta);
            matrix.R1C3.ShouldBeCloseTo(other.R1C3, delta);
            matrix.R1C4.ShouldBeCloseTo(other.R1C4, delta);

            matrix.R2C1.ShouldBeCloseTo(other.R2C1, delta);
            matrix.R2C2.ShouldBeCloseTo(other.R2C2, delta);
            matrix.R2C3.ShouldBeCloseTo(other.R2C3, delta);
            matrix.R2C4.ShouldBeCloseTo(other.R2C4, delta);

            matrix.R3C1.ShouldBeCloseTo(other.R3C1, delta);
            matrix.R3C2.ShouldBeCloseTo(other.R3C2, delta);
            matrix.R3C3.ShouldBeCloseTo(other.R3C3, delta);
            matrix.R3C4.ShouldBeCloseTo(other.R3C4, delta);

            matrix.R4C1.ShouldBeCloseTo(other.R4C1, delta);
            matrix.R4C2.ShouldBeCloseTo(other.R4C2, delta);
            matrix.R4C3.ShouldBeCloseTo(other.R4C3, delta);
            matrix.R4C4.ShouldBeCloseTo(other.R4C4, delta);
        }
    }
}