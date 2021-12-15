using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day15
    {
        Dictionary<Position, RouteInfo> riskMap = new Dictionary<Position, RouteInfo>();
        Dictionary<Position, int> chitonsMap = new Dictionary<Position, int>();

        public string SolvePart1(string input)
        {
            chitonsMap = ReadInput(input);
            InitRiskMap(chitonsMap);
            FillRiskMap();

            return riskMap.Last().Value.Risk.ToString(); ;
        }



        public string SolvePart2(string input)
        {
            chitonsMap = ReadInput(input);
            ExtendMap();

            InitRiskMap(chitonsMap);
            FillRiskMap();

            return riskMap.Last().Value.Risk.ToString(); ;
        }

        public void ExtendMap()
        {
            int maxX = chitonsMap.Max(x => x.Key.X) + 1;
            int maxY = chitonsMap.Max(x => x.Key.Y) + 1;

            //Expand to X direction
            foreach (var item in chitonsMap.ToList())
            {
                int newVal = item.Value;
                for (int i = 1; i < 5; i++)
                {
                    Position newPos = new Position();
                    newPos.X = item.Key.X + (maxX * i);
                    newPos.Y = item.Key.Y;
                    newVal++;
                    if (newVal > 9)
                        newVal = 1;

                    chitonsMap.Add(newPos, newVal);

                }
            }

            //Expand to Y direction
            foreach (var item in chitonsMap.ToList())
            {
                int newVal = item.Value;
                for (int i = 1; i < 5; i++)
                {
                    Position newPos = new Position();
                    newPos.Y = item.Key.Y + (maxY * i);
                    newPos.X = item.Key.X;
                    newVal++;
                    if (newVal > 9)
                        newVal = 1;

                    chitonsMap.Add(newPos, newVal);
                }
            }
        }

        public void FillRiskMap()
        {

            Position actualPosition = new Position();

            actualPosition.X = 0;
            actualPosition.Y = 0;

            int nrOfPos = riskMap.Count();

            while (nrOfPos > 0)
            {
                Position PositionUp = new Position();
                Position PositionDown = new Position();
                Position PositionLeft = new Position();
                Position PositionRight = new Position();

                PositionUp.X = actualPosition.X;
                PositionUp.Y = actualPosition.Y - 1;
                PositionDown.X = actualPosition.X;
                PositionDown.Y = actualPosition.Y + 1;
                PositionLeft.X = actualPosition.X - 1;
                PositionLeft.Y = actualPosition.Y;
                PositionRight.X = actualPosition.X + 1;
                PositionRight.Y = actualPosition.Y;

                //Update riskmap for neighbors.
                UpdateRisk(actualPosition, PositionUp);
                UpdateRisk(actualPosition, PositionDown);
                UpdateRisk(actualPosition, PositionLeft);
                UpdateRisk(actualPosition, PositionRight);

                //Update visited
                UpdateVisited(actualPosition);

                actualPosition = FindNextPostion();
                nrOfPos--;
            }

        }

        public Position FindNextPostion()
        {
            return riskMap.Where(x => x.Value.visited == false).OrderBy(x => x.Value.Risk).FirstOrDefault().Key;
        }
        public void UpdateRisk(Position actualPosition, Position UpdatePosition)
        {
            if (riskMap.ContainsKey(UpdatePosition))
            {
                RouteInfo info = new RouteInfo();
                info.visited = riskMap[UpdatePosition].visited;

                info.Risk = riskMap[UpdatePosition].Risk < (riskMap[UpdatePosition].Risk + chitonsMap[UpdatePosition]) ? riskMap[UpdatePosition].Risk : (riskMap[actualPosition].Risk + chitonsMap[UpdatePosition]);

                riskMap[UpdatePosition] = info;
            }
        }

        public void UpdateVisited(Position actualPosition)
        {
            RouteInfo info = new RouteInfo();

            info.visited = true;
            info.Risk = riskMap[actualPosition].Risk;

            riskMap[actualPosition] = info;
        }

        public void InitRiskMap(Dictionary<Position, int> chitonsMap)
        {
            riskMap = new Dictionary<Position, RouteInfo>();
            RouteInfo info = new RouteInfo();
            info.Risk = long.MaxValue;
            info.visited = false;

            //initialize riskmap on max risk for every pos 
            foreach (var pos in chitonsMap)
            {
                Position thisPos = new Position();
                thisPos.X = pos.Key.X;
                thisPos.Y = pos.Key.Y;

                riskMap.Add(thisPos, info);
            }



            Position startPos = new Position();

            startPos.X = 0;
            startPos.Y = 0;
            info.Risk = 0;
            info.visited = false;

            //Initialize startposition on zero risk
            riskMap[startPos] = info;

        }

        public struct RouteInfo
        {
            public long Risk;
            public bool visited;
        }



        public Dictionary<Position, int> ReadInput(string input)
        {
            Dictionary<Position, int> positions = new Dictionary<Position, int>();

            int y = 0;

            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                int x = 0;
                foreach (char l in line)
                {
                    Position position = new Position();

                    int riskLevel = Int32.Parse(l.ToString());
                    position.X = x;
                    position.Y = y;


                    positions.Add(position, riskLevel);

                    x++;
                }
                y++;
            }

            return positions;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day15();
            return (testInput) ? myInput.testInput : myInput.input;
        }

        public struct Position
        {
            public int X;
            public int Y;
        }

    }



}
