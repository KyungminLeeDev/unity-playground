using System;

namespace SwindlerDiabolicalPlan
{
    using System.IO;

    internal class Program
    {
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter("Secret_plan.txt");

            sw.WriteLine("How I'll defeat Captain Amazing");
            sw.WriteLine("Another genius secret plan by the Swindler");
            sw.WriteLine("I'll unleash my army of clones upon the citizens of Objectville.");

            string location = "the mall";
            for (int number = 1; number <= 5; number++)
            {
                sw.WriteLine("Clone #{0} attacks {1}", number, location);
                location = (location == "the mall") ? "downtown" : "the mall";
            }
            sw.Close();

        }
    }
}
