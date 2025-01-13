using System;

namespace LambdaTestDrive
{
    internal class Program
    {
        // 필드
        //static Random random = new Random();
        // 속성 : 람다식이 항상 메서드철머 작동하기 때문에 속성이 되었음
        static Random random => new Random();

        //static double GetRandomDouble(int max)
        //{
        //    return max * random.NextDouble();
        //}
        static double GetRandomDouble(int max) => max * random.NextDouble();


        //static void printValue(double d)
        //{
        //    Console.WriteLine($"The value is {d:0.0000}");
        //}
        static void printValue(double d) => Console.WriteLine($"The value is {d:0.0000}");

        static void Main(string[] args)
        {
            var value = Program.GetRandomDouble(100);
            Program.printValue(value);
        }
    }
}
