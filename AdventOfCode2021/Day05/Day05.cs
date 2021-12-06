using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day05
    {
        public int MAPSIZE = 999;
        public string SolvePart1(string input)
        {
            List<Points> linesOfVent = ReadInput(input, true);

            var map = createMap(linesOfVent);

            var count = countOverlaps(map);
            //AOC.Helpers.Array2D.PrintArray(map, false, false);

            return count.ToString();
        }

        public string SolvePart2(string input)
        {
            List<Points> linesOfVent = ReadInput(input, false);

            var map = createMap(linesOfVent);

            var count = countOverlaps(map);
            //AOC.Helpers.Array2D.PrintArray(map, false, false);
            return count.ToString();

        }


        public int countOverlaps(int[,] map)
        {
            int count = 0;

            for (int x = 0; x < MAPSIZE; x++)
            {
                for (int y = 0; y < MAPSIZE; y++)
                {
                    if (map[x, y] > 1)
                        count++;
                }
            }

            return count;
        }

        public int[,] createMap(List<Points> linesOfVent)
        {
            int[,] map = new int[MAPSIZE, MAPSIZE];

            foreach (Points line in linesOfVent)
            {
                //Determine direction of line
                int dirX = (line.fromX < line.toX ? +1 : line.fromX > line.toX ? -1 : 0);
                int dirY = (line.fromY < line.toY ? +1 : line.fromY > line.toY ? -1 : 0);

                //Determine line length (assume only 0, 90 or 45 degree)
                int lineLength = line.fromX != line.toX ? Math.Abs(line.fromX - line.toX) : Math.Abs(line.fromY - line.toY);

                for (int i = 0; i <= lineLength; i++)
                {
                    map[line.fromX + (i * dirX), line.fromY + (i * dirY)]++;
                }
            }
            return map;
        }


        public List<Points> ReadInput(string input, bool removeDiagonal)
        {
            List<Points> linesOfVent = new List<Points>();


            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                string[] seperators = { ",", "->", " " };
                // string[] data = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                int[] data = Array.ConvertAll(line.Split(seperators, StringSplitOptions.RemoveEmptyEntries), Int32.Parse);

                Points points = new Points();
                points.fromX = data[0];
                points.fromY = data[1];
                points.toX = data[2];
                points.toY = data[3];

                linesOfVent.Add(points);

            }

            if (removeDiagonal)
                linesOfVent.RemoveAll(x => x.fromX != x.toX && x.fromY != x.toY);

            return linesOfVent;
        }

        public struct Points
        {
            public int fromX;
            public int fromY;
            public int toX;
            public int toY;
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day05();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
