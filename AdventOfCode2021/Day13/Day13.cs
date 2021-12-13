using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day13
    {
        public string SolvePart1(string input)
        {
            var (foldinstructions, paper) = ReadInput(input);

            var paperAfter = FoldPaper(foldinstructions, paper, true);

            var count = paperAfter.Count(p => p.value > 0);

            return count.ToString();
        }



        public string SolvePart2(string input)
        {
            var (foldinstructions, paper) = ReadInput(input);

            var paperAfter = FoldPaper(foldinstructions, paper, false);

            PrintPaper(paperAfter);

            return "See above!";
        }

        public void PrintPaper(List<Position> paper)
        {
            int xMax = paper.Max(v => v.X);
            int yMax = paper.Max(v => v.Y);

            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    char toPrint = ' ';
                    if (paper.Exists(p => p.X == x && p.Y == y))
                    {
                        toPrint = '#';
                    }
                    Console.Write(toPrint);
                }
                Console.WriteLine();
            }
        }

        public List<Position> FoldPaper(List<FoldInstructions> foldInstructions, List<Position> paper, bool FoldOnce)
        {
            foreach (FoldInstructions foldInstr in foldInstructions)
            {
                if (foldInstr.Axis.Equals("y"))
                {
                    foreach (Position position in paper.ToList())
                    {
                        int offSet = position.Y - foldInstr.Position;
                        int newPos = foldInstr.Position - offSet;

                        if (position.Y > foldInstr.Position)
                        {
                            if (!paper.Exists(p => p.X == position.X && p.Y == newPos))
                            {
                                Position newPosPaper = new Position();
                                newPosPaper.X = position.X;
                                newPosPaper.Y = newPos;
                                newPosPaper.value = 1;
                                paper.Add(newPosPaper);
                            }
                        }
                    }

                    paper.RemoveAll(p => p.Y >= foldInstr.Position);
                }
                else
                {
                    foreach (Position position in paper.ToList())
                    {
                        int offSet = position.X - foldInstr.Position;
                        int newPos = foldInstr.Position - offSet;

                        if (position.X > foldInstr.Position)
                        {
                            if (!paper.Exists(p => p.Y == position.Y && p.X == newPos))
                            {
                                Position newPosPaper = new Position();
                                newPosPaper.Y = position.Y;
                                newPosPaper.X = newPos;
                                newPosPaper.value = 1;
                                paper.Add(newPosPaper);
                            }
                        }
                    }

                    paper.RemoveAll(p => p.X >= foldInstr.Position);
                }

                if (FoldOnce)
                    break;
            }

            return paper;
        }

        public (List<FoldInstructions>, List<Position>) ReadInput(string input)
        {
            List<Position> paper = new List<Position>();
            List<FoldInstructions> foldInstructions = new List<FoldInstructions>();
            bool partTwo = false;

            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {

                if (line.Equals(Environment.NewLine) || line.Equals(""))
                {
                    partTwo = true;
                }
                else
                {
                    if (partTwo)
                    {
                        string[] seperators = { ",", " ", "=" };
                        string[] data = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        FoldInstructions foldInstruction = new FoldInstructions();

                        foldInstruction.Axis = data[2];
                        foldInstruction.Position = Int32.Parse(data[3]);

                        foldInstructions.Add(foldInstruction);

                    }
                    else
                    {
                        string[] data = line.Split(",");

                        Position position = new Position();
                        position.X = Int32.Parse(data[0]);
                        position.Y = Int32.Parse(data[1]);
                        position.value = 1;

                        paper.Add(position);
                    }
                }

            }

            return (foldInstructions, paper);
        }

        public struct Position
        {
            public int X;
            public int Y;
            public int value;
        }
        public struct FoldInstructions
        {
            public string Axis;
            public int Position;
        }
        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day13();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
