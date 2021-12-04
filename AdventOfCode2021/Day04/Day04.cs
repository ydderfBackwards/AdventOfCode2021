using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2021
{
    public class Day04
    {
        public string SolvePart1(string input)
        {
            (int[] drawNumbers, List<Board> boards) = ReadInput(input);

            int boardSize = 5;
            int round = 0;
            int winningBoardNr = -1;
            bool bingo = false;

            //Play until bingo
            while (bingo == false)
            {
                winningBoardNr = MarkNumber(drawNumbers[round], boards);

                if (winningBoardNr < 0)
                    round++;
                else
                    bingo = true;

            }

            //Get total off not marked numbers
            int total = 0;
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    if (boards[winningBoardNr].marked[x, y] == false)
                        total += boards[winningBoardNr].numbers[x, y];
                }
            }

            int result = total * drawNumbers[round];

            return result.ToString();
        }



        public string SolvePart2(string input)
        {
            int boardSize = 5;
            int round = 0;
            int winningBoardNr = -1;

            //Read input
            (int[] drawNumbers, List<Board> boards) = ReadInput(input);

            //Play until one left
            while (boards.Count > 1)
            {
                winningBoardNr = MarkNumber(drawNumbers[round], boards);

                if (winningBoardNr < 0)
                    round++;
                else
                    boards.RemoveAt(winningBoardNr);
            }

            //Keep playing until last board wins
            winningBoardNr = -1;
            while (winningBoardNr < 0)
            {
                round++;
                winningBoardNr = MarkNumber(drawNumbers[round], boards);
            }

            //Get total off not marked numbers for the last board
            int total = 0;
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    if (boards[0].marked[x, y] == false)
                        total += boards[0].numbers[x, y];
                }
            }

            int result = total * drawNumbers[round];

            return result.ToString();




        }

        public int MarkNumber(int drawnNumber, List<Board> boards)
        {
            int boardSize = 5;
            int winningBoardNr = -1;

            //Mark number
            foreach (Board board in boards)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    for (int y = 0; y < boardSize; y++)
                    {
                        if (board.numbers[x, y] == drawnNumber)
                            board.marked[x, y] = true;
                    }
                }
            }

            int countA = 0;
            int countB = 0;

            //check for winner
            for (int i = 0; i < boards.Count(); i++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    countA = 0;
                    countB = 0;
                    for (int y = 0; y < boardSize; y++)
                    {
                        //Check for rows and columns
                        if (boards[i].marked[x, y] == true)
                            countA++;
                        if (boards[i].marked[y, x] == true)
                            countB++;
                    }

                    //Check if row or column is full
                    if (countA == boardSize || countB == boardSize)
                    {
                        winningBoardNr = i;
                        goto foundWinner;
                    }
                }
            }

        foundWinner:
            return winningBoardNr;
        }



        public (int[] drawNumbers, List<Board> boards) ReadInput(string input)
        {
            int boardSize = 5;

            string[] lines = input.Split(Environment.NewLine);

            //Drawn numbers are located on first line
            int[] drawNumber = Array.ConvertAll(lines[0].Split(','), Int32.Parse);

            int nrOffBoards = (lines.Length - 1) / (boardSize + 1);

            List<Board> boards = new List<Board>();


            //Loop to board data
            for (int i = 2; i < lines.Length; i += (boardSize + 1))
            {
                Board thisBoard = new Board();
                thisBoard.numbers = new int[boardSize, boardSize];
                thisBoard.marked = new bool[boardSize, boardSize];

                for (int y = 0; y < boardSize; y++)
                {
                    int[] line = Array.ConvertAll(lines[i + y].Split(' ', StringSplitOptions.RemoveEmptyEntries), Int32.Parse); //remove double spaces

                    for (int x = 0; x < boardSize; x++)
                    {
                        thisBoard.numbers[x, y] = line[x];
                        thisBoard.marked[x, y] = false;
                    }
                }

                boards.Add(thisBoard);
            }

            return (drawNumber, boards);
        }

        public struct Board
        {
            public int[,] numbers;
            public bool[,] marked;
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day04();
            return (testInput) ? myInput.testInput : myInput.input;
        }



    }



}
