using System;
using System.Collections.Generic;
using System.Text;

namespace ClownWithLambdas
{

    internal class TallGuy : IClown
    {

        public string Name;
        public int Height;

        public void TalkAboutYourself()
        {
            Console.WriteLine($"My name is {Name} and I'm {Height} inches tall.");
        }

        public string FunnyThingIHave => "big red shoes";

        public void Honk() => Console.WriteLine("Honk honk!");
    }
}
