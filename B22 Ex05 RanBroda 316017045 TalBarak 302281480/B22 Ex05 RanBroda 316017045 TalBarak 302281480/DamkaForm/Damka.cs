using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ex02;

namespace DamkaForm
{
    public partial class Damka : Form
    {
        private Button[,] m_BoardButton;
        private int m_NumOfStepsInCurrentRound;
        private static Player s_FirstPlayer = new Player();
        private static Player s_SecondPlayer = new Player();
        private Tuple<int, int> m_ButtonLocation;
        private bool m_FirstPlayerTurn = true;
        private char[,] m_BoardGame;
        private int m_NumOfOpponentCheckers;

        public Damka(string i_NameOfPlayerOne, string i_NameOfPlayerTwo, int i_SizeOfBoard, bool i_PlayAgainstPlayer)
        {
            InitializeComponent();       
            GameManager.InitializePlayers(s_FirstPlayer, s_SecondPlayer, i_SizeOfBoard, i_NameOfPlayerOne, i_NameOfPlayerTwo, !i_PlayAgainstPlayer);
            this.nameOfPlayerOne.Text = i_NameOfPlayerOne;
            this.nameOfPlayerTwo.Text = i_NameOfPlayerTwo;
            this.m_BoardGame = GameManager.BuildBoard(i_SizeOfBoard, s_FirstPlayer, s_SecondPlayer);
            this.m_NumOfOpponentCheckers = s_SecondPlayer.PlayerLeftCheckers;
            if (!i_PlayAgainstPlayer)
            {
                s_SecondPlayer.IsComputer = true;
            }

            designBoard(i_SizeOfBoard);
            buildButtonsMatrixBoard();          
        }

