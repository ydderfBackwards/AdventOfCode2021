using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex


namespace AdventOfCode2021
{
    public class Day19
    {
        int minNrOfMaches = 12;
        List<Bacon> completeMap = new List<Bacon>();
        List<List<Bacon>> scanners = new List<List<Bacon>>();
        List<Bacon> foundScanners = new List<Bacon>();
        public string SolvePart1(string input)
        {
            ReadInput(input);

            FillMap();

            var LongestDistance = FindLongestDistance();

            var total = completeMap.Distinct().ToList();
            var totalBacons = total.Count().ToString();


            return "total bacons: " + totalBacons.ToString() + " Longest distance: " + LongestDistance.ToString();
        }



        public string SolvePart2(string input)
        {



            return "Handled in part 1";
        }
        public long FindLongestDistance()
        {
            List<long> distance = new List<long>();

            foreach (var scanA in foundScanners)
            {
                foreach (var scanB in foundScanners)
                {
                    long thisDistance = Math.Abs((scanA.X - scanB.X)) + Math.Abs((scanA.Y - scanB.Y)) + Math.Abs((scanA.Z - scanB.Z));
                    distance.Add(thisDistance);
                }
            }

            return distance.Max();
        }
        public void FillMap()
        {

            //Copy the bacons from the first scanner to the map
            foreach (Bacon bacon in scanners[0])
            {
                completeMap.Add(bacon);
            }

            //Remove this scanner from list
            scanners.RemoveAt(0);

            Bacon firstScanner = new Bacon();
            firstScanner.X = 0;
            firstScanner.Y = 0;
            firstScanner.Z = 0;

            foundScanners.Add(firstScanner);

            int orientation = 1;

            //Keep trying until all bacons are placed on the complete map
            while (scanners.Count() > 0)
            {
                foreach (var scanner in scanners.ToList())
                {
                    MatchBacon(scanner);
                }
                //---- Orientation:
                // 1 = side 1
                // 2 = side 1 rotate 90
                // 3 = side 1 rotate 180
                // 4 = side 1 rotate 270
                // 5 = side 2
                // 6 = side 2 rotate 90
                // 7 = side 2 rotate 180
                // 8 = side 2 rotate 270
                // 9 = side 3
                // 10 = side 3 rotate 90
                // 11 = side 3 rotate 180
                // 12 = side 3 rotate 270
                // 13 = side 4
                // 14 = side 4 rotate 90
                // 15 = side 4 rotate 180
                // 16 = side 4 rotate 270
                // 17 = side 5
                // 18 = side 5 rotate 90
                // 19 = side 5 rotate 180
                // 20 = side 5 rotate 270
                // 21 = side 6
                // 22 = side 6 rotate 90
                // 23 = side 6 rotate 180
                // 24 = side 6 rotate 270

                if (orientation <= 24)
                {
                    Rotate_X_90degree();
                }

                if (orientation == 4 || orientation == 8 || orientation == 12 || orientation == 16)
                {
                    Rotate_Z_90degree(); //Show another side
                }

                if (orientation == 16 || orientation == 24)
                {
                    Rotate_Y_90degree(); //Show another side
                }

                if (orientation == 20)
                {
                    Rotate_Y_90degree(); //Show another side
                    Rotate_Y_90degree(); //Show another side
                }


                orientation++;
                if (orientation > 24)
                {
                    orientation = 1;
                }

            }
        }

