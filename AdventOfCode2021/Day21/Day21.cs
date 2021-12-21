using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day21
    {
        public string SolvePart1(string input)
        {
            var (player1, player2) = ReadInput(input);

            var points = PlayGame(player1, player2);

            return points.ToString();
        }


        public string SolvePart2(string input)
        {
            var (player1, player2) = ReadInput(input);

            var points = PlayGameDiracDice(player1, player2);

            return points.ToString();


        }

        public long PlayGame(long pos_p1, long pos_p2)
        {
            long dice = 6;
            long nrOfRolls = 0;
            bool toggle = false;
            long score_p1 = 0;
            long score_p2 = 0;


            while (score_p1 < 1000 && score_p2 < 1000)
            {
                if (toggle == false)
                {
                    pos_p1 = MoveToSpace(pos_p1, dice);
                    score_p1 += pos_p1;
                }
                else
                {
                    pos_p2 = MoveToSpace(pos_p2, dice);
                    score_p2 += pos_p2;
                }

                dice += 9;
                nrOfRolls += 3;
                toggle = !toggle;
            }


            long scoreLoser = score_p1 > score_p2 ? score_p2 : score_p1;

            return scoreLoser * nrOfRolls;
        }

        public long PlayGameDiracDice(long pos_p1, long pos_p2)
        {
            Player p1 = new Player();
            Player p2 = new Player();


            p1.score = 0;
            p1.pos = pos_p1;
            p2.score = 0;
            p2.pos = pos_p2;

            long p1_wins = 0;
            long p2_wins = 0;

            //Possible dice total and how often that is possible:
            //3 ==> 1 time
            //4 ==> 3 time
            //5 ==> 6 time
            //6 ==> 7 time
            //7 ==> 6 time
            //8 ==> 3 time
            //9 ==> 1 time
            //
            // So don't calculate all these same options separate, but multiply the result with the number of possibilities. 

            (var p1_u, var p2_u) = PlayUniverse(p1, p2, false, 3);
            p1_wins += p1_u * 1;
            p2_wins += p2_u * 1;

            (p1_u, p2_u) = PlayUniverse(p1, p2, false, 4);
            p1_wins += p1_u * 3;
            p2_wins += p2_u * 3;

            (p1_u, p2_u) = PlayUniverse(p1, p2, false, 5);
            p1_wins += p1_u * 6;
            p2_wins += p2_u * 6;

            (p1_u, p2_u) = PlayUniverse(p1, p2, false, 6);
            p1_wins += p1_u * 7;
            p2_wins += p2_u * 7;

            (p1_u, p2_u) = PlayUniverse(p1, p2, false, 7);
            p1_wins += p1_u * 6;
            p2_wins += p2_u * 6;

            (p1_u, p2_u) = PlayUniverse(p1, p2, false, 8);
            p1_wins += p1_u * 3;
            p2_wins += p2_u * 3;

            (p1_u, p2_u) = PlayUniverse(p1, p2, false, 9);
            p1_wins += p1_u * 1;
            p2_wins += p2_u * 1;



            return p1_wins < p2_wins ? p2_wins : p1_wins;
        }

        public (long, long) PlayUniverse(Player p1, Player p2, bool player, long dice)
        {

            long p1_wins = 0;
            long p2_wins = 0;

            if (player == false)
            {
                p1.pos = MoveToSpace(p1.pos, dice);
                p1.score += p1.pos;

            }
            else
            {
                p2.pos = MoveToSpace(p2.pos, dice);
                p2.score += p2.pos;
            }

            player = !player;

            if (p1.score < 21 && p2.score < 21)
            {
                (var p1_u, var p2_u) = PlayUniverse(p1, p2, player, 3);
                p1_wins += p1_u * 1;
                p2_wins += p2_u * 1;

                (p1_u, p2_u) = PlayUniverse(p1, p2, player, 4);
                p1_wins += p1_u * 3;
                p2_wins += p2_u * 3;

                (p1_u, p2_u) = PlayUniverse(p1, p2, player, 5);
                p1_wins += p1_u * 6;
                p2_wins += p2_u * 6;

                (p1_u, p2_u) = PlayUniverse(p1, p2, player, 6);
                p1_wins += p1_u * 7;
                p2_wins += p2_u * 7;

                (p1_u, p2_u) = PlayUniverse(p1, p2, player, 7);
                p1_wins += p1_u * 6;
                p2_wins += p2_u * 6;

                (p1_u, p2_u) = PlayUniverse(p1, p2, player, 8);
                p1_wins += p1_u * 3;
                p2_wins += p2_u * 3;

                (p1_u, p2_u) = PlayUniverse(p1, p2, player, 9);
                p1_wins += p1_u * 1;
                p2_wins += p2_u * 1;
            }
            else
            {
                if (p1.score > p2.score)
                {
                    p1_wins = 1;
                    p2_wins = 0;

                }
                else
                {
                    p1_wins = 0;
                    p2_wins = 1;

                }
            }

            return (p1_wins, p2_wins);
        }

        public struct Player
        {
            public long pos;
            public long score;

        }
        public long MoveToSpace(long pos, long dice)
        {

            pos += dice;

            pos = pos % 10;

            //Pos 0 should be pos 10
            pos = pos < 1 ? 10 : pos;

            return pos;
        }

        public (long, long) ReadInput(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            string[] data = lines[0].Split(" ");
            long player1 = long.Parse(data[data.Length - 1]);

            data = lines[1].Split(" ");
            long player2 = long.Parse(data[data.Length - 1]);

            return (player1, player2);
        }
        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day21();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
