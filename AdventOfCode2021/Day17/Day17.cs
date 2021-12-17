using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day17
    {
        public string SolvePart1(string input)
        {
            var area = ReadInput(input);
            var maxHeight = DetermineMaxHight(area);

            return maxHeight.ToString();
        }


        public string SolvePart2(string input)
        {
            var area = ReadInput(input);
            var nrOfHit = DetermineNrOfTrajectory(area);

            return nrOfHit.ToString();
        }

        public int DetermineNrOfTrajectory(Area area)
        {
            int maxY = Math.Abs(area.minY) - 1; 

            int nrOfHit = 0;

            for (int x = 0; x <= area.maxX; x++) //If the start speed is bigger than the target area, then no need to check.
            {
                for (int y = area.minY; y <= maxY; y++) //Only check a usefull speed. 
                {
                    if (HitTarget(area, x, y))
                        nrOfHit++;
                }
            }


            return nrOfHit;
        }

        public bool HitTarget(Area area, int speedX, int speedY)
        {
            bool hitTarget = false;
            bool passedTarget = false;
            int currentX = 0;
            int currentY = 0;

            while (!hitTarget && !passedTarget)
            {
                currentX += speedX;
                currentY += speedY;

                speedX--;
                if (speedX < 0)
                    speedX = 0;

                speedY--;

                if (currentY < area.minY || currentX > area.maxX)
                {
                    passedTarget = true;
                }

                if (currentY >= area.minY && currentY <= area.maxY && currentX >= area.minX && currentX <= area.maxX)
                {
                    hitTarget = true;
                }

            }

            return hitTarget;
        }


        public int DetermineMaxHight(Area area)
        {
            //Asumption: X is a don't care. Because we go so high, the object will fall in a streight line (speed x=0) 
            // before it will reach the target area.
            //
            //We always go to position y=0 on the fall down. At the max speed (for max height), we will reach the next step
            //the bottom point of the area. So the startspeed y will be the lowest point of the area minus one. 

            int startSpeed = Math.Abs(area.minY) - 1;

            int actHeight = 0;

            int speed = startSpeed;

            while (speed > 0)
            {
                actHeight += speed;
                speed--;

            }

            return actHeight;
        }
    
        public Area ReadInput(string input)
        {
            Area area = new Area();

            string[] splitOptions = { "=", "..", "," };
            string[] data = input.Split(splitOptions, StringSplitOptions.RemoveEmptyEntries);

            area.minX = int.Parse(data[1]);
            area.maxX = int.Parse(data[2]);
            area.minY = int.Parse(data[4]);
            area.maxY = int.Parse(data[5]);

            return area;
        }

        public struct Area
        {

            public int minX;
            public int maxX;
            public int minY;
            public int maxY;

        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day17();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
