using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_3289_5101
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Good morning");
            Console.WriteLine("new wworld");
            Welcome3289();
            Welcome5101();
            Console.ReadKey();
        }


        static partial void Welcome5101();
        private static void Welcome3289()
        {
            Console.WriteLine("Enter your name: ");
            string readName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", readName);
        }
    }
}
