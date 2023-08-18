using System;
using System.Text;

namespace Back
{
	public class Board
	{
		private Position[,] m_Board;
		private int m_XLeft;
		private int m_OLeft;
		private int m_XKings;
		private int m_OKings;
		private int m_ScoreX;
		private int m_ScoreO;
		private readonly int r_Size;
		
		public Board(int i_Size)
		{
			r_Size = i_Size;
			m_OLeft = SizeOfCheckersArray(i_Size);
			m_XLeft = SizeOfCheckersArray(i_Size);
			m_Board = new Position[i_Size, i_Size];
			this.m_Board = initBoard(i_Size);
			m_OKings = 0;
			m_XKings = 0;
			m_ScoreO = 0;
			m_ScoreX = 0;
		}

		public int XKings
		{
			get
			{
				return m_XKings;
			}
			set
			{
				m_XKings = value;
			}
		}

		public int OKings
		{
			get
			{
				return m_OKings;
			}
			set
			{
				m_OKings = value;
			}
		}

		public int XLeft
		{
			get
			{
				return m_XLeft;
			}
			set
			{
				m_XLeft = value;
			}
		}

		public int OLeft
		{
			get
			{
				return m_OLeft;
			}
			set
			{
				m_OLeft = value;
			}
		}

		public int ScoreX
        {
			get
            {
				return m_ScoreX;
            }
			set
            {
				m_ScoreX = value;
            }
        }
		public int ScoreO
		{
			get
			{
				return m_ScoreO;
			}
			set
			{
				m_ScoreO = value;
			}
		}

		public int Size
		{
			get
			{
				return r_Size;
			}
		}

		public Position[,] BoardArr
		{
			get
			{
				return m_Board;
			}
            set
            {
				m_Board = value;

			}
		}

		public void Move(Position i_Start, Position i_Destination, Board i_Board)
		{
			i_Board.BoardArr[i_Destination.Row, i_Destination.Col].Occupied = true;
			i_Board.BoardArr[i_Destination.Row, i_Destination.Col].XorO = i_Start.XorO;
			i_Board.BoardArr[i_Start.Row, i_Start.Col].Occupied = false;

			if (i_Board.BoardArr[i_Start.Row, i_Start.Col].IsKing == true)
            {
				i_Board.BoardArr[i_Start.Row, i_Start.Col].IsKing = false;
				i_Board.BoardArr[i_Destination.Row, i_Destination.Col].IsKing = true;
			}
			if (i_Destination.Row == 0 && i_Destination.XorO == 'X')
			{
				i_Board.BoardArr[i_Destination.Row, i_Destination.Col].IsKing = true;
				this.XKings++;
			}
			if (i_Destination.Row == r_Size - 1 && i_Destination.XorO == 'O')
			{
				i_Board.BoardArr[i_Destination.Row, i_Destination.Col].IsKing = true;
				this.OKings++;
			}
		}

		public void Eat(Position i_Start, Position i_Destination, Board i_Board)
		{
			Position posEatenChecker = m_Board[(i_Start.Row + i_Destination.Row) / 2,
				     (i_Start.Col + i_Destination.Col) / 2];

			if (i_Board.BoardArr[i_Start.Row, i_Start.Col].IsKing)
            {
				i_Board.BoardArr[i_Start.Row, i_Start.Col].IsKing = false;
				i_Board.BoardArr[i_Destination.Row, i_Destination.Col].IsKing = true;
			}
			i_Board.BoardArr[i_Destination.Row, i_Destination.Col].Occupied = true;
			i_Board.BoardArr[i_Destination.Row, i_Destination.Col].XorO = i_Start.XorO;
			i_Board.BoardArr[i_Start.Row, i_Start.Col].Occupied = false;
			i_Board.BoardArr[posEatenChecker.Row, posEatenChecker.Col].Occupied = false;

			if (i_Board.BoardArr[i_Destination.Row, i_Destination.Col].XorO == 'X')
            {
				if (i_Board.BoardArr[posEatenChecker.Row, posEatenChecker.Col].IsKing)
				{
					ScoreX += 4;
				}
				else
                {
					ScoreX++;
				}

				i_Board.BoardArr[posEatenChecker.Row, posEatenChecker.Col].IsKing = false;
				OLeft--;
            }
			else if (i_Board.BoardArr[i_Destination.Row, i_Destination.Col].XorO == 'O')
			{
				if (BoardArr[posEatenChecker.Row, posEatenChecker.Col].IsKing)
				{
					ScoreO += 4;
				}
				else
				{
					ScoreO++;
				}
				i_Board.BoardArr[posEatenChecker.Row, posEatenChecker.Col].IsKing = false;
				XLeft--;
			}

			if (i_Destination.Row == 0 && i_Destination.XorO == 'X')
			{
				i_Board.BoardArr[i_Destination.Row, i_Destination.Col].IsKing = true;
				m_XKings++;
			}
			if (i_Destination.Row == r_Size - 1 && i_Destination.XorO == 'O')
			{
				i_Board.BoardArr[i_Destination.Row, i_Destination.Col].IsKing = true;
				m_OKings++;
			}	
		}

