using System;
class Program
{
    public static void Main()
    {
        Console.WriteLine("Hi,please enter a word with 8 characters:");
        string userInput = Console.ReadLine();
        bool goodInput = true, checkingInput = IsLegalInput(userInput);

        while (goodInput != checkingInput)
        {
            Console.WriteLine("Your input is illegal, please try again:");
            userInput = Console.ReadLine();
            checkingInput = IsLegalInput(userInput);
        }
        bool isPalimdrome = IsPalindromeStr(userInput);
        if (isPalimdrome)
        {
            Console.WriteLine("your input is a palindrome");
        } 
        else
        {
            Console.WriteLine("your input is not a palindrome");
        }
        int inputInt;
        bool isNum = int.TryParse(userInput, out inputInt);
        if (isNum)
        {
            if (IsMult3(inputInt))
            {
               Console.WriteLine("your input is a number and a multiplication of 3");
            }
            else
            {
                Console.WriteLine("your input is a number and is not a multiplication of 3");
            }
        }
        else if (IsEngilsOnly(userInput))
        {
            int num = CountingNonCapitalLetters(userInput);
            string msg = string.Format("your input is an english word contains {0} non-capital letters",num);
            Console.WriteLine(msg);
        }
        
    }
    public static bool IsLegalInput(string i_InputStr)
    {
        bool englishOrDigits = true;
        int i = 0;

        while (englishOrDigits && i < i_InputStr.Length)
        {
            englishOrDigits = ((i_InputStr[i] >= 48 && i_InputStr[i] <= 57) ||
                (i_InputStr[i] >= 65 && i_InputStr[i] <= 90) ||
                (i_InputStr[i] >= 97 && i_InputStr[i] <= 122));
            i++;
        }

        return (i_InputStr.Length == 8) && englishOrDigits;
    }
    public static bool IsPalindromeStr(string i_StrToCheck)
    {
        int strToCheckLenMinusTwo = i_StrToCheck.Length -2;

        return (i_StrToCheck.Length < 2 || ((i_StrToCheck[0] == i_StrToCheck[i_StrToCheck.Length - 1]))
                && IsPalindromeStr(i_StrToCheck.Substring(1, strToCheckLenMinusTwo)));
    }
    
    public static bool IsMult3(int i_InputNum)
    {
        bool isNumAndMult3 = i_InputNum % 3 == 0;

        return isNumAndMult3;
    }
    public static bool IsEngilsOnly(string i_UserInput)
    {
        bool isEnglishWord = true;
        int i = 0;

        while (isEnglishWord && i < i_UserInput.Length)
        {
            isEnglishWord = ((i_UserInput[i] >= 65 && i_UserInput[i] <= 90) || (i_UserInput[i] >= 97 && i_UserInput[i] <= 122));
            i++;
        }

        return isEnglishWord;
    }

    public static int CountingNonCapitalLetters(string i_Input)
    {
        bool isLowerCase = true;
        int countLowerCase = 0, i = 0;

        while (i < i_Input.Length)
        {
            isLowerCase = (i_Input[i] >= 97 && i_Input[i] <= 122);
            if (isLowerCase)
            {
                countLowerCase++;
            }
            i++;
        }

        return countLowerCase;
    }
}
