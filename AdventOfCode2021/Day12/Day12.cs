using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day12
    {
        List<string> foundPath = new List<string>();

        public string SolvePart1(string input)
        {
            var AvailablePaths = ReadInput(input);

            var nrOfPaths = FindNrOfPaths(AvailablePaths, "start");

            return nrOfPaths.ToString();
        }



        public string SolvePart2(string input)
        {
            var AvailablePaths = ReadInput(input);

            List<string> visitedSmall = new List<string>();

            var nrOfPaths = FindNrOfPathsPart2(AvailablePaths, "start", visitedSmall);



            return nrOfPaths.ToString();
        }


        public int FindNrOfPathsPart2(List<Path> AvailablePaths, string From, List<string> visitedSmall)
        {
            int nrOfPaths = 0;

            if (IsSmallCave(From))
            {
                visitedSmall.Add(From);

                var tmp = visitedSmall.Distinct().ToList();

                //Check if not more than one small cave is visited twice.
                if ((visitedSmall.Count() - tmp.Count()) > 1 )
                {
                    nrOfPaths = 0;
                    goto End;
                }
            }

            var checkPaths = AvailablePaths.Where(x => x.From.Equals(From)).ToList();

            //Find next part of the path
            if (checkPaths.Count() > 0)
            {
                //For each possible path
                foreach (Path path in checkPaths)
                {
                    if (path.To.Equals("end"))
                    {
                        //Found the end
                        nrOfPaths += 1;
                    }
                    else
                    {
                        nrOfPaths += FindNrOfPathsPart2(AvailablePaths.ToList(), path.To, visitedSmall.ToList());
                    }
                }
            }
            else
            {
                //This path is a dead end
                nrOfPaths = 0;
            }
        End:;
            return nrOfPaths;
        }


        public int FindNrOfPaths(List<Path> AvailablePaths, string From)
        {
            int nrOfPaths = 0;

            //Remove small caves so we can not access them twice or more
            if (IsSmallCave(From))
            {
                AvailablePaths.RemoveAll(x => x.To.Equals(From));
            }

            //Find next part of the path
            if (AvailablePaths.Any(x => x.From.Equals(From)))
            {
                var checkPaths = AvailablePaths.Where(x => x.From.Equals(From)).ToList();
                //For each possible path
                foreach (Path path in checkPaths)
                {
                    if (path.To.Equals("end"))
                    {
                        //Found the end
                        nrOfPaths += 1;
                    }
                    else
                    {
                        nrOfPaths += FindNrOfPaths(AvailablePaths.ToList(), path.To);
                    }
                }
            }
            else
            {
                //This path is a dead end
                nrOfPaths = 0;
            }

            return nrOfPaths;
        }

        public bool IsSmallCave(string cave)
        {

            return Char.IsLower(cave[0]) && !string.Equals(cave, "start");
        }

        public List<Path> ReadInput(string input)
        {
            List<Path> paths = new List<Path>();

            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                string[] data = line.Split("-");

                Path path = new Path();

                path.From = data[0];
                path.To = data[1];

                paths.Add(path);

                path = new Path();

                path.From = data[1];
                path.To = data[0];

                paths.Add(path);


            }

            paths.Distinct();
            paths.RemoveAll(x => x.From.Equals("end"));
            paths.RemoveAll(x => x.To.Equals("start"));

            return paths;
        }


        public class Path
        {
            public string From;
            public string To;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day12();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