        public void Rotate_Y_90degree()
        {

            List<List<Bacon>> newScanners = new List<List<Bacon>>();

            foreach (var scanner in scanners)
            {
                List<Bacon> newScanner = new List<Bacon>();

                foreach (var bacon in scanner)
                {
                    Bacon newBacon = new Bacon();
                    newBacon.X = bacon.Z;
                    newBacon.Y = bacon.Y;
                    newBacon.Z = -bacon.X;

                    newScanner.Add(newBacon);
                }

                newScanners.Add(newScanner);
            }

            scanners = newScanners;
        }
        public void Rotate_Z_90degree()
        {

            List<List<Bacon>> newScanners = new List<List<Bacon>>();

            foreach (var scanner in scanners)
            {
                List<Bacon> newScanner = new List<Bacon>();

                foreach (var bacon in scanner)
                {
                    Bacon newBacon = new Bacon();
                    newBacon.X = -bacon.Y;
                    newBacon.Y = bacon.X;
                    newBacon.Z = bacon.Z;

                    newScanner.Add(newBacon);
                }

                newScanners.Add(newScanner);
            }

            scanners = newScanners;
        }
        public void Rotate_X_90degree()
        {

            List<List<Bacon>> newScanners = new List<List<Bacon>>();

            foreach (var scanner in scanners)
            {
                List<Bacon> newScanner = new List<Bacon>();

                foreach (var bacon in scanner)
                {
                    Bacon newBacon = new Bacon();
                    newBacon.X = bacon.X;
                    newBacon.Y = -bacon.Z;
                    newBacon.Z = bacon.Y;

                    newScanner.Add(newBacon);
                }

                newScanners.Add(newScanner);
            }



            scanners = newScanners;
        }
        public void MatchBacon(List<Bacon> scanner)
        {
            List<Offset> offsets = new List<Offset>();

            //Find the offset off all bacons of this scanner, to all bacons in the complete map.
            foreach (var bacon in scanner)
            {
                foreach (var mapBacon in completeMap)
                {
                    Offset offset = new Offset();
                    offset.OffsetX = bacon.X - mapBacon.X;
                    offset.OffsetY = bacon.Y - mapBacon.Y;
                    offset.OffsetZ = bacon.Z - mapBacon.Z;

                    offsets.Add(offset);
                }
            }

            //Group items with same offset in all axis. 
            var groupedOffsets = from x in offsets
                                 group x by new
                                 {
                                     x.OffsetX,
                                     x.OffsetY,
                                     x.OffsetZ

                                 } into g
                                 let count = g.Count()
                                 orderby count descending
                                 select new { Name = g.Key, Count = count, X = g.First().OffsetX, Y = g.First().OffsetY, Z = g.First().OffsetZ };

            if (groupedOffsets.Count() > 0)
            {
                if (groupedOffsets.First().Count >= minNrOfMaches)
                {
                    // Console.WriteLine("match found");

                    //Copy the bacons from the found scanner to the map
                    foreach (Bacon bacon in scanner)
                    {
                        Bacon baconToAdd = new Bacon();

                        //Add offset
                        baconToAdd.X = bacon.X - groupedOffsets.First().X;
                        baconToAdd.Y = bacon.Y - groupedOffsets.First().Y;
                        baconToAdd.Z = bacon.Z - groupedOffsets.First().Z;

                        completeMap.Add(baconToAdd);
                    }

                    scanners.RemoveAt(scanners.IndexOf(scanner));

                    completeMap = completeMap.Distinct().ToList();

                    //Store location of found scanner
                    Bacon scannerToAdd = new Bacon();

                    scannerToAdd.X = groupedOffsets.First().X;
                    scannerToAdd.Y = groupedOffsets.First().Y;
                    scannerToAdd.Z = groupedOffsets.First().Z;

                    foundScanners.Add(scannerToAdd);
                }
            }
        }


        public void ReadInput(string input)
        {
            // List<List<Bacon>> scanners = new List<List<Bacon>>();
            List<Bacon> scanner = new List<Bacon>();

            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (line.StartsWith("---"))
                {
                    if (scanner.Count() > 0)
                    {
                        scanners.Add(scanner);
                    }
                    scanner = new List<Bacon>();
                }
                else
                {
                    string[] data = line.Split(",");

                    Bacon bacon = new Bacon();
                    bacon.X = int.Parse(data[0]);
                    bacon.Y = int.Parse(data[1]);
                    bacon.Z = int.Parse(data[2]);

                    scanner.Add(bacon);

                }
            }
            scanners.Add(scanner);

            // return scanners;
        }

        public struct Bacon
        {
            public int X;
            public int Y;
            public int Z;
        }

        public struct Offset
        {
            public int OffsetX;
            public int OffsetY;
            public int OffsetZ;

        }
        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day19();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
