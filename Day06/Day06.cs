using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day06
    {
        public string SolvePart1(string input)
        {
            int nrDays = 80;
            int[] data = Array.ConvertAll(input.Split(',', StringSplitOptions.RemoveEmptyEntries), Int32.Parse);

            List<int> fish = new List<int>(data);

            for (int day = 0; day < nrDays; day++)
            {
                //Minus a day
                fish = fish.Select(f => f - 1).ToList();

                int nrZero = fish.Count(f => f < 0);

                for (int i = 0; i < nrZero; i++)
                {
                    fish.Add(8);
                    fish.Add(6);
                }

                fish.RemoveAll(f => f < 0);
               
            }

            int result = fish.Count();

            return result.ToString();
        }



        public string SolvePart2(string input)
        {

            int days = 256;

            //Store nr of fish for each start value
            long[] preCalc = new long[9];
            preCalc[1] = CalcFish(days, 1, 1);
            Console.WriteLine("1 -> {0}", preCalc[1]);

            preCalc[2] = CalcFish(days, 2, 1);
            Console.WriteLine("2 -> {0}", preCalc[2]);

            preCalc[3] = CalcFish(days, 3, 1);
            Console.WriteLine("3 -> {0}", preCalc[3]);

            preCalc[4] = CalcFish(days, 4, 1);
            Console.WriteLine("4 -> {0}", preCalc[4]);

            preCalc[5] = CalcFish(days, 5, 1);
            Console.WriteLine("5 -> {0}", preCalc[5]);

            preCalc[8] = CalcFish(days, 8, 1);
            Console.WriteLine("8 -> {0}", preCalc[8]);


            //Calculate total
            int[] data = Array.ConvertAll(input.Split(',', StringSplitOptions.RemoveEmptyEntries), Int32.Parse);
            long total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                total += preCalc[data[i]];
            }
            return total.ToString();
        }




        public long CalcFish(int currentToGo, int startNr, long total)
        {
            int daysToGo = currentToGo - startNr - 1;

            //Check if end of days is reached
            if (daysToGo >= 0)
            {
                //Add one extra fish. This new one starts with 8. Current fish restarts at 6
                total++;
                total += CalcFish(daysToGo, 8, 0);
                total += CalcFish(daysToGo, 6, 0);

            }

            return total;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day06();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
