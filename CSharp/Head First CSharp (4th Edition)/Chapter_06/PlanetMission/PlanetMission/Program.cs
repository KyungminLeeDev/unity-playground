using System;

namespace PlanetMission
{
    abstract class PlanetMission
    {
        protected float fuelPerKm;
        protected long kmPerHour;
        protected long kmToPlanet;
        public string MissionInfo()
        {
            long fuel = (long)(kmToPlanet * fuelPerKm);
            long time = kmToPlanet / kmPerHour;
            return $"We'll burn {fuel} units of fuel in {time} hours";
        }
    }

    internal class Mars : PlanetMission
    {
        public Mars()
        {
            kmToPlanet = 92000000;
            fuelPerKm = 1.73f;
            kmPerHour = 37000;
        }
    }

    internal class Venus : PlanetMission
    {
        public Venus()
        {
            kmToPlanet = 41000000;
            fuelPerKm = 2.11f;
            kmPerHour = 29500;
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Venus().MissionInfo());
            Console.WriteLine(new Mars().MissionInfo());
            //Console.WriteLine(new PlanetMission().MissionInfo()); PlanetMission이 추상클래스이므로 인스턴스화 할수 없음
        }
    }
}
