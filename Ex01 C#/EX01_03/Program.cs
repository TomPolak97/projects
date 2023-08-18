using System;
class Program
{
    public static void Main()
    {
        int numOfLinesNum;

        Console.WriteLine("Please enter the number of lines for the sand clock:");
        bool isStrToNumSucceeded = int.TryParse(Console.ReadLine(), out numOfLinesNum);
        while (!isStrToNumSucceeded)
        {
            Console.WriteLine("Your Input is illegal, please try again.");
            isStrToNumSucceeded = int.TryParse(Console.ReadLine(), out numOfLinesNum);
        }
        if (isStrToNumSucceeded)
        {
            // printing sand clock with any number the user asked, calling the method from Ex01_02.
            Ex01_02.Program.SandClock(numOfLinesNum);
        }
    }
}