		public Position CheckerInCurrentPostion(Position i_CurrentPositon)
		{
			return BoardArr[i_CurrentPositon.Row, i_CurrentPositon.Col];
		}

		public bool IsPositionsAreEquals(Position i_Position1, Position i_Positions2)
        {
			return (i_Positions2 != null) && (i_Position1.Row == i_Positions2.Row) && (i_Position1.Col == i_Positions2.Col);
        }

		public String Winner()
		{
			String winnerType = null;

			if (m_XLeft == 0)
			{
				winnerType = "O";
			}
			if (m_OLeft == 0)
			{
				winnerType = "X";
			}

			return winnerType;
		}

		public int SizeOfCheckersArray(int i_Size)
		{
			int sizeArr = 0;

			if (i_Size == 6)
			{
				sizeArr = 6;
			}
			if (i_Size == 8)
			{
				sizeArr = 12;
			}
			if (i_Size == 10)
			{
				sizeArr = 20;
			}

			return sizeArr;
		}

		private Position[,] initBoard(int i_Size)
		{

			Position[,] board = new Position[i_Size, i_Size];
			int i = 0, j = 0;

			for (i = 0 ; i < i_Size ; i++)
			{
				for (j = 0 ; j < i_Size ; j++)
				{
					board[i, j] = new Position(i, j);
					board[i, j].IsKing = false;
				}
			}
			for (i = i_Size - 1 ; i > i_Size / 2 ; i--)
			{
				for (j = 0 ; j < i_Size ; j++)
				{
					if ((j % 2) == ((i + 1) % 2))
					{
						board[i, j].Occupied = true;
						board[i, j].XorO = 'X';
					}
				}
			}
			for (i = 0 ; i < (i_Size / 2) - 1 ; i++)
			{
				for (j = i_Size - 1 ; j >= 0 ; j--)
				{
					if ((j % 2) == ((i + 1) % 2))
					{
						board[i, j].Occupied = true;
						board[i, j].XorO = 'O';
					}
				}
			}

			return board;
		}

		public void PrintBoard(int i_BoardSize)
		{
			StringBuilder board = new StringBuilder();
			string gapsBetweenColLetters = "   ", currentLine = "";

			for (int i = 65 ; i < 65 + i_BoardSize ; i++)
			{
				gapsBetweenColLetters += ((char)i + "   ");
			}
			board.AppendLine(gapsBetweenColLetters);
			board.AppendLine(separatorsPrinter(i_BoardSize));

			for (int i = 97 ; i < i_BoardSize + 97 ; i++)
			{
				currentLine = "";
				currentLine += (char)i + "|";

				for (int j = 0 ; j < i_BoardSize ; j++)
				{
					foreach (Position position in this.BoardArr)
					{
						if (position.Row == (i - 97) && position.Col == j)
						{
							if (!position.Occupied)
							{
								currentLine += "  ";
							}
							else
							{
								if (BoardArr[position.Row, position.Col].IsKing == true && BoardArr[position.Row, position.Col].XorO == 'X')
								{
									currentLine += " " + "Z";

								}
								else if (BoardArr[position.Row, position.Col].IsKing == true && BoardArr[position.Row, position.Col].XorO == 'O')
								{
									currentLine += " " + "Q";

								}
								else
								{
									currentLine += " " + BoardArr[position.Row, position.Col].XorO;
								}
							}
						}
					}
					currentLine += " |";
				}
				board.AppendLine(currentLine);
				board.AppendLine(separatorsPrinter(i_BoardSize));
			}
			Console.WriteLine(board);
		}

