using System;
using System.Text;

namespace Back
{
    public class GameManager
    {

        public static bool PvP(String i_PlayingAgainst)
        {
            return i_PlayingAgainst.Equals("2");
        }

        public static bool ChangeTurn(ref int io_Turn)
        {
            io_Turn++;
            return (io_Turn % 2 == 0);
        }

        public static bool PlayWitnYourChecker(String i_Input, int i_Turn, Board i_Board)
        {
            int[] inputArr = null;

            if (Rules.IsLegalNextStepInputFromUser(i_Input, i_Board))
            {
                inputArr = Rules.SplitInput(i_Input);
            }

            return (i_Turn % 2 == 1 && i_Board.BoardArr[inputArr[0], inputArr[1]].XorO == 'X') ||
                (i_Turn % 2 == 0 && i_Board.BoardArr[inputArr[0], inputArr[1]].XorO == 'O');
        }

        public static bool PlayWitnYourCheckerComp(String i_Input, int i_Turn, Board i_Board)
        {
            int[] inputArr = null;

            if (Rules.IsLegalNextStepInputFromUser(i_Input, i_Board))
            {
                inputArr = Rules.SplitInput(i_Input);
            }

            return (i_Turn % 2 == 0 && i_Board.BoardArr[inputArr[0], inputArr[1]].XorO == 'X') ||
                (i_Turn % 2 == 1 && i_Board.BoardArr[inputArr[0], inputArr[1]].XorO == 'O');
        }

        public static bool IfStuck(Player i_Player, Board i_Board)
        {
            bool isStuck = true;
            Position[] eatPosArr = new Position[4];
            Position[] movePosArr = new Position[4];

            for (int i = 0; i < i_Board.Size; i++)
            {
                for(int j = 0; j < i_Board.Size ; j++)
                {
                    if (i_Player.XOrO == i_Board.BoardArr[i,j].XorO)
                    {
                        eatPosArr = i_Board.GetEatPositions(i_Board.BoardArr[i, j]);
                        movePosArr = i_Board.GetMovePositions(i_Board.BoardArr[i, j]);
                        if (eatPosArr[0] != null || eatPosArr[1] != null || eatPosArr[2] != null || eatPosArr[3] != null
                            || movePosArr[0] != null || movePosArr[1] != null || movePosArr[2] != null || movePosArr[3] != null)
                        {
                            isStuck = false;
                        }
                    }
                }
            }

            return isStuck;
        }

        // gets input of move from player and makes a move.
        // returns true if succeeded eating/moving
        // returns false if didn't
        public static bool MakeStep(String i_PlayerInput, Board i_Board, Player i_Player)
        {
            int[] arrOfNextStep = Rules.SplitInput(i_PlayerInput);
            bool madeMove = false;
            Position sourcePosition = i_Board.BoardArr[arrOfNextStep[0], arrOfNextStep[1]];
            Position destinationPosition = i_Board.BoardArr[arrOfNextStep[2], arrOfNextStep[3]];

            if (Rules.IsValidToEat(sourcePosition, destinationPosition, i_Board, i_Player))
            {
                i_Board.Eat(sourcePosition, destinationPosition, i_Board);
                madeMove = true;
            }
            else if (Rules.IsValidToMove(sourcePosition, destinationPosition, i_Board, i_Player))
            {
                i_Board.Move(sourcePosition, destinationPosition, i_Board);
                madeMove = true;
            }

            return madeMove;
        }

