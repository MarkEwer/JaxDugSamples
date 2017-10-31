using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Iterator_to_Selector.ProceduralVersion
{
    public class Analysis
    {
        public string FindSandwichTypeOrderedMostOftenWithoutCoupon(IEnumerable<Sale> sales)
        {
            var sandwichCount = new Dictionary<string, int>();

            foreach(var s in sales)
            {
                if (s.Coupons == 0)
                {
                    if (!sandwichCount.ContainsKey(s.Item))
                    {
                        sandwichCount.Add(s.Item, s.Quantity);
                    }
                    else
                    {
                        sandwichCount[s.Item] += s.Quantity;
                    }
                }
            }

            var sandwichType = "";
            var count = 0;

            foreach(var k in sandwichCount.Keys)
            {
                if(sandwichCount[k]>count)
                {
                    sandwichType = k;
                    count = sandwichCount[k];
                }
            }

            return sandwichType;
        }
    }
}
