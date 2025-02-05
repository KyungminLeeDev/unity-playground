using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CreateSequenceByHand
{
    internal class ManualSportSequence : IEnumerable<Sport>
    {
        public IEnumerator<Sport> GetEnumerator()
        {
            return new ManualSportEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
