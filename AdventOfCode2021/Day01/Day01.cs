using System;
using System.IO;
using System.Linq;


namespace AdventOfCode2021
{
    public class Day01
    {
        public string SolvePart1(string input)
        {
            int count = 0;

            //Convert input to array of integers.
            int[] numbers = Array.ConvertAll(input.Split(Environment.NewLine), int.Parse);

            //Get array length
            int arrayLength = numbers.Length;

            //Loop through array
            for (var i = 0; i < arrayLength-1; i++)
            {
                if (numbers[i] < numbers[i+1] ) {count++;}
            }

            return count.ToString();
        }



        public string SolvePart2(string input)
        {
            int count = 0;

            //Convert input to array of integers.
            int[] numbers = Array.ConvertAll(input.Split(Environment.NewLine), int.Parse);

            //Get array length
            int arrayLength = numbers.Length;

            //Loop through array
            for (var i = 0; i < arrayLength-3; i++)
            {
                //Compare sum of three numbers. But two numbers are the same, so comparing the different numbers is enough
                //Eg: if ((199 + 200 + 208) < (200 + 208 + 210))    is the same as: if (199 < 210)
                if (numbers[i] < numbers[i+3] ) {count++;}
            }

            return count.ToString();

        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day01();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
