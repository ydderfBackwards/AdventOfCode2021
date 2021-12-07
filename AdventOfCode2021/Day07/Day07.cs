using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day07
    {
        public string SolvePart1(string input)
        {
            int[] crabPos = Array.ConvertAll(input.Split(',', StringSplitOptions.RemoveEmptyEntries), Int32.Parse); ;

            int[] fuelUsedCrab = new int[crabPos.Max()]; //The optimal pos will be between the lowest en heighest start position.

            //Calc the total needed fuel for all crabs to move to a position
            for (int i = 0; i < fuelUsedCrab.Length; i++)
            {
                fuelUsedCrab[i] = crabPos.Sum(x => Math.Abs(x - i));
            }

            //Get optimal position
            int fuelSpend = fuelUsedCrab.Min();

            return fuelSpend.ToString();
        }



        public string SolvePart2(string input)
        {
            int[] crabPos = Array.ConvertAll(input.Split(',', StringSplitOptions.RemoveEmptyEntries), Int32.Parse); ;

            int[] fuelUsedCrab = new int[crabPos.Max()]; //The optimal pos will be between the lowest en heighest start position.

            int[] fuelNeeded = new int[crabPos.Max() + 1]; //The optimal pos will be between the lowest en heighest start position.


            //Fist calc the needed fuel to move a number of positions
            fuelNeeded[0] = 0; //Should not be needed because a new array should be zero's
            for (int i = 1; i < fuelNeeded.Length; i++)
            {
                fuelNeeded[i] = fuelNeeded[i - 1] + i;
            }

            //Calc the total needed fuel for all crabs to move to a position
            for (int i = 0; i < fuelUsedCrab.Length; i++)
            {
                fuelUsedCrab[i] = crabPos.Sum(x => fuelNeeded[Math.Abs(x - i)]);
            }

            //Get optimal position
            int fuelSpend = fuelUsedCrab.Min();

            return fuelSpend.ToString(); ;
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day07();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
