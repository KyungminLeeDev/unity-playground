using System;

namespace CreateSequenceByHand
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sports = new ManualSportSequence();
            foreach (var sport in sports)
                Console.WriteLine(sport);

        }
    }
}
