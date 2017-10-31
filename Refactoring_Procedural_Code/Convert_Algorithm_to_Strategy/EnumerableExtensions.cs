using System;
using System.Collections.Generic;
using System.Linq;

namespace Convert_Algorithm_to_Strategy
{
    public static class EnumerableExtensions
    {
        public static T WithMaximum<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> criterion)
            where T : class
            where TKey : IComparable<TKey>
            {
                return sequence.Aggregate((T)null, (best, cur) =>
                {
                    if (best == null) return cur;
                    else return criterion(cur).CompareTo(criterion(best)) < 0 ? best : cur;
                }
                );
        }
    }
}
