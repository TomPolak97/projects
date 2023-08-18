using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Back;

namespace DamkaUI
{
    public partial class DamkaForm : Form
    {
        private Label m_LabelPlayer1 = new Label();
        private Label m_LabelPlayer2 = new Label();
        private Board m_Board;
        private Button[,] m_ButtonsBoard;
        private gameSettingsForm m_GameSettingsForm;
        private Player m_Player1;
        private Player m_Player2;
        private Button m_ChosenButton;
        private bool m_firstClickDamka = false;
        private int m_TurnChange = 1;
        private StringBuilder m_NextMove = new StringBuilder();

        public DamkaForm()
        {
            m_GameSettingsForm = new gameSettingsForm();

            if (this.m_GameSettingsForm.ShowDialog() == DialogResult.OK)
            {
                m_Player1 = new Player(m_GameSettingsForm.Player1Name, 'X');
                m_Board = initBoard();
                m_ButtonsBoard = new Button[m_Board.Size, m_Board.Size];
                m_Player2 = initPlayer2();
                m_LabelPlayer1.Text = m_GameSettingsForm.Player1Name + ": " + m_Board.ScoreX.ToString();
            }

            this.Text = "Damka";
            initializeComponent();
            this.ShowDialog();
        }

        private Board initBoard()
        {
            int boardSize;

            if (m_GameSettingsForm.RadioButton6x6.Checked)
            {
                boardSize = 6;
            }
            else if (m_GameSettingsForm.RadioButton8x8.Checked)
            {
                boardSize = 8;
            }
            else
            {
                boardSize = 10;
            }

            return new Board(boardSize);
        }

        private Player initPlayer2()
        {
            string playerName;

            if (m_GameSettingsForm.CheckBoxPlayer2.Checked)
            {
                playerName = m_GameSettingsForm.Player2Name;
                m_LabelPlayer2.Text = m_GameSettingsForm.Player2Name + ": " + m_Board.ScoreO.ToString();
            }
            else
            {
                playerName = "Computer";
                m_LabelPlayer2.Text = "Computer: " + m_Board.ScoreO.ToString();
            }

            return new Player(playerName, 'O');
        }

        private void initializeComponent()
        {
            int i = 0, j = 0;

            this.ClientSize = new Size(35 * m_Board.Size + 17, 35 * m_Board.Size + 40);

            for (i = 0; i < m_Board.Size; i++)
            {
                for (j = 0; j < m_Board.Size; j++)
                {
                    m_ButtonsBoard[i, j] = new Button();
                    m_ButtonsBoard[i, j].Size = new Size(35, 35);
                    m_ButtonsBoard[i, j].Click += damkaForm_Click;
                    setButtonsLocation(i, j);
                    this.Controls.Add(m_ButtonsBoard[i, j]);

                    if ((j % 2) == (i % 2))
                    {
                        m_ButtonsBoard[i, j].Enabled = false;
                        m_ButtonsBoard[i, j].BackColor = Color.Gray;
                    }
                }
            }

            for (i = m_Board.Size - 1; i > m_Board.Size / 2; i--)
            {
                for (j = 0; j < m_Board.Size; j++)
                {
                    if ((j % 2) == ((i + 1) % 2))
                    {
                        m_ButtonsBoard[i, j].Text = "X";
                    }
                }
            }

            for (i = 0; i < (m_Board.Size / 2) - 1; i++)
            {
                for (j = m_Board.Size - 1; j >= 0; j--)
                {
                    if ((j % 2) == ((i + 1) % 2))
                    {
                        m_ButtonsBoard[i, j].Text = "O";
                    }
                }
            }

            this.m_LabelPlayer1.Location = new Point(ClientSize.Width / 3 - m_LabelPlayer1.Width / 2, 9);
            this.m_LabelPlayer2.Location = new Point((ClientSize.Width - ClientSize.Width / 3) - m_LabelPlayer1.Width / 2, 9);
            this.m_LabelPlayer1.AutoSize = true;
            this.m_LabelPlayer2.AutoSize = true;
            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_LabelPlayer2);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void setButtonsLocation(int i_Row, int i_Col)
        {
            int xValue = ((this.m_ButtonsBoard[i_Row, i_Col].Width) * i_Col + 10);
            int yValue = ((this.m_ButtonsBoard[i_Row, i_Col].Width) * i_Row) + 30;

            this.m_ButtonsBoard[i_Row, i_Col].Location = new Point(xValue, yValue);
        }

