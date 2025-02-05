using System;

namespace Shoes
{
    internal class Program
    {
        static ShoeCloset shoeCloset = new ShoeCloset();

        static void Main(string[] args)
        {
            while (true)
            {
                shoeCloset.PrintShoe();
                Console.WriteLine("\nPress 'a' to add or 'r' to remove a shoe: ");
                char key = Console.ReadKey().KeyChar;
                
                switch (key)
                {
                    case 'a':
                    case 'A':
                        shoeCloset.AddShoe();
                        break;
                    case 'r':
                    case 'R':
                        shoeCloset.RemoveShoe();
                        break;
                    default:
                        return;
                }

            }
            
        }
    }
}
