using System;
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

        public static List<TResult> SelectList<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector)
        {
            return collection.Select(selector).ToList();
        }
    }
}