using ClassLibrary1;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Combination combination = new Combination();

            foreach (var item in combination.PrintCancerTargetTherapy())
            {
                Console.WriteLine(item);
            }

            foreach (var item in combination.PrintCancerInhibitorTherapy())
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
