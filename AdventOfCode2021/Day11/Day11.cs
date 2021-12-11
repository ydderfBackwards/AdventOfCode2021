using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day11
    {
        public string SolvePart1(string input)
        {
            int nrOfSteps = 100;
            long totalFlashes = 0;
            int flashes = 0;

            var octopuses = ReadInput(input);

            for (int i = 0; i < nrOfSteps; i++)
            {
               (flashes, octopuses) = Step(octopuses);
               totalFlashes += flashes;
            }

            return totalFlashes.ToString();
        }

      public string SolvePart2(string input)
        {
            int flashes = 0;
            int step = 0;

            var octopuses = ReadInput(input);

            int totalOctopuses = octopuses.Count();

            while(flashes < totalOctopuses)
            {
               (flashes, octopuses) = Step(octopuses);
               step++;
            }

            return step.ToString();
        }

        public (int, List<Octopus>) Step(List<Octopus> octopuses)
        {
            int totalFlashes = 0;
            //Add one
            octopuses = octopuses.Select(o => { o.Engergy += 1; return o; }).ToList();

            //Keep checking until all done with flashing
            while (octopuses.Exists(x => x.Engergy > 9))
            {
                //Store who flashes this time
                octopuses = octopuses.Select(o => { o.Flash = (o.Engergy > 9); return o; }).ToList();

                //Set all flashing octopuses back to energy level 0
                octopuses = octopuses.Select(o => { o.Engergy = (o.Engergy > 9 ? 0 : o.Engergy); return o; }).ToList();

                //Add one for all neighbours of the flashing octopuses
                octopuses = octopuses.Select(o =>
                {
                    o.Engergy += (o.Engergy == 0 ? 0 : octopuses.Count(b =>
                            (o.X == b.X - 1 && o.Y == b.Y - 1 && b.Flash == true) ||
                            (o.X == b.X - 1 && o.Y == b.Y + 1 && b.Flash == true) ||
                            (o.X == b.X - 1 && o.Y == b.Y && b.Flash == true) ||
                            (o.X == b.X + 1 && o.Y == b.Y - 1 && b.Flash == true) ||
                            (o.X == b.X + 1 && o.Y == b.Y + 1 && b.Flash == true) ||
                            (o.X == b.X + 1 && o.Y == b.Y && b.Flash == true) ||
                            (o.X == b.X && o.Y == b.Y - 1 && b.Flash == true) ||
                            (o.X == b.X && o.Y == b.Y + 1 && b.Flash == true))); return o;
                }).ToList();

                //Store number off flashes
                totalFlashes += octopuses.Count(x => x.Flash == true);
            }

           return (totalFlashes, octopuses);

        }

  
        public List<Octopus> ReadInput(string input)
        {
            List<Octopus> octopuses = new List<Octopus>();

            int y = 0;

            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                int x = 0;
                foreach (char l in line)
                {
                    Octopus octopus = new Octopus();

                    octopus.Engergy = Int32.Parse(l.ToString());
                    octopus.X = x;
                    octopus.Y = y;
                    octopus.Flash = false;

                    octopuses.Add(octopus);

                    x++;
                }
                y++;
            }

            return octopuses;
        }

        public struct Octopus
        {
            public int Engergy;
            public int X;
            public int Y;
            public bool Flash;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day11();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }


}
