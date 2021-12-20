using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day20
    {

        public string SolvePart1(string input)
        {
            int nrOfEnchancements = 2;
            (var IEAlgorithm, var image) = ReadInput(input, nrOfEnchancements);

            for (int i = 0; i < nrOfEnchancements; i++)
            {
                image = EnchanceImage(image, IEAlgorithm);
            }

            long count = CountPixels(image);
            return count.ToString();
        }



        public string SolvePart2(string input)
        {

            int nrOfEnchancements = 50;
            (var IEAlgorithm, var image) = ReadInput(input, nrOfEnchancements);

            for (int i = 0; i < nrOfEnchancements; i++)
            {
                image = EnchanceImage(image, IEAlgorithm);
            }

            long count = CountPixels(image);
            return count.ToString();
        }

        public long CountPixels(int[,] image)
        {
            long count = 0;
            for (int x = 1; x < image.GetLength(0) - 2; x++)
            {
                for (int y = 1; y < image.GetLength(0) - 2; y++)
                {
                    if (image[x, y] == 1)
                        count++;
                }
            }

            return count;
        }
        public int[,] EnchanceImage(int[,] image, int[] IEAlgorithm)
        {

            int[,] newImage = new int[image.GetLength(0) - 2, image.GetLength(1) - 2];
            int[,] baseImage = new int[image.GetLength(0), image.GetLength(1)];

            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(0); y++)
                {
                    baseImage[x, y] = image[x, y];
                }
            }


            for (int x = 1; x < baseImage.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < baseImage.GetLength(1) - 1; y++)
                {
                    int binCode =
                    baseImage[x - 1, y - 1] * 256 +
                    baseImage[x - 0, y - 1] * 128 +
                    baseImage[x + 1, y - 1] * 64 +
                    baseImage[x - 1, y - 0] * 32 +
                    baseImage[x - 0, y - 0] * 16 +
                    baseImage[x + 1, y - 0] * 8 +
                    baseImage[x - 1, y + 1] * 4 +
                    baseImage[x - 0, y + 1] * 2 +
                    baseImage[x + 1, y + 1] * 1;

                    if (binCode == 511)
                        binCode = 511;


                    newImage[x - 1, y - 1] = IEAlgorithm[binCode];
                }

            }



            return newImage;
        }

        public (int[], int[,]) ReadInput(string input, int nrOfEnchancements)
        {
            int offsetForEnchancement = 4;
            string[] data = input.Split(Environment.NewLine + Environment.NewLine);

            //------ image enhancement algorithm ------
            int[] IEAlgorithm = new int[data[0].Length];

            for (int i = 0; i < data[0].Length; i++)
            {
                IEAlgorithm[i] = data[0][i].Equals('#') ? 1 : 0;
            }

            //------ Image -----
            string[] lines = data[1].Split(Environment.NewLine);

            //Create a bigger image to handle the infinitive image
            int[,] image = new int[lines.Length + nrOfEnchancements * offsetForEnchancement * 2, lines[0].Length + nrOfEnchancements * offsetForEnchancement * 2];


            int y = 0 + nrOfEnchancements * offsetForEnchancement; //Correction so the base image is placed in the center of the infinitive image
            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    int x = i + nrOfEnchancements * offsetForEnchancement; //Correction so the base image is placed in the center of the infinitive image
                    image[x, y] = line[i].Equals('#') ? 1 : 0;
                }
                y++;
            }

            return (IEAlgorithm, image);
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day20();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
