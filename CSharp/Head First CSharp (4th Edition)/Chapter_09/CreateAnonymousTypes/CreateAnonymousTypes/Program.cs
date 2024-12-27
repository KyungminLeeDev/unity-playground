using System;

namespace CreateAnonymousTypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 익명 타입
            // - var 사용하여 익명타입 선언
            // - 익명타입은 이름이 없는것만 제외하면 다른 타입과 동일
            var whatAmI = new { Color = "Blue", Flavor = "Tasty", Height = 37 };
            Console.WriteLine(whatAmI);

            Console.WriteLine($"My color is {whatAmI.Color} and I'm {whatAmI.Flavor}");
        }
    }
}
