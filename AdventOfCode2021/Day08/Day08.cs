using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day08
    {
        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine);
            int total = 0;

            foreach (string line in lines)
            {
                string[] data = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                string[] digitOutputs = data[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string digitOutput in digitOutputs)
                {
                    if (digitOutput.Length <= 4 || digitOutput.Length == 7)
                    {
                        total++;
                    }
                }
            }

            return total.ToString();
        }



        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            int total = 0;

            //Read every line and calculate total
            foreach (string line in lines)
            {
                total += ReadNote(line);

            }


            return total.ToString();
        }

        public int ReadNote(string line)
        {
            int total = 0;
            int[] foundSegments = new int[7];
            //pos in array ==> 0 = a, 1=b ....
            //Value of array means:
            // a = 1
            // b = 2
            // c = 4
            // d = 8
            // e = 16
            // f = 32
            // g = 64
            // So if foundSegments[1] = 16 ==> it means that letter B (on the notes) is segment A;


            string[] data = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
            string[] signalPaterns = data[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] digitOutputs = data[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            //************************* Find B, E, F *******************//
            //Count the total number of each letter.
            //Number of times a segment is used for numbers 0 - 9
            // A 8 (2x)
            // B 6 uniek
            // C 8 (2x)
            // D 7 (2x)
            // E 4 uniek
            // F 9 uniek
            // G 7 (2x)


            int[] letterCount = new int[7];
            char letterF = ' ';
            char letterE = ' ';
            char letterB = ' ';

            foreach (string signalPattern in signalPaterns)
            {
                foreach (char letter in signalPattern)
                {
                    letterCount[((int)letter) - 97]++; //Ascii 97 = a
                }
            }

            //Store segments b, e, f
            for (int i = 0; i < letterCount.Length; i++)
            {
                switch (letterCount[i])
                {
                    case 4:
                        foundSegments[i] = 16; //This letter should be e
                        letterE = Convert.ToChar(i + 97);
                        break;

                    case 6:
                        foundSegments[i] = 2; //This letter should be b
                        letterB = Convert.ToChar(i + 97);
                        break;

                    case 9:
                        foundSegments[i] = 32; //This letter should be f
                        letterF = Convert.ToChar(i + 97);
                        break;
                }
            }

            //*************** Find segment A ************//
            // Segment A is present in the combination with number with three active segments, but not in the number with two segments.
            string length2 = "";
            string lenght3 = "";
            string lenght4 = "";

            foreach (string signalPattern in signalPaterns)
            {
                if (signalPattern.Length == 2)
                {
                    length2 = signalPattern;
                }

                if (signalPattern.Length == 3)
                {
                    lenght3 = signalPattern;
                }

                if (signalPattern.Length == 4)
                {
                    lenght4 = signalPattern;
                }


            }
            //Find the unique char
            char[] letterA = lenght3.Except(length2).ToArray();

            foundSegments[(int)letterA[0] - 97] = 1; // should be segment a


            //************** FOUND UNTIL NOW: A, B, E, F ***************//
            //************** FIND C ***********//
            //the one with two segments is C and F. We found F so the other should be C.

            char[] letterC = length2.Except(letterF.ToString()).ToArray();
            foundSegments[(int)letterC[0] - 97] = 4; // should be segment c

            //************** FOUND UNTIL NOW: A, B, C, E, F ***************//
            // FIND d
            // The one with a length of 4 char, contains B C D F. We got B C F so the remaining one should be D

            string lettersBCF = letterB.ToString() + letterC[0].ToString() + letterF.ToString();

            char[] letterD = lenght4.Except(lettersBCF).ToArray();
            foundSegments[(int)letterD[0] - 97] = 8; // should be segment d

            //************** FOUND UNTIL NOW: A, B, C, D, E, F ***************//
            // FIND g (only remaining)

            for (int i = 0; i < foundSegments.Length; i++)
            {
                if (foundSegments[i] == 0)
                {
                    foundSegments[i] = 64; //should be segment g
                }
            }




            //**************** GetTotal of second part of note ***********
            string numberStr = "";
            foreach (string digitOutput in digitOutputs)
            {
                int digitTotal = 0;
                foreach (char letter in digitOutput)
                {
                    digitTotal += foundSegments[((int)letter - 97)];
                }
                // Value of a segment:
                // a = 1
                // b = 2
                // c = 4
                // d = 8
                // e = 16
                // f = 32
                // g = 64

                //So a number will have a total value of:
                // 0 = 1 + 2 + 4 + 16 + 32 +64 = 119
                // 1 = 4 + 32 = 36
                // 2 = 1 + 4 + 8 + 16 + 64 = 93
                // 3 = 1 + 4 + 8 + 32 + 64 = 109
                // 4 = 2+4+8+32 = 46
                // 5 = 1+2+8+32+64 = 107
                // 6 = 1+2+8+16+32+64 = 123
                // 7 = 1+4+32 = 37
                // 8 = 1+2+4+8+16+32+64 = 127
                // 9 = 1+2+4+8+32+64 = 111
                switch (digitTotal)
                {
                    case 119:
                        numberStr += "0";
                        break;

                    case 36:
                        numberStr += "1";
                        break;

                    case 93:
                        numberStr += "2";
                        break;

                    case 109:
                        numberStr += "3";
                        break;

                    case 46:
                        numberStr += "4";
                        break;

                    case 107:
                        numberStr += "5";
                        break;

                    case 123:
                        numberStr += "6";
                        break;

                    case 37:
                        numberStr += "7";
                        break;

                    case 127:
                        numberStr += "8";
                        break;

                    case 111:
                        numberStr += "9";
                        break;

                    default:
                        Console.WriteLine("Could not find: {0}", digitTotal);
                        break;
                }


            }

            //String to int. 
            total = Int32.Parse(numberStr);

            return total;
        }

     
        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day08();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
