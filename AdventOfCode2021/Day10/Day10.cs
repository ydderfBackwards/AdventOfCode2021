using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day10
    {
        public string SolvePart1(string input)
        {
            return GetTotalSyntaxErrors(input).ToString();
        }



        public string SolvePart2(string input)
        {
            return GetMiddleScore(input).ToString();
        }

        public long GetMiddleScore(string input)
        {
            List<long> scoreList = new List<long>();
            long score = 0;
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                score = GetScore(line);

                if (score > 0)
                    scoreList.Add(score);
            }

            scoreList.Sort();

            //Get middle value. -1 for zero based count
            long result = scoreList[((scoreList.Count + 1) / 2) - 1];

            return result;
        }


        public long GetScore(string input)
        {
            long score = 0;
            int lastLength = input.Length + 1;

            //Remove all valid chunks until no more valid found. 
            while (input.Length != lastLength)
            {
                lastLength = input.Length;
                input = Regex.Replace(input, @"(\(\)|\{\}|\[\]|\<\>)", "");
            }

            //Search if there are any closing brackets left
            Match myMatch = Regex.Match(input, @"(\)|\}|\]|\>)");

            //If closing bracket found --> get error value of the first one
            if (myMatch.Length > 0)
            {
                //Line with error --> ignore
            }
            else
            {
                //Line incomplete
                //Go to remaining string from end to start. The found opening brackets mean, we mis the correspondending closing brackets
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    score *= 5;
                    switch (input[i])
                    {
                        case '(':
                            score += 1;
                            break;

                        case '[':
                            score += 2;
                            break;

                        case '{':
                            score += 3;
                            break;

                        case '<':
                            score += 4;
                            break;
                    }
                }
            }

            return score;
        }

        public long GetTotalSyntaxErrors(string input)
        {
            long syntaxError = 0;
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                syntaxError += GetSyntaxError(line);
            }
            return syntaxError;
        }

        public long GetSyntaxError(string input)
        {
            long syntaxError = 0;
            int lastLength = input.Length + 1;

            //Remove all valid chunks until no more valid found. 
            while (input.Length != lastLength)
            {
                lastLength = input.Length;
                input = Regex.Replace(input, @"(\(\)|\{\}|\[\]|\<\>)", "");
            }

            //Search if there are any closing brackets left
            Match myMatch = Regex.Match(input, @"(\)|\}|\]|\>)");

            //If closing bracket found --> get error value of the first one
            if (myMatch.Length > 0)
            {
                switch (myMatch.Value)
                {
                    case ")":
                        syntaxError += 3;
                        break;

                    case "]":
                        syntaxError += 57;
                        break;

                    case "}":
                        syntaxError += 1197;
                        break;

                    case ">":
                        syntaxError += 25137;
                        break;
                }
            }
            else
            {
                //Line incomplete
            }


            return syntaxError;
        }




        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day10();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
