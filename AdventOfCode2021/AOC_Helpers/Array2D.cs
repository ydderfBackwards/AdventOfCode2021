using System;
using System.IO;
using System.Linq;


namespace AOC.Helpers
{
    public static class Array2D
    {
        /******************************************************************************************************************/
        /******************************************* ROTATE ARRAY *********************************************************/
        /******************************************************************************************************************/

        public static bool[,] RotateCW(bool[,] source, int degree = 90)
        {
            /***************************** Rotates a matrix ****************************/
            /* Inputs:
            /*          -int[,] source. The array to rotate. 
            /*          -int degrees to rotate (90 or 180 or 270), default: 90
            /*
            /* Outputs:
            /*          -int[,] destination. The rotated array.
            /*
            /*
            /* Example:
            /*          input:          rotate 90 degree        output:
            /*              123456                                  71
            /*              778899             ==>                  72   
            /*                                                      83
            /*                                                      84
            /*                                                      95
            /*                                                      96
            /*
            /*          input:          rotate 180 degree       output:
            /*              123456                                  998877
            /*              778899             ==>                  654321   
            /*
            /*          input:          rotate 270 degree      output:
            /*              123456                                  69
            /*              778899             ==>                  59   
            /*                                                      48
            /*                                                      38
            /*                                                      27
            /*                                                      17
            /*
            /***************************************************************************/

            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);
            int newSizeX = sizeX;
            int newSizeY = sizeY;

            if (degree > 360)
            {
                degree = degree % 360;
            }

            if (degree == 90 || degree == 270)
            {
                newSizeX = sizeY;
                newSizeY = sizeX;
            }

            bool[,] destination = new bool[newSizeX, newSizeY];

            switch (degree)
            {
                case 0:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[x, y] = source[x, y]; //Just return a copy of the source array
                        }
                    }
                    break;

