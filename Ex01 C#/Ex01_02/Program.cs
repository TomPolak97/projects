using System;

namespace Ex01_02
{
    public class Program
    {
        public static void Main()
        { 
            SandClock(5);
        }
        public static void SandClock(int i_NumOfLinesInt)
        {
            if (i_NumOfLinesInt % 2 == 0)
            {
                i_NumOfLinesInt++;
            }
            PrintSandClock(i_NumOfLinesInt, i_NumOfLinesInt, "down");
        }

        // when printing the stars - if "down" - decreasing the number of them.
        //                             if "up" - increasing the number of them.
        public static void PrintSandClock(int i_CountingStars, int i_NumOfLinesInt, string i_DownOrUp)
        {
            for (int i = 0; i < (i_NumOfLinesInt - i_CountingStars) / 2; i++)
            {
                Console.Write(" ");
            }
            for (int j = 0; j < i_CountingStars; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
            if (i_DownOrUp == "down" && i_CountingStars > 1)
            {
                PrintSandClock(i_CountingStars - 2, i_NumOfLinesInt, "down");
            }
            if ((i_DownOrUp == "down") && (i_CountingStars == 1) || ((i_DownOrUp == "up") && (i_CountingStars < i_NumOfLinesInt)))
            {
                PrintSandClock(i_CountingStars + 2, i_NumOfLinesInt, "up");
            }

            return;
        }
    }
}
