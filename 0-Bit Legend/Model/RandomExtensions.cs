namespace _0_Bit_Legend.Model;

public static class RandomExtensions
{
    private static class Cache<T> where T : struct, Enum
    {
        public static readonly T[] Values = Enum.GetValues<T>();
    }

    extension(Random random)
    {
        public T RandomEnum<T>() where T : struct, Enum
        {
            var values = Cache<T>.Values;
            return values[random.Next(values.Length)];
        }
    }
}