                case 90:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[newSizeX - y - 1, x] = source[x, y];
                        }
                    }
                    break;

                case 180:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[newSizeX - x - 1, newSizeY - y - 1] = source[x, y];
                        }
                    }
                    break;

                case 270:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[y, newSizeY - x - 1] = source[x, y];
                        }
                    }
                    break;

                default:
                    Console.WriteLine("ERROR in function: Array2D.RotateCW(). Invalid degrees.");
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[x, y] = source[x, y]; //Just return a copy of the source array
                        }
                    }
                    break;
            }




            return destination;
        }


        public static int[,] RotateCW(int[,] source, int degree = 90)
        {
            /***************************** Rotates a matrix ****************************/
            /* Inputs:
            /*          -int[,] source. The array to rotate. 
            /*          -int degrees to rotate (90 or 180 or 270), default: 90
            /*
            /* Outputs:
            /*          -int[,] destination. The rotated array.
            /*
            /*
            /* Example:
            /*          input:          rotate 90 degree        output:
            /*              123456                                  71
            /*              778899             ==>                  72   
            /*                                                      83
            /*                                                      84
            /*                                                      95
            /*                                                      96
            /*
            /*          input:          rotate 180 degree       output:
            /*              123456                                  998877
            /*              778899             ==>                  654321   
            /*
            /*          input:          rotate 270 degree      output:
            /*              123456                                  69
            /*              778899             ==>                  59   
            /*                                                      48
            /*                                                      38
            /*                                                      27
            /*                                                      17
            /*
            /***************************************************************************/

            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);
            int newSizeX = sizeX;
            int newSizeY = sizeY;

            if (degree > 360)
            {
                degree = degree % 360;
            }

            if (degree == 90 || degree == 270)
            {
                newSizeX = sizeY;
                newSizeY = sizeX;
            }

            int[,] destination = new int[newSizeX, newSizeY];

            switch (degree)
            {
                case 0:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[x, y] = source[x, y]; //Just return a copy of the source array
                        }
                    }
                    break;
                case 90:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[newSizeX - y - 1, x] = source[x, y];
                        }
                    }
                    break;

                case 180:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[newSizeX - x - 1, newSizeY - y - 1] = source[x, y];
                        }
                    }
                    break;

                case 270:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[y, newSizeY - x - 1] = source[x, y];
                        }
                    }
                    break;

                default:
                    Console.WriteLine("ERROR in function: Array2D.RotateCW(). Invalid degrees: {0}", degree);
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[x, y] = source[x, y]; //Just return a copy of the source array
                        }
                    }
                    break;
            }




            return destination;
        }


        public static long[,] RotateCW(long[,] source, int degree = 90)
        {
            /***************************** Rotates a matrix ****************************/
            /* Inputs:
            /*          -long[,] source. The array to rotate. 
            /*          -int degrees to rotate (90 or 180 or 270), default: 90
            /*
            /* Outputs:
            /*          -long[,] destination. The rotated array.
            /*
            /*
            /* Example:
            /*          input:          rotate 90 degree        output:
            /*              123456                                  71
            /*              778899             ==>                  72   
            /*                                                      83
            /*                                                      84
            /*                                                      95
            /*                                                      96
            /*
            /*          input:          rotate 180 degree       output:
            /*              123456                                  998877
            /*              778899             ==>                  654321   
            /*
            /*          input:          rotate 270 degree      output:
            /*              123456                                  69
            /*              778899             ==>                  59   
            /*                                                      48
            /*                                                      38
            /*                                                      27
            /*                                                      17
            /*
            /***************************************************************************/

            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);
            int newSizeX = sizeX;
            int newSizeY = sizeY;

            if (degree > 360)
            {
                degree = degree % 360;
            }

            if (degree == 90 || degree == 270)
            {
                newSizeX = sizeY;
                newSizeY = sizeX;
            }

            long[,] destination = new long[newSizeX, newSizeY];

            switch (degree)
            {
                case 0:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[x, y] = source[x, y]; //Just return a copy of the source array
                        }
                    }
                    break;

                case 90:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[newSizeX - y - 1, x] = source[x, y];
                        }
                    }
                    break;

                case 180:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[newSizeX - x - 1, newSizeY - y - 1] = source[x, y];
                        }
                    }
                    break;

                case 270:
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[y, newSizeY - x - 1] = source[x, y];
                        }
                    }
                    break;

                default:
                    Console.WriteLine("ERROR in function: Array2D.RotateCW(). Invalid degrees.");
                    for (int x = 0; x < sizeX; x++)
                    {
                        for (int y = 0; y < sizeY; y++)
                        {
                            destination[x, y] = source[x, y]; //Just return a copy of the source array
                        }
                    }
                    break;
            }

            return destination;
        }

        /******************************************************************************************************************/
        /******************************************** FLIP ARRAY **********************************************************/
        /******************************************************************************************************************/


        public static bool[,] FlipHorizontal(bool[,] source)
        {
            /***************************** Flips a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to flip. 
            /*
            /* Outputs:
            /*          -int[,] destination. The flipped array.
            /*
            /*
            /* Example:
            /*
            /*          input:               output:
            /*              123456                  778899
            /*              778899                  123456   
            /*
            /*************************************************************************/
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);

            bool[,] destination = new bool[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    destination[x, sizeY - y - 1] = source[x, y];
                }
            }

            return destination;
        }



        public static int[,] FlipHorizontal(int[,] source)
        {
            /***************************** Flips a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to flip. 
            /*
            /* Outputs:
            /*          -int[,] destination. The flipped array.
            /*
            /*
            /* Example:
            /*
            /*          input:               output:
            /*              123456                  778899
            /*              778899                  123456   
            /*
            /*************************************************************************/
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);

            int[,] destination = new int[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    destination[x, sizeY - y - 1] = source[x, y];
                }
            }

            return destination;
        }


        public static long[,] FlipHorizontal(long[,] source)
        {
            /***************************** Flips a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to flip. 
            /*
            /* Outputs:
            /*          -int[,] destination. The flipped array.
            /*
            /*
            /* Example:
            /*
            /*          input:               output:
            /*              123456                  778899
            /*              778899                  123456   
            /*
            /*************************************************************************/
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);

            long[,] destination = new long[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    destination[x, sizeY - y - 1] = source[x, y];
                }
            }

            return destination;
        }



        public static bool[,] FlipVertical(bool[,] source)
        {
            /***************************** Flips a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to flip. 
            /*
            /* Outputs:
            /*          -int[,] destination. The flipped array.
            /*
            /*
            /* Example:
            /*
            /*          input:               output:
            /*              123456                  654321
            /*              778899                  998877   
            /*
            /*************************************************************************/
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);

            bool[,] destination = new bool[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    destination[sizeX - x - 1, y] = source[x, y];
                }
            }

            return destination;
        }


        public static int[,] FlipVertical(int[,] source)
        {
            /***************************** Flips a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to flip. 
            /*
            /* Outputs:
            /*          -int[,] destination. The flipped array.
            /*
            /*
            /* Example:
            /*
            /*          input:               output:
            /*              123456                  654321
            /*              778899                  998877   
            /*
            /*************************************************************************/
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);

            int[,] destination = new int[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    destination[sizeX - x - 1, y] = source[x, y];
                }
            }

            return destination;
        }

        public static long[,] FlipVertical(long[,] source)
        {
            /***************************** Flips a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to flip. 
            /*
            /* Outputs:
            /*          -int[,] destination. The flipped array.
            /*
            /*
            /* Example:
            /*
            /*          input:               output:
            /*              123456                  654321
            /*              778899                  998877   
            /*
            /*************************************************************************/
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);

            long[,] destination = new long[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    destination[sizeX - x - 1, y] = source[x, y];
                }
            }

            return destination;
        }


        /******************************************************************************************************************/
        /******************************************* COUNT ARRAY **********************************************************/
        /******************************************************************************************************************/

        public static long CountArray(bool[,] source, bool valueToCount)
        {
            long count = 0;
            for (int x = 0; x < source.GetLength(0); x++)
            {
                for (int y = 0; y < source.GetLength(1); y++)
                {
                    if (source[x, y] == valueToCount)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static long CountArray(int[,] source, int valueToCount)
        {
            long count = 0;
            for (int x = 0; x < source.GetLength(0); x++)
            {
                for (int y = 0; y < source.GetLength(1); y++)
                {
                    if (source[x, y] == valueToCount)
                    {
                        count++;
                    }
                }
            }

            return count;
        }


        public static long CountArray(long[,] source, long valueToCount)
        {
            long count = 0;
            for (int x = 0; x < source.GetLength(0); x++)
            {
                for (int y = 0; y < source.GetLength(1); y++)
                {
                    if (source[x, y] == valueToCount)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /******************************************************************************************************************/
        /******************************************* PRINT ARRAY **********************************************************/
        /******************************************************************************************************************/
        public static void PrintArray(bool[,] source, bool separateBlankLines = false, bool separateSpaces = false)
        {
            /***************************** Prints a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to print to terminal console. 
            /*          -bool separateBlankLines. If true it starts and ends with a empty line
            /*          -bool separateSpaces. If true prints a space between values
            /*
            /* Outputs:
            /*          -none.
            /*
            /***************************************************************************/

            if (separateBlankLines) Console.WriteLine();
            for (int y = 0; y < source.GetLength(1); y++)
            {
                for (int x = 0; x < source.GetLength(0); x++)
                {
                    Console.Write(source[x, y]);
                    if (separateSpaces) Console.Write(" ");
                }
                Console.WriteLine();
            }
            if (separateBlankLines) Console.WriteLine();
        }


        public static void PrintArray(int[,] source, bool separateBlankLines = false, bool separateSpaces = false)
        {
            /***************************** Prints a array ****************************/
            /* Inputs:
            /*          -int[,] source. The array to print to terminal console. 
            /*          -bool separateBlankLines. If true it starts and ends with a empty line
            /*          -bool separateSpaces. If true prints a space between values
            /*
            /* Outputs:
            /*          -none.
            /*
            /***************************************************************************/

            if (separateBlankLines) Console.WriteLine();
            for (int y = 0; y < source.GetLength(1); y++)
            {
                for (int x = 0; x < source.GetLength(0); x++)
                {
                    Console.Write(source[x, y]);
                    if (separateSpaces) Console.Write(" ");
                }
                Console.WriteLine();
            }
            if (separateBlankLines) Console.WriteLine();
        }


        public static void PrintArray(long[,] source, bool separateBlankLines = false, bool separateSpaces = false)
        {
            /***************************** Prints a array ****************************/
            /* Inputs:
            /*          -long[,] source. The array to print to terminal console. 
            /*          -bool separateBlankLines. If true it starts and ends with a empty line
            /*          -bool separateSpaces. If true prints a space between values
            /*
            /* Outputs:
            /*          -none.
            /*
            /***************************************************************************/

            if (separateBlankLines) Console.WriteLine();
            for (int y = 0; y < source.GetLength(1); y++)
            {
                for (int x = 0; x < source.GetLength(0); x++)
                {
                    Console.Write(source[x, y]);
                    if (separateSpaces) Console.Write(" ");
                }
                Console.WriteLine();
            }
            if (separateBlankLines) Console.WriteLine();
        }


    }



}
