using System;
using System.Text;
class Program
{
    public static void Main()
    {
        bool checkThreeInputs;
        string[] arrayOfInputs = new string[3];

        Console.WriteLine("Hi, please enter 3 digits...");
        for (int i = 0; i < arrayOfInputs.Length; i++)
        {
            arrayOfInputs[i] = Console.ReadLine();
        }
        checkThreeInputs = CheckIfBinary(arrayOfInputs[0]) && CheckIfBinary(arrayOfInputs[1]) && CheckIfBinary(arrayOfInputs[2]);
        while (!checkThreeInputs)
        {
            Console.WriteLine("The input you entered is invalid. Please try again.");
            for (int i = 0; i < arrayOfInputs.Length; i++)
            {
                arrayOfInputs[i] = Console.ReadLine();
            }
            checkThreeInputs = CheckIfBinary(arrayOfInputs[0]) && CheckIfBinary(arrayOfInputs[1]) && CheckIfBinary(arrayOfInputs[2]);
        }
        string[] argsToFormat = new string[10];
        argsToFormat[0] = BinaryToDecimal(arrayOfInputs[0]).ToString();
        argsToFormat[1] = BinaryToDecimal(arrayOfInputs[1]).ToString();
        argsToFormat[2] = BinaryToDecimal(arrayOfInputs[2]).ToString();
        argsToFormat[3] = CountingHowManyPowerOfTwo(arrayOfInputs);
        argsToFormat[4] = CountingHowManyIncreasingSequence(arrayOfInputs);
        argsToFormat[5] = AverageAmountOfOnesOrZeros(arrayOfInputs, '1').ToString();
        argsToFormat[6] = AverageAmountOfOnesOrZeros(arrayOfInputs, '0').ToString();
        argsToFormat[7] = MaxOrMinVal(arrayOfInputs, "max").ToString();
        argsToFormat[8] = MaxOrMinVal(arrayOfInputs, "min").ToString();
        argsToFormat[9] = CountingHowManyPalindromes(arrayOfInputs);
        string msg = string.Format(
            @"the numbers are {0}, {1}, {2} - {3} power of 2, {4} of them consists of
            digits which are a ""strictly monotonically increasing sequence"", the average
            of ones is {5}, the average of zeros is {6}, the greatest is {7}, smallest is {8}.
            In addition, {9} palindrome.", argsToFormat);
        Console.WriteLine(msg);
    }
    public static bool CheckIfBinary(string i_BinaryNumTocheck)
    {
        int i = 0;
        bool isBinary;

        isBinary = (i_BinaryNumTocheck.Length == 8);
        while(isBinary && i <= 7)
        {
            isBinary = (i_BinaryNumTocheck[i] == '0' || i_BinaryNumTocheck[i] == '1');
            i++;
        }

        return isBinary;
    }
    public static int BinaryToDecimal(string i_BinaryNumToConvert)
    {
        int decimalNum = 0;

        for(int i = 8; i > 0; i--)
        {
            decimalNum += (i_BinaryNumToConvert[i-1] - 48) * (int)Math.Pow(2, 8 - i);
        }

        return decimalNum;
    }

    public static double AverageAmountOfOnesOrZeros(string[] i_ArrayOfInputs, char i_OneOrZero)
    {
        double sumOfOnesOrZeros = 0;
        //concatenating the three inputs and iterating over them.
        StringBuilder concatThreeInputs = new StringBuilder(i_ArrayOfInputs[0], 24);

        concatThreeInputs.Append(i_ArrayOfInputs[1]);
        concatThreeInputs.Append(i_ArrayOfInputs[2]);
        for(int i = 0; i < concatThreeInputs.Length; i++)
        {
            if(concatThreeInputs[i] == i_OneOrZero)
            {
                sumOfOnesOrZeros++;
            }
        }
        
        return sumOfOnesOrZeros / 3;
    }
    public static int MaxOrMinVal(string[] i_ArrayOfInputs, string i_MaxOrMin)
    {
        int maxOrMinVal = 0, firstNum = 0, secondNum = 0, thirdNum = 0;

        firstNum = BinaryToDecimal(i_ArrayOfInputs[0]);
        secondNum = BinaryToDecimal(i_ArrayOfInputs[1]);
        thirdNum = BinaryToDecimal(i_ArrayOfInputs[2]);
        if (i_MaxOrMin == "max")
        {
            maxOrMinVal = Math.Max(firstNum, Math.Max(secondNum, thirdNum));
        }
        else
        {
            maxOrMinVal = Math.Min(firstNum, Math.Min(secondNum, thirdNum));
        }

        return maxOrMinVal;
    }
    public static string CountingHowManyPowerOfTwo(string[] i_ArrInputs)
    {
        string countingPowerOfTwoStr = "";
        int countingPowerOfTwoNum = 0;
        bool indeedPoweroOfTwo = true;
        for (int i = 0; i < i_ArrInputs.Length; i++)
        {
            bool checkIfPowerOfTwo = ((Math.Log10(BinaryToDecimal(i_ArrInputs[i])) / Math.Log10(2)) % 1 == 0);
            if (indeedPoweroOfTwo == checkIfPowerOfTwo) 
            {
                countingPowerOfTwoNum++;
            }
        }
        if (countingPowerOfTwoNum == 0)
        {
            countingPowerOfTwoStr = "none of them is";
        }
        if (countingPowerOfTwoNum == 1)
        {
            countingPowerOfTwoStr = "one of them is";
        }
        if (countingPowerOfTwoNum == 2)
        {
            countingPowerOfTwoStr = "two of them are";
        }
        if (countingPowerOfTwoNum == 3)
        {
            countingPowerOfTwoStr = "three of them are";
        }

        return countingPowerOfTwoStr;
    }
    public static string CountingHowManyIncreasingSequence(string[] i_ArrInputs)
    {
        string countingIncreasingSequenceStr = "";
        int countingIncreasingSequenceNum = 0;
        bool indeedIncreasingSequence = true;

        for (int i = 0; i < i_ArrInputs.Length; i++)
        {
            bool checkIfIncreasingSequence = true;
            int currentDigit;
            int numTocheck = BinaryToDecimal(i_ArrInputs[i]);
            currentDigit = numTocheck % 10;
            numTocheck = numTocheck / 10;
            while (numTocheck > 0 && checkIfIncreasingSequence)
            {
                checkIfIncreasingSequence = (currentDigit > (numTocheck % 10));
                currentDigit = numTocheck % 10;
                numTocheck = numTocheck / 10;
            }
            if (indeedIncreasingSequence == checkIfIncreasingSequence)
            {
                countingIncreasingSequenceNum++;
            }

        }
        if (countingIncreasingSequenceNum == 0)
        {
            countingIncreasingSequenceStr = "none";
        }
        if (countingIncreasingSequenceNum == 1)
        {
            countingIncreasingSequenceStr = "one";
        }
        if (countingIncreasingSequenceNum == 2)
        {
            countingIncreasingSequenceStr = "two";
        }
        if (countingIncreasingSequenceNum == 3)
        {
            countingIncreasingSequenceStr = "three";
        }

        return countingIncreasingSequenceStr;
    }
    public static string CountingHowManyPalindromes(string[] i_ArrInputs)
    {
        string countingPalindromesStr = "";
        int countingPalindromesNum = 0;
        bool indeedPalidrome = true;

        for (int i = 0; i < i_ArrInputs.Length; i++)
        {
            string inputStr = BinaryToDecimal(i_ArrInputs[i]).ToString();
            bool checkIfPalindrome = true;
            int start = 0;
            int end = inputStr.Length - 1;

            while (((end - start) >= 1) && checkIfPalindrome)
            {
                checkIfPalindrome = (inputStr[start] == inputStr[end]);
                start++;
                end--;
            }

            checkIfPalindrome = (inputStr.Length == 1) || (checkIfPalindrome);
            if (indeedPalidrome == checkIfPalindrome)
            {
                countingPalindromesNum++;
            }
        }
        if (countingPalindromesNum == 0)
        {
            countingPalindromesStr = "none of them is a";
        }
        if (countingPalindromesNum == 1)
        {
            countingPalindromesStr = "one of them is a";
        }
        if (countingPalindromesNum == 2)
        {
            countingPalindromesStr = "two of them are";
        }
        if (countingPalindromesNum == 3)
        {
            countingPalindromesStr = "three of them are";
        }

        return countingPalindromesStr;
    }
}
