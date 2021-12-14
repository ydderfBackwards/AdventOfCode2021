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
            var (polymerTemplate, insertionRules) = ReadInput(input);
            Hashtable pairCount = new Hashtable();


            //Store all pairs in a table and count them. 
            for (int i = 0; i < (polymerTemplate.Length - 1); i++)
            {
                string pair = polymerTemplate.Substring(i, 2);

                if (pairCount.ContainsKey(pair))
                {
                    pairCount[pair] = int.Parse(pairCount[pair].ToString()) + 1;
                }
                else
                {
                    pairCount.Add(pair, 1);
                }
            }

            //Chemical reaction
            for (int i = 0; i < nrOfLoops; i++)
            {
                pairCount = ProcesPairCount(pairCount, insertionRules);

            }


            //Count the letters
            var letterCount = GetLetterCount(pairCount, polymerTemplate[polymerTemplate.Length - 1]);

            long max = 0;
            long min = long.MaxValue;

            //Get max and min value
            foreach (DictionaryEntry letter in letterCount)
            {

                long currentCount = long.Parse(letter.Value.ToString());
                max = Math.Max(max, currentCount);
                min = Math.Min(min, currentCount);
            }
            long result = max - min;

            return result.ToString();
        }


        public Hashtable GetLetterCount(Hashtable pairCount, char finalPolymer)
        {
            string pairStr = "";
            char newLetter = ' ';
            long currentCount = 0;
            Hashtable newLetterCount = new Hashtable();


            foreach (DictionaryEntry pair in pairCount)
            {
                pairStr = pair.Key.ToString();
                newLetter = pairStr[0];

                //The count of the current letter.
                currentCount = long.Parse(pair.Value.ToString());

                if (newLetterCount.ContainsKey(newLetter))
                {
                    newLetterCount[newLetter] = long.Parse(newLetterCount[newLetter].ToString()) + currentCount;
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
                newLetterCount[newLetter] = long.Parse(newLetterCount[newLetter].ToString()) + currentCount;
            }
            else
            {
                newLetterCount.Add(newLetter, currentCount);
            }


            return newLetterCount;
        }

        public Hashtable ProcesPairCount(Hashtable pairCount, Hashtable insertionRules)
        {
            Hashtable newPairCount = new Hashtable();

            foreach (DictionaryEntry pair in pairCount)
            {
                string pairStr = pair.Key.ToString();
                string newLetter = insertionRules[pairStr].ToString();

                //Build new pairs
                string newPair1 = pairStr[0] + newLetter;
                string newPair2 = newLetter + pairStr[1];

                //The count of the current pair.
                long currentCount = long.Parse(pair.Value.ToString());

                //Add new pairs. If they exist --> add the count. Else a new record.
                if (newPairCount.ContainsKey(newPair1))
                {
                    newPairCount[newPair1] = long.Parse(newPairCount[newPair1].ToString()) + currentCount;
                }
                else
                {
                    newPairCount.Add(newPair1, currentCount);
                }

                if (newPairCount.ContainsKey(newPair2))
                {
                    newPairCount[newPair2] = long.Parse(newPairCount[newPair2].ToString()) + currentCount;
                }
                else
                {
                    newPairCount.Add(newPair2, currentCount);
                }
            }

            return newPairCount;
        }


        public long GetQuantity(string polymerTemplate)
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
        public string Process(string polymerTemplate, Hashtable insertionRules)
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

        public (string, Hashtable) ReadInput(string input)
        {
            // Dictionary<string, string> insertionRules = new Dictionary<string, string>();
            Hashtable insertionRules = new Hashtable();

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
