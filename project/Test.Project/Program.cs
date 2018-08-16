using System;
using Test.Package;

namespace Test.Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Current Package Version: {0}", TestClass.GetVersion());
        }
    }
}
