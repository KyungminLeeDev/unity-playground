using System;

namespace PickRandomCards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of cards to pick: ");

            string line = Console.ReadLine();

            if (int.TryParse(line, out int numberOfCards))
            {
                // line을 int로 변환할 수 있으면 반환된 int 값은
                // numberOfCards 변수에 저장되고 이 코드 블록 실행

                foreach (string card in CardPicker.PickSomeCards(numberOfCards))
                {
                    Console.WriteLine(card);
                }
            }
            else
            {
                // line을 int로 변환할 수 없으면 이 코드 블록 실행

                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
}
