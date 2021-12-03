using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list

namespace AdventOfCode2021
{
    public class Day03
    {
        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            char[] gammaRate = new char[lines[0].Length];
            char[] epsilonRate = new char[lines[0].Length];

            for (int i = 0; i < lines[0].Length; i++)
            {
                int nrZero = 0;
                int nrOne = 0;

                for (int j = 0; j < lines.Length; j++)
                {
                    if (lines[j][i] == '0')
                        nrZero++;
                    if (lines[j][i] == '1')
                        nrOne++;
                }

                if (nrZero > nrOne)
                {
                    gammaRate[i] = '0';
                    epsilonRate[i] = '1';
                }
                else
                {
                    gammaRate[i] = '1';
                    epsilonRate[i] = '0';
                }

            }

            //Char array to string
            string gammaRateSt = new string(gammaRate);
            string epsilonRateSt = new string(epsilonRate);

            //Get decimal number from string with bin code
            int gammaRateInt = Convert.ToInt32(gammaRateSt, 2);
            int epsilonRateInt = Convert.ToInt32(epsilonRateSt, 2);

            int powerConsumption = gammaRateInt * epsilonRateInt;

            return powerConsumption.ToString();
        }



        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine);
            List<string> list = new List<string>(lines);
            int i = 0;

            while (list.Count > 1)
            {
                int nrZero = 0;
                int nrOne = 0;

                foreach (string listItem in list)
                {
                    if (listItem[i] == '0')
                        nrZero++;
                    if (listItem[i] == '1')
                        nrOne++;
                }

                if (nrZero > nrOne)
                {
                    list.RemoveAll(x => x[i].Equals('1'));
                }
                else
                {
                    list.RemoveAll(x => x[i].Equals('0'));
                }

                i++;
            }

            //Read remaining item
            string oxyGenRatingSt = list[0];


            //Reload list
            list = new List<string>(lines);
            i = 0;

            while (list.Count > 1)
            {
                int nrZero = 0;
                int nrOne = 0;

                foreach (string listItem in list)
                {
                    if (listItem[i] == '0')
                        nrZero++;
                    if (listItem[i] == '1')
                        nrOne++;
                }

                if ( nrZero <= nrOne)
                {
                    list.RemoveAll(x => x[i].Equals('1'));
                }
                else
                {
                    list.RemoveAll(x => x[i].Equals('0'));
                }

                i++;
            }

            //Read remaining string
            string coRatingSt = list[0];

            //Get decimal number from string
            int oxyGenRatingInt = Convert.ToInt32(oxyGenRatingSt, 2);
            int coRatingInt = Convert.ToInt32(coRatingSt, 2);


            int liveSupportRating = oxyGenRatingInt * coRatingInt;

            return liveSupportRating.ToString();
        }





        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day03();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
