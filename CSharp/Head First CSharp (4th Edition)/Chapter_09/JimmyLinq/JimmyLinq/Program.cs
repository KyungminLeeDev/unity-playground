using System;

using System.Collections.Generic;
using System.Linq;

using System.Globalization;

namespace JimmyLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Comic> mostExpensive =
                from comic in Comic.catalog
                where Comic.Prices[comic.Issue] > 500
                orderby Comic.Prices[comic.Issue] descending
                select comic;

            foreach (Comic comic in mostExpensive)
            {
                //Console.WriteLine($"{comic} is worth {Comic.Prices[comic.Issue]:c}");

                // 지역화 때문에 출력결과가 예상과 달라서 수정함
                Console.WriteLine($"{comic} is worth {Comic.Prices[comic.Issue].ToString("c", CultureInfo.GetCultureInfo("en-US"))}");
            }


        }
    }
}
