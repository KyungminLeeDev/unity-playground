using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SloppyJoe
{
    class MenuItem
    {
        public static Random Randomizer = new Random();
        public string[] Proteins = { "Roast beef", "Salami", "Turkey", "Ham", "Pastrami", "Tofu" };
        public string[] Condiments = { "yellow mustard", "brown mustard", "honey mustard", "mayo", "relish", "french dressing" };
        public string[] Breads = { "rye", "white", "wheat", "pumpernickel", "a roll" };

        public string Description = "";
        public string Price;
        
        public void Generate()
        {
            string randomProtein = Proteins[Randomizer.Next(Proteins.Length)];
            string randomCondiment = Condiments[Randomizer.Next(Condiments.Length)];
            string randomBread = Breads[Randomizer.Next(Breads.Length)];
            Description = randomProtein + " with " + randomCondiment + " on " + randomBread;

            decimal bucks = Randomizer.Next(2, 5);
            decimal cents = Randomizer.Next(1, 98);
            decimal price = bucks + (cents * .01M);
            var usCultureInfo = new System.Globalization.CultureInfo("en-US");
            Price = price.ToString("c", usCultureInfo);

        }
    }
}