        private void damkaForm_Click(object sender, EventArgs e)
        {
            Button theSender = sender as Button;
            int stepStatus;

            if (!m_firstClickDamka)
            {
                theSender.BackColor = Color.LightBlue;
                m_NextMove.Append(findAndConvertToStringPositionOfButton(theSender));
                m_NextMove.Append(">");
                m_firstClickDamka = true;
            }
            else
            {
                m_firstClickDamka = false;
                m_ChosenButton = getPreviousButtonLocation();

                if (theSender == m_ChosenButton)
                {
                    m_NextMove = new StringBuilder();
                    theSender.BackColor = Color.White;
                }
                else
                {
                    m_NextMove.Append(findAndConvertToStringPositionOfButton(theSender));
                    stepStatus = doNextStep();
                }
            }
        }

        private Button getPreviousButtonLocation()
        {
            Button prevButton = new Button();

            foreach (Button button in m_ButtonsBoard)
            {
                if (button.BackColor == Color.LightBlue)
                {
                    prevButton = button;
                    break;
                }
            }

            return prevButton;
        }

        private void displayPlayer2()
        {
            if (m_GameSettingsForm.CheckBoxPlayer2.Checked)
            {
                m_LabelPlayer2.Text = m_GameSettingsForm.Player2Name + ": " + m_Board.ScoreO.ToString();
            }
            else
            {
                m_LabelPlayer2.Text = "Computer: " + m_Board.ScoreO.ToString();
            }
        }

        private string findAndConvertToStringPositionOfButton(Button i_Button)
        {
            StringBuilder currentIdx = new StringBuilder();

            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    if (m_ButtonsBoard[i, j].Equals(i_Button))
                    {
                        currentIdx.Append((char)(j + 65));
                        currentIdx.Append((char)(i + 97));
                    }
                }
            }

