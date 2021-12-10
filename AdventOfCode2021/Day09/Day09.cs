using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day09
    {
        public string SolvePart1(string input)
        {

            var locations = ReadInput(input);

            var count = LevelOfLowPoints(locations);

            return count.ToString();
        }



        public string SolvePart2(string input)
        {

            var locations = ReadInput(input);

            var result = FindBasins(locations);

            return result.ToString();
        }


        public long FindBasins(List<Location> locations)
        {
            List<int> baySizes = new List<int>();

            //Remove all High locations
            locations.RemoveAll(x => x.Height == 9);

            //Search until all locations are handled
            while (locations.Count() > 0)
            {
                int lastCount = locations.Count() + 1;
                List<Location> thisBay = new List<Location>();

                //Start with the first location we find
                thisBay.Add(locations[0]);
                locations.RemoveAt(0);

                //Unit someting found for this bay.
                while (locations.Count() != lastCount)
                {
                    lastCount = locations.Count();

                    //Find nearby locations
                    var bayLocations = locations.Where(a => thisBay.Any(b =>
                                                                    (a.X == b.X - 1 && a.Y == b.Y) ||
                                                                    (a.X == b.X + 1 && a.Y == b.Y) ||
                                                                    (a.X == b.X && a.Y == b.Y - 1) ||
                                                                    (a.X == b.X && a.Y == b.Y + 1)
                                                                    ));

                    //Store found locations for this bay
                    thisBay.AddRange(bayLocations);

                    //Remove found locations
                    locations.RemoveAll(a => thisBay.Any(b => a.Equals(b)));

                }

                //Store bay size
                baySizes.Add(thisBay.Count());

            }


            var sortedList = baySizes.OrderByDescending(i => i).ToList();

            long result = sortedList[0] * sortedList[1] * sortedList[2];

            return result;
        }

        public int LevelOfLowPoints(List<Location> locations)
        {
            int count = 0;

            int minX = 0;
            int maxX = locations.Max(x => x.X);
            int minY = 0;
            int maxY = locations.Max(x => x.Y);


            foreach (Location location in locations)
            {
                int thisHeight = location.Height;
                int thisX = location.X;
                int thisY = location.Y;
                int checkValue = 4;

                int nrLow = locations.Count(x => x.Height > thisHeight && (
                                                  (x.X == thisX - 1 && x.Y == thisY) ||
                                                  (x.X == thisX + 1 && x.Y == thisY) ||
                                                  (x.X == thisX && x.Y == thisY - 1) ||
                                                  (x.X == thisX && x.Y == thisY + 1)));

                //Correct for border locations
                if (thisX == minX)
                    checkValue--;
                if (thisX == maxX)
                    checkValue--;
                if (thisY == minY)
                    checkValue--;
                if (thisY == maxY)
                    checkValue--;


                if (nrLow == checkValue)
                    count += thisHeight + 1;

            }


            return count;
        }

        public List<Location> ReadInput(string input)
        {
            List<Location> locations = new List<Location>();

            int y = 0;

            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                int x = 0;
                foreach (char l in line)
                {
                    Location location = new Location();

                    location.Height = Int32.Parse(l.ToString());
                    location.X = x;
                    location.Y = y;

                    locations.Add(location);

                    x++;
                }

                y++;
            }

            return locations;

        }

        public struct Location
        {
            public int Height;
            public int X;
            public int Y;
        }
        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day09();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
