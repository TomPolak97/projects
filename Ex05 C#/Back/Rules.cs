using System;

namespace Back
{
    public class Rules
    {

        public static bool IsLegalInput1(String i_Name)
        {
            bool isName = true;
            int i = 0;

            while (isName && i < i_Name.Length)
            {
                isName = (i_Name[i] >= 65 && i_Name[i] <= 90) ||
                    (i_Name[i] >= 97 && i_Name[i] <= 122);
                i++;
            }

            return(i_Name.Length <= 10) && isName && i_Name != "";
        }

        public static bool IsLegalInput2(String i_SizeOfBoard)
        {
            return i_SizeOfBoard.Equals("6") || i_SizeOfBoard.Equals("8") || i_SizeOfBoard.Equals("10");
        }

        public static bool IsLegalInput3(String i_FriendORCompuetr)
        {
            return i_FriendORCompuetr.Equals("1") || i_FriendORCompuetr.Equals("2");
        }

        public static bool IsLegalNextStepInputFromUser(String i_Input, Board i_Board)
        {
            bool isLegalNextStepInputFromUser = true;
            int count = 0;

            // Uppercase
            if ((i_Input[0] >= 65 && i_Input[0] <= i_Board.Size + 64) &&
                (i_Input[3] >= 65 && i_Input[3] <= i_Board.Size + 64))
            {
                count++;
            }
            //Lowercase
            if ((i_Input[1] >= 97 && i_Input[1] <= i_Board.Size + 96) &&
                (i_Input[4] >= 97 && i_Input[4] <= i_Board.Size + 96))
            {
                count++;
            }
            if (i_Input[2] == '>')
            {
                count++;
            }
            if (count != 3)
            {
                isLegalNextStepInputFromUser = false;
            }

            return isLegalNextStepInputFromUser;
        }

        //returns true if player hit Q 
        public static bool PressedQ(String i_Input)
        {
            return i_Input == "Q";
        }

        public static int[] SplitInput(String i_Input)
        {
            int[] inputArray = new int[4];
       
            inputArray[0] = (int)(i_Input[1] - 97);
            inputArray[1] = (int)(i_Input[0] - 65);
            inputArray[2] = (int)(i_Input[4] - 97);
            inputArray[3] = (int)(i_Input[3] - 65);

            return inputArray;
        }
     
        public static bool IsValidToMove(Position i_Start, Position i_Destination, Board i_Board, Player i_Player)
        {
            Position[] movePositions = new Position[4];
            bool isValidToMove1 = false, isValidToMove2 = false, isValidToMove3 = false;

            isValidToMove1 = (i_Player.XOrO == i_Start.XorO);
            movePositions = i_Board.GetMovePositions(i_Start);
            isValidToMove2 = (movePositions[0] != null || movePositions[1] != null || movePositions[2] != null || movePositions[3] != null);
            foreach (Position position in movePositions)
            {
                if (i_Board.IsPositionsAreEquals(i_Destination, position))
                {
                    isValidToMove3 = true;
                }
            }

            return isValidToMove1 && isValidToMove2 && isValidToMove3;
        }

        public static bool IsValidToEat(Position i_Start, Position i_Destination, Board i_Board, Player i_Player)
        {
            Position[] eatPositions = new Position[4];
            bool isValidToMove1 = false, isValidToMove2 = false, isValidToMove3 = false;

            isValidToMove1 = (i_Player.XOrO == i_Start.XorO);
            eatPositions = i_Board.GetEatPositions(i_Start);
            isValidToMove2  = eatPositions[0] != null || eatPositions[1] != null || eatPositions[2] != null || eatPositions[3] != null;
            foreach (Position position in eatPositions)
            {
                if (i_Board.IsPositionsAreEquals(i_Destination, position))
                {
                    isValidToMove3 = true;
                }
            }

            return isValidToMove1 && isValidToMove2 && isValidToMove3;
        }
    }
}