            return currentIdx.ToString();
        }

        private void updateLocationOfEatenButton(Button i_CurrentButton, Button i_DestButton)
        {
            int currRow = 0, currCol = 0, destRow = 0, destCol = 0;

            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    if (m_ButtonsBoard[i, j].Equals(i_CurrentButton))
                    {
                        currRow = i;
                        currCol = j;
                    }
                    if (m_ButtonsBoard[i, j].Equals(i_DestButton))
                    {
                        destRow = i;
                        destCol = j;
                    }
                }
            }

            m_ButtonsBoard[(currRow + destRow) / 2, (currCol + destCol) / 2].Text = string.Empty;
        }

        private int doNextStep()
        {
            int nextStepStatus = 0;
            bool validStep = true;

            if (m_GameSettingsForm.CheckBoxPlayer2.Checked)
            {
                if (GameManager.ChangeTurn(ref m_TurnChange))
                {
                    nextStepStatus = GameManager.MakeStep(m_NextMove.ToString(), m_Board, m_Player1);
                }
                else
                {
                    nextStepStatus = GameManager.MakeStep(m_NextMove.ToString(), m_Board, m_Player2);
                }

                GameManager.ChangeTurn(ref m_TurnChange);
                if (nextStepStatus == 0 || !GameManager.PlayWitnYourChecker(m_NextMove.ToString(), m_TurnChange, m_Board))
                {
                    MessageBox.Show("Invalid move. Please try again", "Invalid move",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    validStep = !validStep;
                }

                GameManager.ChangeTurn(ref m_TurnChange);
                if (validStep)
                {
                    updateButtonsBoard(nextStepStatus);
                }

                gameOver();
            }
            else if (!m_GameSettingsForm.CheckBoxPlayer2.Checked)
            {
                if (GameManager.ChangeTurn(ref m_TurnChange))
                {
                    nextStepStatus = GameManager.MakeStep(m_NextMove.ToString(), m_Board, m_Player1);
                }

                GameManager.ChangeTurn(ref m_TurnChange);
                if (nextStepStatus == 0 || !GameManager.PlayWitnYourChecker(m_NextMove.ToString(), m_TurnChange, m_Board))
                {
                    MessageBox.Show("Invalid move. Please try again", "Invalid move",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    validStep = !validStep;
                }

                if (validStep)
                {
                    updateButtonsBoard(nextStepStatus);
                }
                gameOver();
                string computerMove = GameManager.ComputerMove(m_Board);
                m_NextMove.Append(computerMove);
                nextStepStatus = GameManager.MakeStep(m_NextMove.ToString(), m_Board, m_Player2);
                updateButtonsBoard(nextStepStatus);
                gameOver();
            }

            return nextStepStatus;
        }

        private void updateButtonsBoard(int i_NextStepStatus)
        {
            Button buttonStart = findButtonInBoard(m_NextMove[0], m_NextMove[1]);
            Button buttonDest = findButtonInBoard(m_NextMove[3], m_NextMove[4]);

            if (i_NextStepStatus == 1)
            {
                buttonStart.BackColor = Color.White;
                updateButtonsText(buttonStart, buttonDest);
                buttonDest.BackColor = Color.White;
            }
            else if (i_NextStepStatus == 2)
            {
                buttonStart.BackColor = Color.White;
                updateButtonsText(buttonStart, buttonDest);
                buttonDest.BackColor = Color.White;
                updateLocationOfEatenButton(buttonStart, buttonDest);
                m_LabelPlayer1.Text = m_GameSettingsForm.Player1Name + ": " + m_Board.ScoreX.ToString();
                displayPlayer2();
            }

            m_NextMove = new StringBuilder();
        }

        private Button findButtonInBoard(char i_Col, char i_Row)
        {
            return m_ButtonsBoard[i_Row - 97, i_Col - 65];
        }

        private void gameOver()
        {
            string winner;

            if (GameManager.IfStuck(m_Player1, m_Board) && GameManager.IfStuck(m_Player2, m_Board))
            {
                if (MessageBox.Show(String.Format(@"Tie!
AnotherRound?"), "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    resetGame();
                }
                else
                {
                    this.Close();
                }
            }
            else if ((winner = m_Board.Winner()) != null)
            {
                if (MessageBox.Show(String.Format(@"Player {0} Won!
AnotherRound?", winner), "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    resetGame();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void resetGame()
        {
            m_Board = initBoard();
            resetBoard();
        }

        private void resetBoard()
        {
            int i = 0, j = 0;

            for (i = 0; i < m_Board.Size; i++)
            {
                for (j = 0; j < m_Board.Size; j++)
                {
                    if ((j % 2) == (i % 2))
                    {
                        m_ButtonsBoard[i, j].Enabled = false;
                        m_ButtonsBoard[i, j].BackColor = Color.Gray;
                    }

                    if (m_ButtonsBoard[i, j].Text != string.Empty)
                    {
                        m_ButtonsBoard[i, j].Text = string.Empty;
                    }
                }
            }

            for (i = m_Board.Size - 1; i > m_Board.Size / 2; i--)
            {
                for (j = 0; j < m_Board.Size; j++)
                {
                    if ((j % 2) == ((i + 1) % 2))
                    {
                        m_ButtonsBoard[i, j].Text = "X";
                    }
                    else
                    {
                        m_ButtonsBoard[i, j].Text = string.Empty;
                    }
                }
            }

            for (i = 0; i < (m_Board.Size / 2) - 1; i++)
            {
                for (j = m_Board.Size - 1; j >= 0; j--)
                {
                    if ((j % 2) == ((i + 1) % 2))
                    {
                        m_ButtonsBoard[i, j].Text = "O";
                    }
                    else
                    {
                        m_ButtonsBoard[i, j].Text = string.Empty;
                    }
                }
            }
        }

        private void DamkaForm_Load(object sender, EventArgs e)
        {

        }

        private void updateButtonsText(Button i_CurrButton, Button i_DestButton)
        {
            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    if (m_ButtonsBoard[i, j].Equals(i_DestButton))
                    {
                        if (m_Board.BoardArr[i, j].IsKing && m_Board.BoardArr[i, j].XorO == 'X')
                        {
                            i_DestButton.Text = "Z";
                        }
                        else if (m_Board.BoardArr[i, j].IsKing && m_Board.BoardArr[i, j].XorO == 'O')
                        {
                            i_DestButton.Text = "Q";
                        }
                        else
                        {
                            i_DestButton.Text = i_CurrButton.Text;
                        }

                        i_CurrButton.Text = string.Empty;
                    }
                }
            }
        }
    }
}      
    

