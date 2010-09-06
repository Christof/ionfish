namespace Core.Helper
{
    public class ArrayHelper
    {
        public static T[] Create<T>(int length, T value)
        {
            var array = new T[length];

            for (var i = 0; i < length; i++)
            {
                array[i] = value;
            }

            return array;
        }
    }
}