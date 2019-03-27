using System.Collections.Generic;
using System.Linq;

namespace Cocorico.Shared.Helpers
{
    public struct CustomHashCode
    {
        private readonly int value;

        private CustomHashCode(int value) => this.value = value;

        public static implicit operator int(CustomHashCode hashCode) => hashCode.value;
        public static implicit operator CustomHashCode(int hashCode) => new CustomHashCode(hashCode);

        public static CustomHashCode Of<T>(T item) => new CustomHashCode(GetHashCode(item));

        public CustomHashCode And<T>(T item) => new CustomHashCode(CombineHashCodes(value, GetHashCode(item)));

        public CustomHashCode AndEach<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                return value;
            }

            var hashCode = items.Any() ? items.Select(GetHashCode).Aggregate(CombineHashCodes) : 0;
            return new CustomHashCode(CombineHashCodes(value, hashCode));
        }

        private static int CombineHashCodes(int h1, int h2)
        {
            unchecked
            {
                // Code copied from System.Tuple so it must be the best way to combine hash codes or at least a good one.
                return ((h1 << 5) + h1) ^ h2;
            }
        }

        private static int GetHashCode<T>(T item) => item is null ? 0 : item.GetHashCode();
    }
}