        public static String ComputerMove(Board i_Board)
        {
            String computerInput = null, str = "";
            Position checkerToUseSrc = null, destPos = null;
            int randomCheckerIndexFromEats, randomCheckerIndexFromMoves, k = 0 , randomIdx;
            Position[] optionalCheckersToUse = new Position[i_Board.SizeOfCheckersArray(i_Board.Size)];
            Position[] eatArr = null, moveArr = null, validEats = null, validMoves = null;
            Random random = new Random();

            for (int i = 0 ; i < i_Board.Size ; i++)
            {
                for (int j = 0 ; j < i_Board.Size ; j++)
                {
                    eatArr = i_Board.GetEatPositions(i_Board.BoardArr[i, j]);
                    if (i_Board.BoardArr[i,j].Occupied && i_Board.BoardArr[i, j].XorO == 'O' &&
                        (eatArr[0] != null || eatArr[1] != null || eatArr[2] != null || eatArr[3] != null))
                    {
                        optionalCheckersToUse[k] = i_Board.BoardArr[i,j];
                        str = "eat";
                        k++;
                    }
                }
            }
            // no eating options, checking for moving options
            if (k == 0)
            {
                for (int i = 0 ; i < i_Board.Size ; i++)
                {
                    for (int j = 0 ; j < i_Board.Size ; j++)
                    {
                        moveArr = i_Board.GetMovePositions(i_Board.BoardArr[i, j]);
                        if (i_Board.BoardArr[i, j].Occupied && i_Board.BoardArr[i, j].XorO == 'O' &&
                            (moveArr[0] != null || moveArr[1] != null || moveArr[2] != null || moveArr[3] != null))
                        {
                            optionalCheckersToUse[k] = i_Board.BoardArr[i, j];
                            str = "move";
                            k++;
                        }
                    }
                }
            }
            // no eat no move are possible
            if (k == 0)
            {
                computerInput = null;
            }
            randomIdx = random.Next(0, k);
            checkerToUseSrc = optionalCheckersToUse[randomIdx];
            if (str == "eat")
            {
                validEats = i_Board.GetEatPositions(checkerToUseSrc);
                randomCheckerIndexFromEats = random.Next(0, validEats.Length);
                while (validEats[randomCheckerIndexFromEats] == null)
                {
                    randomCheckerIndexFromEats = random.Next(0, validEats.Length);
                }

                destPos = validEats[randomCheckerIndexFromEats];
                computerInput += (char)(checkerToUseSrc.Col + 65);
                computerInput += (char)(checkerToUseSrc.Row + 97);
                computerInput += ">";
                computerInput += (char)(destPos.Col + 65);
                computerInput += (char)(destPos.Row + 97);
            }
            if (str == "move")
            {
                validMoves = i_Board.GetMovePositions(checkerToUseSrc);
                randomCheckerIndexFromMoves = random.Next(0, validMoves.Length);
                while (validMoves[randomCheckerIndexFromMoves] == null)
                {
                    randomCheckerIndexFromMoves = random.Next(0, validMoves.Length);
                }

                destPos = validMoves[randomCheckerIndexFromMoves];
                computerInput += (char)(checkerToUseSrc.Col + 65);
                computerInput += (char)(checkerToUseSrc.Row + 97);
                computerInput += ">";
                computerInput += (char)(destPos.Col + 65);
                computerInput += (char)(destPos.Row + 97);
            }

            return computerInput;
        }
            
        // not initialized
        public static String DisplayWinnerQuitOrStuck(Player i_Winner, Board i_Board)
        {
            String winner = null;

            if(i_Winner.XOrO == 'X')
            {
                winner = String.Format(@"The winner is {0}!! The final score difference for {0} is {1} ", i_Winner.Name,
                    Math.Abs((4 * i_Board.XKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.OLeft))
                    - 4 * i_Board.OKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.XLeft)));
            }

            if (i_Winner.XOrO == 'O')
            {
                winner = String.Format(@"The winner is {0}!! The final score difference for {0} is {1} ", i_Winner.Name,
                   Math.Abs((4 * i_Board.XKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.OLeft))
                   - 4 * i_Board.OKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.XLeft)));
            }

            return winner;
        }

        public static String DisplayWinner(Player i_Player1, Player i_Player2, String i_Winner, Board i_Board)
        {
            String winner = null;

            if(i_Winner.Equals("X"))
            {
                winner = String.Format(@"The winner is {0}!! The final score difference for {0} is {1} ", i_Player1.Name , 
                    Math.Abs((4 * i_Board.XKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.OLeft))
                    - 4 * i_Board.OKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.XLeft)));
            }
            if (i_Winner.Equals("O"))
            {
                winner = String.Format(@"The winner is {0}!! The final score difference for {0} is {1} ", i_Player2.Name,
                    Math.Abs((4 * i_Board.XKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.OLeft))
                    - 4 * i_Board.OKings + (i_Board.SizeOfCheckersArray(i_Board.Size) - i_Board.XLeft)));
            }

            return winner;
        }
    }
}