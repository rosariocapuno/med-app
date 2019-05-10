using ClassLibrary1;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CancerTargetedTherapy cancerTargetedTherapy = new CancerTargetedTherapy();
            CommonMeds commonMeds = new CommonMeds();

            foreach (var item in commonMeds.Print())
            {
                Console.WriteLine(item);
            }

            foreach (var item in cancerTargetedTherapy.PrintCancerTargetTherapy())
            {
                Console.WriteLine(item);
            }

            foreach (var item in cancerTargetedTherapy.PrintCancerInhibitorTherapy())
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
