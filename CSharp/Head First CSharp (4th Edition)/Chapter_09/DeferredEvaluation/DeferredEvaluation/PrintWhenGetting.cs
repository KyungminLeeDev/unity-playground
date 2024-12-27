using System;
using System.Collections.Generic;
using System.Text;

namespace DeferredEvaluation
{
    internal class PrintWhenGetting
    {
        private int instanceNumber;
        public int InstanceNumber
        {
            set { instanceNumber = value; }
            get
            {
                Console.WriteLine($"Getting #{instanceNumber}");
                return instanceNumber;
            }
        }
    }
}
