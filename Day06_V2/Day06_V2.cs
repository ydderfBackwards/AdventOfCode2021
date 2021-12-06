using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day06_V2
    {
        //Second version. First version took very long to run. 
        public string SolvePart1(string input)
        {
            var result = GoFish(80, input);
            return result.ToString();
        }



        public string SolvePart2(string input)
        {
            var result = GoFish(256, input);
            return result.ToString();
        }


        public long GoFish(int nrOfDays, string input)
        {
            long[] data = Array.ConvertAll(input.Split(',', StringSplitOptions.RemoveEmptyEntries), Int64.Parse);

            long[] fishTimer = new long[9];

            //Loop to all current fish
            for (int i = 0; i < data.Length; i++)
            {
                //Group the fish by there internal timer value
                fishTimer[data[i]]++;
            }

            //For all days
            for (int day = 0; day < nrOfDays; day++)
            {
                //Every day the current timer is one less
                //So if now there are 10 fishes with time 3, then the next day there will be 10 fishes with timer 2.
                //Solution --> Create a shift register with the grouped timer value.

                //Store counter for timer zero
                long timerZero = fishTimer[0];

                //Shift all timers one position
                for (int i = 1; i < fishTimer.Length; i++)
                {
                    fishTimer[i - 1] = fishTimer[i];
                }

                //Fishes with timer zero will continu as timer 6 and create a new fish
                fishTimer[6] += timerZero; //There could be already some fish with this timer value --> add
                fishTimer[8] = timerZero; //Timer value 8 is the highest so no need to check actual count. 
            }

            return fishTimer.Sum();

        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day06();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
