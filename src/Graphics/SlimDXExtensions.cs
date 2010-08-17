namespace Graphics
{
    public static class SlimDXExtensions
    {
        public static SlimDX.Matrix ToSlimDX(this Math.Matrix matrix)
        {
            var result = new SlimDX.Matrix
            {
                M11 = matrix.R1C1,
                M12 = matrix.R1C2,
                M13 = matrix.R1C3,
                M14 = matrix.R1C4,
                M21 = matrix.R2C1,
                M22 = matrix.R2C2,
                M23 = matrix.R2C3,
                M24 = matrix.R2C4,
                M31 = matrix.R3C1,
                M32 = matrix.R3C2,
                M33 = matrix.R3C3,
                M34 = matrix.R3C4,
                M41 = matrix.R4C1,
                M42 = matrix.R4C2,
                M43 = matrix.R4C3,
                M44 = matrix.R4C4
            };

            return result;
        }
    }
}