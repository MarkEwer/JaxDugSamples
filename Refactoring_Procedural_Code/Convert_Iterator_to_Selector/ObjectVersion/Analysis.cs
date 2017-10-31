using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Convert_Iterator_to_Selector.ObjectVersion
{
    public class Analysis
    {
        public string FindSandwichTypeOrderedMostOftenWithoutCoupon(IEnumerable<Sale> sales)
        {
            var found = sales
                .Where(x => x.Coupons == 0)                               // only sales without coupons
                .GroupBy(x => x.Item)                                     // group by the kind of sandwich
                .Select(x => new ItemSum(x.Key, x.Sum(y => y.Quantity)))  // Sum the quantity for that kind
                .WithMaximum(x=>x.Quantity);                              // Select the highest value

            return found.ItemType;
        }

        private class ItemSum
        {
            public ItemSum(string type, int quantity)
            {
                this.ItemType = type;
                this.Quantity = quantity;
            }
            public string ItemType { get; }
            public int Quantity { get; }
        }
    }

    public static class EnumerableExtensions
    {
        public static T WithMaximum<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> criterion) 
            where T:class
            where TKey:IComparable<TKey>
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
