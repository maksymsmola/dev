using System.Collections.Generic;
using System.Linq;

namespace MoneyKeeper.Core.Extensions
{
    public static class CollectionsExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection?.Any() ?? true;
        }
    }
}