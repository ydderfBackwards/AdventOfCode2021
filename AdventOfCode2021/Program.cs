using System;
using System.Diagnostics; //For Stopwatch function.

namespace AdventOfCode2021
{
    class AdventOfCode
    {
        static void Main(string[] args)
        {
            //Default start
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            string result1 = "";
            string result2 = "";

            

            //Call Day solution
            var aoc = new Day01(); //Change day number for correct day
            bool testMode = false; //False for running with real input. True for running with example input

            result1 = aoc.SolvePart1(aoc.GetInput(testMode));
            result2 = aoc.SolvePart2(aoc.GetInput(testMode));


            //Default end cycle
            stopWatch.Stop();

            Console.WriteLine(string.Format("Result for part 1: {0}", result1));
            Console.WriteLine(string.Format("Result for part 2: {0}", result2));
            Console.WriteLine();
            Console.WriteLine("Total runtime: " + (stopWatch.ElapsedMilliseconds) + "ms");

            Console.ReadLine();

        }
    }
}
