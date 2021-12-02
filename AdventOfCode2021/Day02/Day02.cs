using System;
using System.IO;
using System.Linq;


namespace AdventOfCode2021
{
    public class Day02
    {
        public string SolvePart1(string input)
        {
            int posHor = 0;
            int posDepth = 0;

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                char separatingString = ' '; //Char used for splitting
                string[] data = line.Split(separatingString); //Split line to string array
                string direction = data[0];
                int units = int.Parse(data[1]);

                switch (direction)
                {
                    case "forward":
                        posHor += units;
                        break;

                    case "down":
                        posDepth += units;
                        break;

                    case "up":
                        posDepth -= units;
                        break;
                }
            }

            int result = posHor * posDepth;

            return result.ToString();
        }



        public string SolvePart2(string input)
        {
            int posHor = 0;
            int posDepth = 0;
            int aim = 0;

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                char separatingString = ' '; //Char used for splitting
                string[] data = line.Split(separatingString); //Split line to string array
                string direction = data[0];
                int units = int.Parse(data[1]);


                switch (direction)
                {
                    case "forward":
                        posHor += units;
                        posDepth += (units * aim);
                        break;

                    case "down":
                        aim += units;
                        break;

                    case "up":
                        aim -= units;
                        break;
                }



            }

            int result = posHor * posDepth;


            return result.ToString();
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day02();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
