using System;
using System.Collections.Generic;
using System.Text;

namespace JewelThief
{
    internal class SafeOwner
    {
        private string valuables = "";
        public void ReceiveContents(string safeContetns)
        {
            valuables = safeContetns;
            Console.WriteLine($"Thank you for returning my {valuables}!");
        }
    }
}
