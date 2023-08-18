using System;
class Program
{
    public static void Main()
    {
        int inputNum;
        Console.WriteLine("Please enter a 7 digits number:");
        string input = Console.ReadLine();
        bool isLegal = ((input.Length == 7) && (int.TryParse(input, out inputNum)));

        while (!isLegal)
        {
            Console.WriteLine("Illegal input, please try again...");
            input = Console.ReadLine();
            isLegal = ((input.Length == 7) && (int.TryParse(input, out inputNum)));
        }
            Console.WriteLine("The smallest number is " + SmallestDigit(input));
            Console.WriteLine("The average number is " + AverageNum(input));
            Console.WriteLine("The number of evens is " + CountsEven(input));
            Console.WriteLine("The number of digits that are smaller than the units number is " + SmallerThanUnitsDigit(input));
    }

    public static int SmallestDigit(string i_Input)
    {
        int smallestNum = i_Input[0], i = 0;

        while (i < i_Input.Length)
        {
            if (i_Input[i] < smallestNum)
            {
                smallestNum = i_Input[i];
            }
            i++;
        }

        return smallestNum - 48;
    }

    public static double AverageNum(string i_Input)
    {
        double sumOfDigits = 0;

        for (int i = 0; i < i_Input.Length; i++)
        {
            sumOfDigits += i_Input[i] - 48;
        }
        return sumOfDigits / 7;
    }

    public static int CountsEven(string i_Input)
    {
        int countEvens = 0;

        for (int i = 0; i < i_Input.Length; i++)
        {
            if (i_Input[i] % 2 == 0)
            {
                countEvens++;
            }
        }

        return countEvens;
    }

    public static int SmallerThanUnitsDigit(string i_Input)
    {
        int countSmallerThanUnitsDigit = 0;
        int unitsDigit = (i_Input[i_Input.Length -1]) - 48;

        for (int i = 0; i < i_Input.Length - 1; i++)
        {
            if (i_Input[i] - 48 < unitsDigit)
            {
                countSmallerThanUnitsDigit++;
            }
        }

        return countSmallerThanUnitsDigit;
    }
}