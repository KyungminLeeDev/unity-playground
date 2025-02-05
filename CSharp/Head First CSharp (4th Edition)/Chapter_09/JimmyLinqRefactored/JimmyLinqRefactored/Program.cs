using System;
using System.Text.RegularExpressions;

using System.Globalization;

namespace JimmyLinqRefactored
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var done = false;
            while (!done)
            {
                Console.WriteLine("\nPress G to group comics by price, R to get reviews, any other key to quit\n");

                switch (Console.ReadKey(true).KeyChar.ToString().ToUpper())
                {
                    case "G":
                        done = GroupComicsByPrice();
                        break;

                    case "R":
                        done = GetReviews();
                        break;

                    default:
                        done = true;
                        break;
                }

            }



        }


        private static bool GroupComicsByPrice()
        {
            var groups = ComicAnalyzer.GroupComicsByPrice(Comic.catalog, Comic.Prices);
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key} comics:");
                foreach (var comic in group)
                {
                    //Console.WriteLine($"#{comic.Issue} {comic.Name}: {Comic.Prices[comic.Issue]:c}");
                    
                    // 지역화 때문에 출력결과가 예상과 달라서 수정함
                    Console.WriteLine($"#{comic.Issue} {comic.Name}: {Comic.Prices[comic.Issue].ToString("C", CultureInfo.GetCultureInfo("en-US"))}");
                }
                

            }
            
            return false;
        }

        private static bool GetReviews()
        {
            var reviews = ComicAnalyzer.GetReviews(Comic.catalog, Comic.Reviews);
            foreach (var review in reviews)
                Console.WriteLine(review);

            return false;
        }
    }
}