        private void buildButtonsMatrixBoard()
        {
            int boardSize = this.m_BoardGame.GetLength(0);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (this.m_BoardGame[i, j].Equals('Z'))
                    {
                        m_BoardButton[i, j].Text = "Z";
                        m_BoardButton[i, j].Font = new Font("French Script MT", 18, FontStyle.Bold);
                    }
                    else if (this.m_BoardGame[i, j].Equals('Q'))
                    {
                        m_BoardButton[i, j].Text = "Q";
                        m_BoardButton[i, j].Font = new Font("French Script MT", 18, FontStyle.Bold);
                    }
                    else if (this.m_BoardGame[i, j].Equals('X'))
                    {
                        m_BoardButton[i, j].Text = "X";
                        m_BoardButton[i, j].Font = new Font("French Script MT", 18, FontStyle.Bold);
                    }
                    else if (this.m_BoardGame[i, j].Equals('O'))
                    {
                        m_BoardButton[i, j].Text = "O";
                        m_BoardButton[i, j].Font = new Font("French Script MT", 18, FontStyle.Bold);
                    }
                    else
                    {
                        m_BoardButton[i, j].Text = " ";
                    }

                }
            }
        }

        private void designBoard(int i_SizeOfBoard)
        {
            int left = 40;
            int top = 45;

            m_BoardButton = new Button[i_SizeOfBoard, i_SizeOfBoard];
            for (int i = 0; i < i_SizeOfBoard; i = i + 2)
            {
                for (int j = 0; j < i_SizeOfBoard; j++)
                {
                    m_BoardButton[i, j] = new Button();
                    m_BoardButton[i, j].Click += new EventHandler(buttons_Click);
                    if (j % 2 == 0)
                    {
                        m_BoardButton[i, j].BackColor = Color.Black;
                        m_BoardButton[i, j].Enabled = false;
                        m_BoardButton[i, j].Location = new Point(left, top);
                        m_BoardButton[i, j].Size = new Size(45, 45);
                        this.Controls.Add(m_BoardButton[i, j]);
                    }
                    else
                    {
                        m_BoardButton[i, j].BackColor = Color.White;                        
                        m_BoardButton[i, j].Location = new Point(left, top);
                        m_BoardButton[i, j].Size = new Size(45, 45);
                        this.Controls.Add(m_BoardButton[i, j]);
                    }

                    left += 45;
                }

                top += 90;
                left = 40;
            }

            left = 40; 
            top = 90;
            for (int i = 1; i < i_SizeOfBoard; i = i + 2)
            {
                for (int j = 0; j < i_SizeOfBoard; j++)
                {
                    m_BoardButton[i, j] = new Button();
                    m_BoardButton[i, j].Click += new EventHandler(buttons_Click);
                    if (j % 2 == 0)
                    {
                        m_BoardButton[i, j].BackColor = Color.White;
                        m_BoardButton[i, j].Location = new Point(left, top);
                        m_BoardButton[i, j].Size = new Size(45, 45);
                        this.Controls.Add(m_BoardButton[i, j]);
                    }
                    else
                    {
                        m_BoardButton[i, j].BackColor = Color.Black;
                        m_BoardButton[i, j].Enabled = false;
                        m_BoardButton[i, j].Location = new Point(left, top);
                        m_BoardButton[i, j].Size = new Size(45, 45);
                        this.Controls.Add(m_BoardButton[i, j]);
                    }

                    left += 45;
                }

                top += 90;
                left = 40;
            }

            switch (i_SizeOfBoard)
            {
                case 6:
                    this.Size = new Size(370, 410);
                    break;
                case 8:
                    this.Size = new Size(460, 470);
                    break;
                case 10:
                    this.Size = new Size(550, 580);
                    break;
            }
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            if (m_NumOfStepsInCurrentRound == 0)
            {
                (sender as Button).BackColor = Color.LightBlue;
                this.m_ButtonLocation = findButtonOnBoard(sender as Button);
                m_NumOfStepsInCurrentRound++;                
            }
            else if (m_NumOfStepsInCurrentRound == 1)
            {
                if ((sender as Button).BackColor == Color.LightBlue)
                {
                    (sender as Button).BackColor = Color.White;
                }
                else
                {
                    (sender as Button).BackColor = Color.LightBlue;
                    this.m_BoardButton[m_ButtonLocation.Item1, m_ButtonLocation.Item2].BackColor = Color.White;
                    Tuple<int, int> buttonNextLocation = findButtonOnBoard(sender as Button);
                    bool isValidMove;

                    if (m_FirstPlayerTurn)
                    {
                        isValidMove = GameManager.ManageValidityMethods(this.m_ButtonLocation.Item2, m_ButtonLocation.Item1, buttonNextLocation.Item2,
                        buttonNextLocation.Item1, s_FirstPlayer, s_SecondPlayer, this.m_BoardGame);
                    }
                    else
                    {
                        isValidMove = GameManager.ManageValidityMethods(this.m_ButtonLocation.Item2, m_ButtonLocation.Item1, buttonNextLocation.Item2,
                        buttonNextLocation.Item1, s_SecondPlayer, s_FirstPlayer, this.m_BoardGame);
                    }

                    if (!isValidMove)
                    {
                        MessageBox.Show("An invalid move. please try again!","Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        (sender as Button).BackColor = Color.White;
                        this.m_BoardButton[m_ButtonLocation.Item1, m_ButtonLocation.Item2].BackColor = Color.White;
                    }
                    else
                    {
                        updateMove(buttonNextLocation, sender as Button);
                    }
                    
                }

                m_NumOfStepsInCurrentRound = 0;
            }          
        }

        private void updateMove(Tuple<int,int> i_ButtonNextLocation, Button i_NextButton)
        {
            bool captureOpponent;
            bool playerCanCaptureAgain;
       
            if (m_FirstPlayerTurn)
            {
                this.m_BoardGame = GameManager.UpdateBoard(m_BoardGame, this.m_ButtonLocation.Item2, m_ButtonLocation.Item1, i_ButtonNextLocation.Item2,
                i_ButtonNextLocation.Item1, s_FirstPlayer, s_SecondPlayer);
                m_FirstPlayerTurn = false;
            }
            else
            {
                this.m_BoardGame = GameManager.UpdateBoard(m_BoardGame, this.m_ButtonLocation.Item2, m_ButtonLocation.Item1, i_ButtonNextLocation.Item2,
                i_ButtonNextLocation.Item1, s_SecondPlayer, s_FirstPlayer);
                m_FirstPlayerTurn = true;
            }

            i_NextButton.BackColor = Color.White;
            this.m_BoardButton[m_ButtonLocation.Item1, m_ButtonLocation.Item2].BackColor = Color.White;
            buildButtonsMatrixBoard();
            if (!m_FirstPlayerTurn)
            {
                captureOpponent = s_SecondPlayer.PlayerLeftCheckers == m_NumOfOpponentCheckers;
                if (!captureOpponent)
                {
                    playerCanCaptureAgain = GameManager.CanCheckerCaptureAgain(m_BoardGame, s_FirstPlayer, i_ButtonNextLocation.Item2, i_ButtonNextLocation.Item1);
                    if (playerCanCaptureAgain)
                    {
                        m_FirstPlayerTurn = true;
                        m_NumOfOpponentCheckers = s_SecondPlayer.PlayerLeftCheckers;
                    }
                    else
                    {
                        m_NumOfOpponentCheckers = s_FirstPlayer.PlayerLeftCheckers;
                    }

                }
            }            
            else
            {
                captureOpponent = s_FirstPlayer.PlayerLeftCheckers == m_NumOfOpponentCheckers;
                if (!captureOpponent)
                {
                    playerCanCaptureAgain = GameManager.CanCheckerCaptureAgain(m_BoardGame, s_SecondPlayer, i_ButtonNextLocation.Item2, i_ButtonNextLocation.Item1);
                    if (playerCanCaptureAgain)
                    {
                        this.m_FirstPlayerTurn = false;
                        m_NumOfOpponentCheckers = s_FirstPlayer.PlayerLeftCheckers;
                    }
                    else
                    {
                        m_NumOfOpponentCheckers = s_SecondPlayer.PlayerLeftCheckers;
                    }

                }               
            }

            checkIfGameFinished();          
        }

        private void checkIfGameFinished() 
        {
            string gameOverMessage = GameManager.IsGameFinished(s_FirstPlayer, s_SecondPlayer, m_BoardGame);

            if (!gameOverMessage.Equals("no"))
            {
                summarizeGame(gameOverMessage);
            }
            else
            {
                if (m_FirstPlayerTurn)
                {
                    m_NumOfOpponentCheckers = s_SecondPlayer.PlayerLeftCheckers;                  
                }
                else
                {
                    m_NumOfOpponentCheckers = s_FirstPlayer.PlayerLeftCheckers;
                    if (s_SecondPlayer.IsComputer)
                    {
                        GameManager.PlayComputer(m_BoardGame, s_SecondPlayer, s_FirstPlayer);
                        buildButtonsMatrixBoard();                      
                        m_FirstPlayerTurn = true;
                        gameOverMessage = GameManager.IsGameFinished(s_FirstPlayer, s_SecondPlayer, m_BoardGame);
                        if(gameOverMessage.Equals("Player 2 won!\nAnother round?"))
                        {
                            summarizeGame(gameOverMessage);
                        }
                    }
                }

            }

        }

        private void summarizeGame(string i_GameOverMessage)
        {
            DialogResult FinishGameMessage = MessageBox.Show(i_GameOverMessage, "Play another round", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            this.scorePlayerOne.Text = s_FirstPlayer.TotalPlayerScore.ToString();
            this.scorePlayerTwo.Text = s_SecondPlayer.TotalPlayerScore.ToString();
            if (FinishGameMessage == DialogResult.Yes)
            {
                this.m_FirstPlayerTurn = true;
                GameManager.ResetCheckers(s_FirstPlayer, s_SecondPlayer, m_BoardGame.GetLength(0));
                this.m_BoardGame = GameManager.BuildBoard(m_BoardGame.GetLength(0), s_FirstPlayer, s_SecondPlayer);
                buildButtonsMatrixBoard();
            }
            else
            {
                this.Close();
            }

        }

        private Tuple<int,int> findButtonOnBoard(Button i_CurrentButton)
        {
            Tuple<int, int> buttonLocation = null;

            for (int i = 0; i < this.m_BoardButton.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_BoardButton.GetLength(0); j++)
                {
                    if(this.m_BoardButton[i,j].BackColor == Color.LightBlue)
                    {
                        buttonLocation = new Tuple<int, int>(i,j);
                        break;
                    }
                }
            }

            return buttonLocation;
        }

    }
}