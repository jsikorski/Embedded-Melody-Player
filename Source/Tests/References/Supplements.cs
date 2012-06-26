using System.Collections.Generic;
using System.Linq;

namespace VikingErik.NetMF.MicroLinq
{
    internal static class LinqExtensions
    {
        public static bool Contains<T>(this IEnumerable<T> enumarable, T element)
        {
            return Enumerable.Contains(enumarable, element);
        }

        public static int Count<T>(this IEnumerable<T> enumrable)
        {
            return Enumerable.Count(enumrable);
        }
    }
}