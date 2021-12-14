using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day14
    {
        //Part 1 is "brute force"
        //Part 2 is "smart solution"
        //          Eg NNC is two pairs: NN and NC
        //          The next scan, NN will be replaced bij NC and CN (if we have two times NN, then this wil be replaced by two NC en two CN)
        //
        //          So keep a count of how offten a pair exist. 
        public string SolvePart1(string input)
        {
            var (polymerTemplate, insertionRules) = ReadInput(input);

            for (int i = 0; i < 10; i++)
            {
                polymerTemplate = Process(polymerTemplate, insertionRules);
            }

            var result = GetQuantity(polymerTemplate);

            return result.ToString();
        }




        public string SolvePart2(string input)
        {
            int nrOfLoops = 40;

            //Read input
            var (polymerTemplate, insertionRules) = ReadInput(input);

            //Store all pairs in a table and count them. 
            var pairCount = GetInitialPairCount(polymerTemplate);

            //Chemical reaction
            for (int i = 0; i < nrOfLoops; i++)
            {
                pairCount = ProcesPairCount(pairCount, insertionRules);
            }

            //Count the letters
            var letterCount = GetLetterCount(pairCount, polymerTemplate[polymerTemplate.Length - 1]);

            //Get max and min diff.
            long result = letterCount.Max(x => x.Value) - letterCount.Min(x => x.Value);

            return result.ToString();
        }

        public Dictionary<string, long> GetInitialPairCount(string polymerTemplate)
        {
            Dictionary<string, long> pairCount = new Dictionary<string, long>();

            for (int i = 0; i < (polymerTemplate.Length - 1); i++)
            {
                string pair = polymerTemplate.Substring(i, 2);

                if (pairCount.ContainsKey(pair))
                {
                    pairCount[pair]++;
                }
                else
                {
                    pairCount.Add(pair, 1);
                }
            }

            return pairCount;
        }


        public Dictionary<char, long> GetLetterCount(Dictionary<string, long> pairCount, char finalPolymer)
        {
            string pairStr = "";
            char newLetter = ' ';
            long currentCount = 0;
            Dictionary<char, long> newLetterCount = new Dictionary<char, long>();


            foreach (var pair in pairCount)
            {
                pairStr = pair.Key;
                newLetter = pairStr[0];

                //The count of the current letter.
                currentCount = pair.Value;

                if (newLetterCount.ContainsKey(newLetter))
                {
                    newLetterCount[newLetter] += currentCount;
                }
                else
                {
                    newLetterCount.Add(newLetter, currentCount);
                }
            }

            //Also add the last letter of the polymerTemplate (start input)
            newLetter = finalPolymer;

            //The count of the current letter.
            currentCount = 1;

            if (newLetterCount.ContainsKey(newLetter))
            {
                newLetterCount[newLetter] += currentCount;
            }
            else
            {
                newLetterCount.Add(newLetter, currentCount);
            }


            return newLetterCount;
        }

        public Dictionary<string, long> ProcesPairCount(Dictionary<string, long> pairCount, Dictionary<string, string> insertionRules)
        {
            Dictionary<string, long> newPairCount = new Dictionary<string, long>();

            foreach (var pair in pairCount)
            {
                string pairStr = pair.Key;
                string newLetter = insertionRules[pairStr];

                //Build new pairs
                string newPair1 = pairStr[0] + newLetter;
                string newPair2 = newLetter + pairStr[1];

                //The count of the current pair.
                long currentCount = pair.Value;

                //Add new pairs. If they exist --> add the count. Else a new record.
                if (newPairCount.ContainsKey(newPair1))
                {
                    newPairCount[newPair1] += currentCount;
                }
                else
                {
                    newPairCount.Add(newPair1, currentCount);
                }

                if (newPairCount.ContainsKey(newPair2))
                {
                    newPairCount[newPair2] += currentCount;
                }
                else
                {
                    newPairCount.Add(newPair2, currentCount);
                }
            }

            return newPairCount;
        }


        public long GetQuantity(string polymerTemplate) //For part 1
        {
            List<char> letter = polymerTemplate.ToList();

            var groupedLetters = letter.GroupBy(x => x).ToList();

            long min = long.MaxValue;
            long max = 0;

            foreach (var uniqueLetter in groupedLetters)
            {
                min = Math.Min(min, uniqueLetter.Count());
                max = Math.Max(max, uniqueLetter.Count());
            }

            return max - min;
        }
        public string Process(string polymerTemplate, Dictionary<string, string> insertionRules) //For part 1
        {
            string newPolymer = "";

            for (int i = 0; i < (polymerTemplate.Length - 1); i++)
            {
                newPolymer += polymerTemplate[i];

                string pair = polymerTemplate.Substring(i, 2);
                newPolymer += insertionRules[pair];
            }

            newPolymer += polymerTemplate[polymerTemplate.Length - 1];

            return newPolymer;
        }

        public (string, Dictionary<string, string>) ReadInput(string input)
        {
            // Dictionary<string, string> insertionRules = new Dictionary<string, string>();
            Dictionary<string, string> insertionRules = new Dictionary<string, string>();

            string[] data = input.Split(Environment.NewLine + Environment.NewLine);

            string polymerTemplate = data[0];

            string[] lines = data[1].Split(Environment.NewLine);

            foreach (string line in lines)
            {
                string[] splitOptions = { "->", " " };
                string[] pairData = line.Split(splitOptions, StringSplitOptions.RemoveEmptyEntries);

                insertionRules.Add(pairData[0], pairData[1]);


            }

            return (polymerTemplate, insertionRules);
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day14();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