		private static String separatorsPrinter(int i_BoardSize)
		{
			String separators = " ==";

			for (int i = 0; i < i_BoardSize - 1; i++)
			{
				separators += "====";
			}

			return separators += "===";
		}

		public bool IsInBoard(Position i_PositionToCheck)
        {
			return (i_PositionToCheck.Row >= 0 && i_PositionToCheck.Row < r_Size && i_PositionToCheck.Col >= 0 && i_PositionToCheck.Col < r_Size);
        }

		public Position[] GetMovePositions(Position i_Start)
		{
			Position[] positionsNotOutOfBound = new Position[4];
			Position upRight = new Position(i_Start.Row - 1, i_Start.Col + 1);
			Position upLeft = new Position(i_Start.Row - 1, i_Start.Col - 1);
			Position downRight = new Position(i_Start.Row + 1, i_Start.Col + 1);
			Position downLeft = new Position(i_Start.Row + 1, i_Start.Col - 1);
			Position[] positionsToCheckValid = {upRight, upLeft, downRight, downLeft};
			int i = 0;

			// king X
			if (i_Start.IsKing && i_Start.XorO == 'X')
			{
				foreach (Position position in positionsToCheckValid)
				{
					if (IsInBoard(position))
                    {
						if (BoardArr[position.Row, position.Col].Occupied == false)
                        {
							positionsNotOutOfBound[i] = position;
							i++;
						}
                    }
				}
			}

			// regular X
			else if (i_Start.XorO == 'X' && i_Start.IsKing == false)
			{
				if (IsInBoard(upRight) && BoardArr[upRight.Row, upRight.Col].Occupied == false)
				{
					positionsNotOutOfBound[0] = upRight;
				}
				if (IsInBoard(upLeft) && BoardArr[upLeft.Row, upLeft.Col].Occupied == false)
				{
					positionsNotOutOfBound[1] = upLeft;
				}
			}

			// king O
			else if (i_Start.XorO == 'O' && i_Start.IsKing)
			{
				foreach (Position position in positionsToCheckValid)
				{
					if (IsInBoard(position))
					{
						if (BoardArr[position.Row, position.Col].Occupied == false)
						{
							positionsNotOutOfBound[i] = position;
							i++;
						}
					}
				}
			}

			// regular O
			else if (i_Start.XorO == 'O' && i_Start.IsKing == false)
			{
				if (IsInBoard(downLeft) && BoardArr[downLeft.Row, downLeft.Col].Occupied == false)
				{
					positionsNotOutOfBound[0] = downLeft;
				}
				if (IsInBoard(downRight) && BoardArr[downRight.Row, downRight.Col].Occupied == false)
				{
					positionsNotOutOfBound[1] = downRight;
				}
			}

			return positionsNotOutOfBound;
		}

