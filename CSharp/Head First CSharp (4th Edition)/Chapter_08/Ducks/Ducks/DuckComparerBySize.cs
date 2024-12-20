using System;
using System.Collections.Generic;
using System.Text;

namespace Ducks
{
    internal class DuckComparerBySize : IComparer<Duck>
    {
        public int Compare(Duck x, Duck y)
        {
            if (x.Size < y.Size)
                return -1;
            if (x.Size > y.Size)
                return 1;
            return 0;
        }
    }
}
