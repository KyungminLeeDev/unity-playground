using System;

namespace AbilityScore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AbilityScoreCalculator calculator = new AbilityScoreCalculator();
            while (true) {
                calculator.RollResult = ReadInt(calculator.RollResult, "Starting 4d6 roll");
                calculator.DivideBy = ReadDouble(calculator.DivideBy, "Divide by");
                calculator.AddAmount = ReadInt(calculator.AddAmount, "Add amount");
                calculator.Minimum = ReadInt(calculator.Minimum, "Minimum");
                calculator.CalculateAbilityScore();
                Console.WriteLine("Calculated ability score: " + calculator.Score);
                Console.WriteLine("Press Q to quit, any other key to continue");
                char keyChar = Console.ReadKey(true).KeyChar;
                if ((keyChar == 'Q') || (keyChar == 'q')) return;
            }
        }

        /// <summary>
        /// 메시지를 출력하고 콘솔에서 int 값을 읽어들 입니다.
        /// </summary>
        /// <param name="lastUsedValue">기본값</param>
        /// <param name="prompt">콘솔에 출력할 메시지</param>
        /// <returns>읽어 들인 int 값 또는 변환이 불가능할 때는 기본값</returns>
        static int ReadInt(int lastUsedValue, string prompt)
        {
            Console.Write(prompt + " [ " + lastUsedValue + " ]: ");
            string line = Console.ReadLine();
            if (int.TryParse(line, out int value)) 
            {
                Console.WriteLine(" using value " + value);
                return value;
            }
            else
            {
                Console.WriteLine(" using default value " + lastUsedValue);
                return lastUsedValue;
            }
        }

        static double ReadDouble(double lastUsedValue, string prompt) 
        {
            Console.Write(prompt + " [ " + lastUsedValue + " ]: ");
            string line = Console.ReadLine();
            if (double.TryParse(line, out double value))
            {
                Console.WriteLine(" using value " + value);
                return value;
            }
            else
            {
                Console.WriteLine(" using default value " + lastUsedValue);
                return lastUsedValue;
            }
        }


    }
}