		public Position[] GetEatPositions(Position i_Start)
		{
			int i = 0, k = 0, t = 0;
			Position[] EatPositions = new Position[4];
			Position[] positionToCheckEnemyChecker = new Position[4];
			Position[] positionsToCheckEmpty = new Position[4];
			Position upRight = new Position(i_Start.Row - 1, i_Start.Col + 1);
			Position upLeft = new Position(i_Start.Row - 1, i_Start.Col - 1);
			Position downRight = new Position(i_Start.Row + 1, i_Start.Col + 1);
			Position downLeft = new Position(i_Start.Row + 1, i_Start.Col - 1);
			Position upUpRight = new Position(i_Start.Row - 2, i_Start.Col + 2);
			Position upUpLeft = new Position(i_Start.Row - 2, i_Start.Col - 2);
			Position downDownRight = new Position(i_Start.Row + 2, i_Start.Col + 2);
			Position downDownLeft = new Position(i_Start.Row + 2, i_Start.Col - 2);
			Position[] directions2 = { upUpRight, upUpLeft, downDownRight, downDownLeft };
			Position[] directions1 = { upRight, upLeft, downRight, downLeft };

			// adds to the arrays to check only the directions that are in board
			if (BoardArr[i_Start.Row, i_Start.Col].IsKing)
			{
				for (int j = 0 ; j < 4 ; j++)
				{
					if (IsInBoard(directions1[j]) && IsInBoard(directions2[j]))
					{
						positionToCheckEnemyChecker[i] = BoardArr[directions1[j].Row, directions1[j].Col];
						positionsToCheckEmpty[i] = BoardArr[directions2[j].Row, directions2[j].Col];
						i++;
					}
				}
			}
			// king X - need to check 4 directions
			if (BoardArr[i_Start.Row, i_Start.Col].IsKing && BoardArr[i_Start.Row, i_Start.Col].XorO == 'X')
			{
				for (int l = 0 ; l < positionsToCheckEmpty.Length ; l++)
				{
					if (positionToCheckEnemyChecker[l] == null || positionsToCheckEmpty[l] == null)
					{
						l++;
					}
					else if (positionToCheckEnemyChecker[l].Occupied && positionToCheckEnemyChecker[l].XorO == 'O'
						&& positionsToCheckEmpty[l].Occupied == false)
					{
						EatPositions[t] = positionsToCheckEmpty[l];
						t++;
					}
				}
			}

			// king O - needs to check all 4 directions
			else if (BoardArr[i_Start.Row, i_Start.Col].IsKing && BoardArr[i_Start.Row, i_Start.Col].XorO == 'O')
			{

				for (int l = 0 ; l < positionsToCheckEmpty.Length ; l++)
				{
					if (positionToCheckEnemyChecker[l] == null || positionsToCheckEmpty[l] == null)
                    {
						l++;
                    }
					else if (positionToCheckEnemyChecker[l].Occupied && positionToCheckEnemyChecker[l].XorO == 'X'
						&& positionsToCheckEmpty[l].Occupied == false)
					{
						EatPositions[t] = positionsToCheckEmpty[l];
						t++;
					}
				}
			}
			// regular checker X
			else if (BoardArr[i_Start.Row, i_Start.Col].IsKing == false && BoardArr[i_Start.Row, i_Start.Col].XorO == 'X')
			{
				if (IsInBoard(upRight) && IsInBoard(upUpRight))
				{
					if (BoardArr[upRight.Row, upRight.Col].Occupied && BoardArr[upRight.Row, upRight.Col].XorO == 'O'
												&& BoardArr[upUpRight.Row, upUpRight.Col].Occupied == false)
					{
						EatPositions[k] = upUpRight;
						k++;
					}
				}

				if (IsInBoard(upLeft) && IsInBoard(upUpLeft))
				{
					if (BoardArr[upLeft.Row, upLeft.Col].Occupied && BoardArr[upLeft.Row, upLeft.Col].XorO == 'O'
												&& !BoardArr[upUpLeft.Row, upUpLeft.Col].Occupied)
					{
						EatPositions[k] = upUpLeft;
						k++;
					}
				}
			}
			// regular checker O
			else if (BoardArr[i_Start.Row, i_Start.Col].IsKing == false && BoardArr[i_Start.Row, i_Start.Col].XorO == 'O')
			{
				if (IsInBoard(downLeft) && IsInBoard(downDownLeft))
				{
					if (BoardArr[downLeft.Row, downLeft.Col].Occupied && BoardArr[downLeft.Row, downLeft.Col].XorO == 'X'
												&& !BoardArr[downDownLeft.Row, downDownLeft.Col].Occupied)
					{
						EatPositions[k] = downDownLeft;
						k++;
					}
				}
				if (IsInBoard(downRight) && IsInBoard(downDownRight))
				{
					if (BoardArr[downRight.Row, downRight.Col].Occupied && BoardArr[downRight.Row, downRight.Col].XorO == 'X'
												&& !BoardArr[downDownRight.Row, downDownRight.Col].Occupied)
					{
						EatPositions[k] = downDownRight;
						k++;
					}
				}
			}
		
			return EatPositions;
		}
	}
}