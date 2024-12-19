using System;

namespace ScaryClown
{
    interface IClown
    {
        string FunnyThingIHave { get; }
        void Honk();

        protected static Random random = new Random();
        private static int carCapacity = 12;

        public static int CarCapacity
        {
            get { return carCapacity; }
            set
            {
                if (value > 10) carCapacity = value;
                else Console.Error.WriteLine($"Warning: Car capacity {value} is too small");
            }
        }

        public static string ClownCarDescription()
        {
            return $"A clown car with {random.Next(carCapacity / 2, CarCapacity)} clowns";
        }

    }

    interface IScaryClown : IClown
    {
        string ScaryThingIHave { get; }
        void ScareLittleChildren();

        void ScareAdults()
        {
            Console.WriteLine($@"I am an ancient evil that will haunt your dreams. Behold my terrifying necklace with {random.Next(4, 10)} of my last victim's fingers. Oh, also, before I forget...");
            ScareLittleChildren();
        }

    }

    internal class FunnyFunny : IClown
    {
        private string funnyThingIHave;
        public string FunnyThingIHave { get { return funnyThingIHave; } }

        public FunnyFunny(string funnyThingIHave)
        {
            this.funnyThingIHave = funnyThingIHave;
        }

        public void Honk()
        {
            Console.WriteLine($"Hi kids! I have a { funnyThingIHave}.");
        }
    }

    internal class ScaryScary : FunnyFunny, IScaryClown
    {
        private readonly int scaryThingCount;
        public ScaryScary(string funnyThing, int scaryThingCount) : base(funnyThing)
        {
            this.scaryThingCount = scaryThingCount;
        }

        public string ScaryThingIHave { get { return $"{scaryThingCount} spiders"; } }

        public void ScareLittleChildren()
        {
            Console.WriteLine($"Boo! Gotcha! Look at my {ScaryThingIHave}!");
        }


    }




    internal class Program
    {
        static void Main(string[] args)
        {
            IClown.CarCapacity = 18;
            Console.WriteLine(IClown.ClownCarDescription());

            IClown fingersTheClown = new ScaryScary("big red nose", 14);
            fingersTheClown.Honk();
    
            if (fingersTheClown is IScaryClown iScaryClownReference)
            {
                //iScaryClownReference.ScareLittleChildren();
                iScaryClownReference.ScareAdults();
            }
            

        }
    }
}
