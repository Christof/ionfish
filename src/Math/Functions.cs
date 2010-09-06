
namespace Math
{
    public class Functions
    {
        public static float Sin(float angle)
        {
            return (float) System.Math.Sin(angle);
        }

        public static float Cos(float angle)
        {
            return (float) System.Math.Cos(angle);
        }

        public static float CoTan(float angle)
        {
            return Cos(angle) / Sin(angle);
        }
    }
}