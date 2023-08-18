using System;
using Back;
using Ex02.ConsoleUtils;

namespace Front
{
    public class Game
    {
        public static void run()
        {
            Player player1 = null, player2 = null;
            Board board = null;
            String move = null, message = null, winner = null;
            int turnChange = 1, boardsize;
            int[] inputArrAfterSplit = new int[4];
            int[] computerArr = new int[4];

            // first player name
            Console.WriteLine("Enter your name");
            String firstPlayerName = Console.ReadLine();
            while (!Rules.IsLegalInput1(firstPlayerName))
            {
                Console.WriteLine("Your name is not legal.Enter your name");
                firstPlayerName = Console.ReadLine();
            }
            // size of board
            Console.WriteLine("please enter the size of the board (6, 8 or 10)");
            String boardSize = Console.ReadLine();
            while (!Rules.IsLegalInput2(boardSize))
            {
                Console.WriteLine("The board size is not 6,8,10.Enter a size");
                boardSize = Console.ReadLine();
            }
            // PvP or Pvcomputer
            Console.WriteLine("For playing against the computer please press 1,\nFor" +
            " playing against your friend please press 2");
            String playingAgainst = Console.ReadLine();
            while (!Rules.IsLegalInput3(playingAgainst))
            {
                Console.WriteLine("Try again, please press 1 or 2");
                playingAgainst = Console.ReadLine();
            }
            // players initializations
            player1 = new Player(firstPlayerName, 'X');
            if (playingAgainst == "2")
            {
                Console.WriteLine("Enter second player name");
                String secondPlayerName = Console.ReadLine();
                while (!Rules.IsLegalInput1(secondPlayerName))
                {
                    Console.WriteLine("Your name is invalid. Enter second player name again");
                    secondPlayerName = Console.ReadLine();
                }
                player2 = new Player(secondPlayerName, 'O');
            }
            else if (playingAgainst == "1")
            {
                player2 = new Player("Computer", 'O');
            }
            // board initialization
            bool isStringToNumSucceeded = int.TryParse(boardSize, out boardsize);
            board = new Board(boardsize);

            Screen.Clear();
            // showing board
            board.PrintBoard(boardsize);

            // showing turn string
            message = String.Format(@"{0}'s Turn ({1}) :", firstPlayerName, player1.XOrO);
            Console.WriteLine(message);

            move = Console.ReadLine();
            // PLAYER AGAINST PLAYER
            if (GameManager.PvP(playingAgainst)) // PvP
            {
                // as long as the game is still running and no one won / stuck / pressed Q
                while ((board.Winner() == null) && (!GameManager.IfStuck(player1, board) ||
                        !GameManager.IfStuck(player2, board)) && !Rules.PressedQ(move))
                {
                    // checks if input is legal
                    while (!Rules.IsLegalNextStepInputFromUser(move, board) || !GameManager.PlayWitnYourChecker(move, turnChange, board))
                    {
                        Console.WriteLine("Your move is illegal. please try again");
                        move = Console.ReadLine();
                    }
                    // player1's turn
                    if (GameManager.ChangeTurn(ref turnChange))
                    {
                        // if player1 wants to quit display him the score 
                        if (Rules.PressedQ(move))
                        {
                            GameManager.DisplayWinnerQuitOrStuck(player2, board);
                            break;
                        }
                        if (GameManager.MakeStep(move, board, player1))
                        {
                            Screen.Clear();
                            board.PrintBoard(boardsize);
                            message = String.Format(
                                          @"{0}'s move was ({1}) : {2}
{3}'s Turn ({4}) :", player1.Name, player1.XOrO, move, player2.Name, player2.XOrO);
                            Console.WriteLine(message);
                            move = Console.ReadLine();
                        }
                    }
                    // player2's turn
                    if (!GameManager.ChangeTurn(ref turnChange))
                    {
                        // if player2 wants to quit display him the score 
                        if (Rules.PressedQ(move))
                        {
                            GameManager.DisplayWinnerQuitOrStuck(player1, board);
                            break;
                        }
                        // if player2 made move/eat
                        else if (GameManager.MakeStep(move, board, player2))
                        {
                            Screen.Clear();
                            board.PrintBoard(boardsize);
                            message = String.Format(
                                          @"{0}'s move was ({1}) : {2}
{3}'s Turn ({4}) :", player2.Name, player2.XOrO, move, player1.Name, player1.XOrO);
                            Console.WriteLine(message);
                            move = Console.ReadLine();
                        }
                    }
                } 
            }
            // PLAYER AGAINST COMPUTER
            else if (!GameManager.PvP(playingAgainst))
                {
                // as long as the game is still running and no one won / stuck / pressed Q
                while ((board.Winner() == null) && (!GameManager.IfStuck(player1, board) ||
                        !GameManager.IfStuck(player2, board)) && !Rules.PressedQ(move))
                {
                    if (GameManager.ChangeTurn(ref turnChange))
                    {
                        // checks if input is legal
                        while (!Rules.IsLegalNextStepInputFromUser(move, board) || !GameManager.PlayWitnYourCheckerComp(move, turnChange, board))
                        {
                            Console.WriteLine("Your move is illigal. please try again");
                            move = Console.ReadLine();
                        }
                        // if player1 wants to quit display him the score 
                        if (Rules.PressedQ(move))
                        {
                            GameManager.DisplayWinnerQuitOrStuck(player2, board);
                            break;
                        }
                        // if player1 made move/eat
                        else if (GameManager.MakeStep(move, board, player1))
                        {
                            Screen.Clear();
                            board.PrintBoard(boardsize);
                            message = String.Format(
                                          @"{0}'s move was ({1}) : {2}
{3}'s Turn ({4}) :", player1.Name, player1.XOrO, move, player2.Name, player2.XOrO);
                            Console.WriteLine(message);
                            move = GameManager.ComputerMove(board);
                        }
                    }
                    // computer's turn
                    else
                    {
                        if (GameManager.MakeStep(move, board, player2))
                        {
                            Screen.Clear();
                            board.PrintBoard(boardsize);
                            message = String.Format(
                                          @"{0}'s move was ({1}) : {2}
{3}'s Turn ({4}) :", player2.Name, player2.XOrO, move, player1.Name, player1.XOrO);
                            Console.WriteLine(message);
                            move = Console.ReadLine();
                        }
                    }
                }
            }
            // displays when win
            if ((winner = board.Winner()) != null)
            {
                GameManager.DisplayWinner(player1, player2, winner, board);
            }
            // displays when player1 is stuck
            else if (GameManager.IfStuck(player1, board))
            {
                GameManager.DisplayWinnerQuitOrStuck(player2, board);
            }
            // displays if player2/computer is stuck
            else if (GameManager.IfStuck(player2, board))
            {
                GameManager.DisplayWinnerQuitOrStuck(player1, board);
            }
        }
    }
}
