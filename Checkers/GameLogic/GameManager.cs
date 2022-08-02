using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class GameManager
    {
        // Build the board when we start a new game
        public static char[,] BuildBoard(int i_SizeOfBoard, Player i_FirstPlayer, Player i_SecondPlayer, bool i_ResetCheckers = false)
        {
            char[,] boardGame = new char[i_SizeOfBoard, i_SizeOfBoard];
            int numOfCheckers = HowManyCheckersInBeginningOfGame(i_SizeOfBoard);

            // If it's not the first game, then we only need to reset the checkers because they already exist.
            if (i_ResetCheckers)
            {
                ResetCheckers(i_FirstPlayer, i_SecondPlayer, numOfCheckers);
            }
            else
            {
                InitializeCheckers(numOfCheckers, i_FirstPlayer, i_SecondPlayer);
            }

            char firstPlayersSymbol = i_FirstPlayer.PlayersCheckerSymbol;
            char secondPlayersSymbol = i_SecondPlayer.PlayersCheckerSymbol;

            //Build the first half of the board.
            for (int i = 0; i < (i_SizeOfBoard / 2) - 1; i++)
            {
                for (int j = Math.Abs((i % 2) - 1); j < i_SizeOfBoard; j += 2)
                {
                    boardGame[i, j] = firstPlayersSymbol;
                    //Updating the first player checker's field
                    i_FirstPlayer.PlayersCheckers[numOfCheckers - 1].Column = j;
                    i_FirstPlayer.PlayersCheckers[numOfCheckers - 1].Row = i;
                    numOfCheckers--;
                }
            }

            numOfCheckers = HowManyCheckersInBeginningOfGame(i_SizeOfBoard);
            //Build the second half of the board.
            for (int i = (i_SizeOfBoard / 2) + 1; i < i_SizeOfBoard; i++)
            {
                for (int j = Math.Abs((i % 2) - 1); j < i_SizeOfBoard; j += 2)
                {
                    boardGame[i, j] = secondPlayersSymbol;
                    //Updating the first player checker's field
                    i_SecondPlayer.PlayersCheckers[numOfCheckers - 1].Column = j;
                    i_SecondPlayer.PlayersCheckers[numOfCheckers - 1].Row = i;
                    numOfCheckers--;
                }
            }

            return boardGame;
        }

        /* Returns the number of checkers for each player */
        internal static int HowManyCheckersInBeginningOfGame(int i_SizeOfBoard)
        {
            int numOfCheckers = 20;

            if (i_SizeOfBoard == 6)
            {
                numOfCheckers = 6;
            }
            else if (i_SizeOfBoard == 8)
            {
                numOfCheckers = 12;
            }

            return numOfCheckers;
        }

        /* Initialize the checker array for each player */
        internal static void InitializeCheckers(int i_NumOfCheckers, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            Checker[] firstCheckersArray = new Checker[i_NumOfCheckers];
            Checker[] secondCheckersArray = new Checker[i_NumOfCheckers];

            for (int i = 0; i < i_NumOfCheckers; i++)
            {
                firstCheckersArray[i] = new Checker();
                secondCheckersArray[i] = new Checker();
            }

            i_FirstPlayer.PlayersCheckers = firstCheckersArray;
            i_FirstPlayer.PlayerLeftCheckers = i_NumOfCheckers;
            i_SecondPlayer.PlayersCheckers = secondCheckersArray;
            i_SecondPlayer.PlayerLeftCheckers = i_NumOfCheckers;
            for (int i = 0; i < i_NumOfCheckers; i++)
            {
                i_FirstPlayer.PlayersCheckers[i].CheckerOwner = i_FirstPlayer;
                i_SecondPlayer.PlayersCheckers[i].CheckerOwner = i_SecondPlayer;
            }
        }

        /* Reset the checkers for another round */
        public static void ResetCheckers(Player i_FirstPlayer, Player i_SecondPlayer, int i_NumOfCheckers)
        {
            i_FirstPlayer.PlayerLeftCheckers = i_NumOfCheckers;
            i_FirstPlayer.PlayerPreviousMove = "";
            i_FirstPlayer.PlayerNextMove = "";
            i_SecondPlayer.PlayerLeftCheckers = i_NumOfCheckers;
            i_SecondPlayer.PlayerPreviousMove = "";
            i_SecondPlayer.PlayerNextMove = "";
            for (int i = 0; i < i_NumOfCheckers; i++)
            {
                i_FirstPlayer.PlayersCheckers[i].IsKing = false;
                i_FirstPlayer.PlayersCheckers[i].IsCaptured = false;
                i_SecondPlayer.PlayersCheckers[i].IsKing = false;
                i_SecondPlayer.PlayersCheckers[i].IsCaptured = false;
            }
        }

        /* Initialize the players params for the game */
        public static void InitializePlayers(Player i_FirstPlayer, Player i_SecondPlayer, int i_BoardSize, string i_FirstPlayerName, string i_SecondPlayerName, bool i_PlayerIsComputer)
        {
            i_FirstPlayer.PlayerName = i_FirstPlayerName;
            i_FirstPlayer.PlayersCheckerSymbol = 'O';
            i_FirstPlayer.PlayerLeftCheckers = i_BoardSize;
            i_FirstPlayer.IsComputer = false;
            i_SecondPlayer.PlayerName = i_SecondPlayerName;
            i_SecondPlayer.PlayersCheckerSymbol = 'X';
            i_SecondPlayer.PlayerLeftCheckers = i_BoardSize;
            i_SecondPlayer.IsComputer = i_PlayerIsComputer;
        }

        /* Manage all validity checks methods */
        public static bool ManageValidityMethods(int i_CurrentColumn, int i_CurrentRow, int i_NextColumn, int i_NextRow, Player i_CurrentPlayer, Player i_OpponentPlayer, char[,] i_CurrentBoard)
        {
            List<Checker> checkersThatCanEat = UpdateTheCheckersValidMoves(i_CurrentBoard, i_CurrentPlayer);
            checkersThatCanEat = removeCapturedCheckers(checkersThatCanEat);
            bool isValidMove = UpdateCheckerIfMoveIsValid(i_CurrentColumn, i_CurrentRow, i_NextColumn, i_NextRow, i_CurrentPlayer, checkersThatCanEat);

            return isValidMove;
        }

        private static List<Checker> removeCapturedCheckers(List<Checker> i_ListOfCheckers)
        {
            if(i_ListOfCheckers.Count != 0)
            {
                foreach (Checker checker in i_ListOfCheckers.ToList())
                {
                    if(checker.IsCaptured)
                    {
                        i_ListOfCheckers.Remove(checker);
                        if(i_ListOfCheckers.Count == 0)
                        {
                            break;
                        }
                    }
                }
            }

            return i_ListOfCheckers;
        }

        /* Check if the player move is valid */
        internal static bool UpdateCheckerIfMoveIsValid(int i_CurrentColumn, int i_CurrentRow, int i_NextColumn, int i_NextRow, Player i_CurrentPlayer, List<Checker> i_CheckersThatCanEat)
        {
            bool isMoveValid = false;
            List<(int, int)> nextMove = new List<(int, int)>();
            nextMove.Add((i_NextColumn, i_NextRow));

            //Searching for the checker that the player wants to move and if he can make the next move, update the new checker's square
            for (int i = 0; i < i_CurrentPlayer.PlayersCheckers.Length; i++)
            {
                Checker currentChecker = i_CurrentPlayer.PlayersCheckers[i];

                // If we found the wanted checker - then we check if the move is valid
                if (currentChecker.Column == i_CurrentColumn && currentChecker.Row == i_CurrentRow)
                {
                    // If there are checkers that can eat then the checker should be able to eat - unless it's not a valid move
                    if (i_CheckersThatCanEat.Count != 0 && i_CheckersThatCanEat.Contains(currentChecker))
                    {
                        if (currentChecker.PossibleValidMoves.Contains((i_NextColumn, i_NextRow)))
                        {
                            isMoveValid = true;
                            UpdateChecker(currentChecker, i_NextColumn, i_NextRow);
                        }
                    }
                    // If there are no checkers that can eat, then check if the wanted move is valid
                    else if (i_CheckersThatCanEat.Count == 0)
                    {
                        if (currentChecker.PossibleValidMoves.Contains((i_NextColumn, i_NextRow)))
                        {
                            isMoveValid = true;
                            UpdateChecker(currentChecker, i_NextColumn, i_NextRow);
                        }
                    }

                    break;
                }
            }

            return isMoveValid;
        }

        /* Update the new move that the player execute on board */
        public static char[,] UpdateBoard(char[,] i_CurrentBoard, int i_CurrentColumn, int i_CurrentRow, int i_NextColumn, int i_NextRow, Player i_CurrentPlayer, Player i_OpponentPlayer)
        {
            Checker currentMovedChecker = FindCheckerInPlayerCheckerArray(i_CurrentPlayer.PlayersCheckers, i_NextColumn, i_NextRow);

            if (currentMovedChecker != null)
            {
                // If we captured in the current move, we should update the checker that was captured.
                UpdateIfCaptureInMove(i_CurrentBoard, i_CurrentColumn, i_CurrentRow, i_NextColumn, i_NextRow, i_CurrentPlayer, i_OpponentPlayer);
                // Update the squares of the board after the move
                char symbolInSquare = i_CurrentBoard[i_CurrentRow, i_CurrentColumn];

                i_CurrentBoard[i_NextRow, i_NextColumn] = symbolInSquare;
                i_CurrentBoard[i_CurrentRow, i_CurrentColumn] = '\0';
                // After updating board, check if the current checker is king and update its property
                UpdateIfCheckerIsKing(currentMovedChecker, i_CurrentBoard.GetLength(0), i_CurrentBoard);
            }   

            return i_CurrentBoard;
        }

        /* Update the position properties of the checker */
        internal static void UpdateChecker(Checker i_CurrentChecker, int i_NextColumn, int i_NextRow)
        {
            i_CurrentChecker.Column = i_NextColumn;
            i_CurrentChecker.Row = i_NextRow;
        }

        // Updates the valid moves for each checker in board and returns a list of the checkers that can eat
        internal static List<Checker> UpdateTheCheckersValidMoves(char[,] i_BoardGame, Player i_CurrentPlayer)
        {
            int sizeOfBoard = i_BoardGame.GetLength(0);
            List<Checker> checkersThatCanEat = new List<Checker>();

            //Iterating through the board and update possible moves for each checker
            for (int i = 0; i < sizeOfBoard; i++)
            {
                for (int j = 0; j < sizeOfBoard; j++)
                {
                    char squareSymbol = i_BoardGame[i, j];
                    Checker currentChecker = FindCheckerInPlayerCheckerArray(i_CurrentPlayer.PlayersCheckers, j, i);

                    if (currentChecker != null && currentChecker.IsCaptured == false)
                    {
                        currentChecker.PossibleValidMoves = null;
                        // For each checker, check for capturing steps first. If there are no capturing moves, try to find empty square to move
                        if (squareSymbol == 'X')
                        {
                            // Call the method that checks the valid steps for going backwards
                            FindValidBackwardCapturing(i_BoardGame, currentChecker);
                            if (currentChecker.PossibleValidMoves.Count == 0)
                            {
                                FindValidBackwardEmptySpace(i_BoardGame, currentChecker);
                            }
                            else
                            {
                                checkersThatCanEat.Add(currentChecker);
                            }
                        }
                        else if (squareSymbol == 'O')
                        {
                            // Call the method that checks the valid steps for going forward
                            FindValidUpwardCapturing(i_BoardGame, currentChecker);
                            if (currentChecker.PossibleValidMoves.Count == 0)
                            {
                                FindValidForwardEmptySpace(i_BoardGame, currentChecker);
                            }
                            else
                            {
                                checkersThatCanEat.Add(currentChecker);
                            }
                        }
                        // If the checker is a king try all possibles steps
                        else if (currentChecker.IsKing)
                        {
                            FindValidBackwardCapturing(i_BoardGame, currentChecker);
                            FindValidUpwardCapturing(i_BoardGame, currentChecker);
                            if (currentChecker.PossibleValidMoves.Count == 0)
                            {
                                FindValidForwardEmptySpace(i_BoardGame, currentChecker);
                                FindValidBackwardEmptySpace(i_BoardGame, currentChecker);
                            }
                            else
                            {
                                checkersThatCanEat.Add(currentChecker);
                            }
                        }
                    }
                }
            }

            return checkersThatCanEat;
        }

        /* Finding the player's checker by its row and colmun and returns it */
        internal static Checker FindCheckerInPlayerCheckerArray(Checker[] i_CheckerArray, int i_CheckerColumn, int i_CheckerRow)
        {
            Checker foundedChecker = null;
            for (int i = 0; i < i_CheckerArray.Length; i++)
            {
                if (i_CheckerArray[i].Column == i_CheckerColumn && i_CheckerArray[i].Row == i_CheckerRow)
                {
                    foundedChecker = i_CheckerArray[i];
                    break;
                }
            }

            return foundedChecker;
        }

        // Relevant for kings and for 'O' - Find valid steps
        internal static void FindValidUpwardCapturing(char[,] i_BoardGame, Checker i_CurrentChecker)
        {
            int sizeOfBoard = i_BoardGame.GetLength(0);

            // Check if the checker can eat an opponent on its left diagonal square
            if (i_CurrentChecker.Column > 1 && i_CurrentChecker.Row < sizeOfBoard - 2)
            {
                if (i_BoardGame[i_CurrentChecker.Row + 2, i_CurrentChecker.Column - 2].Equals('\0'))
                {
                    char leftDiagonalSquare = i_BoardGame[i_CurrentChecker.Row + 1, i_CurrentChecker.Column - 1];

                    // If you are player with the symbol 'O' - check if you can capture, and add your possible square
                    if (i_CurrentChecker.CheckerOwner.PlayersCheckerSymbol == 'O')
                    {
                        if (leftDiagonalSquare == 'X' || leftDiagonalSquare == 'Z')
                        {
                            i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column - 2, i_CurrentChecker.Row + 2));
                        }
                    }
                    // If you are king of the player with the symbol 'X' (king is 'Z') - check if you can capture.
                    else if (leftDiagonalSquare == 'O' || leftDiagonalSquare == 'Q')
                    {
                        i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column - 2, i_CurrentChecker.Row + 2));
                    }
                }
            }

            // Check if the checker can eat an opponent on its right diagonal square
            if (i_CurrentChecker.Column < sizeOfBoard - 2 && i_CurrentChecker.Row < sizeOfBoard - 2)
            {
                if (i_BoardGame[i_CurrentChecker.Row + 2, i_CurrentChecker.Column + 2].Equals('\0'))
                {
                    char rightDiagonalSquare = i_BoardGame[i_CurrentChecker.Row + 1, i_CurrentChecker.Column + 1];

                    // If you are player with the symbol 'O' - check if you can capture, and add your possible square
                    if (i_CurrentChecker.CheckerOwner.PlayersCheckerSymbol == 'O')
                    {
                        if (rightDiagonalSquare == 'X' || rightDiagonalSquare == 'Z')
                        {
                            i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column + 2, i_CurrentChecker.Row + 2));
                        }
                    }
                    // If you are king of the player with the symbol 'X' (king is 'Z') - check if you can capture.
                    else if (rightDiagonalSquare == 'O' || rightDiagonalSquare == 'Q')
                    {
                        i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column + 2, i_CurrentChecker.Row + 2));
                    }
                }
            }
        }

        // Relevant for kings and for 'X' - Find valid steps
        internal static void FindValidBackwardCapturing(char[,] i_BoardGame, Checker i_CurrentChecker)
        {
            // Check if the checker can eat an opponent on its right diagonal square
            int sizeOfBoard = i_BoardGame.GetLength(0);

            if (i_CurrentChecker.Column < sizeOfBoard - 2 && i_CurrentChecker.Row > 1)
            {
                if (i_BoardGame[i_CurrentChecker.Row - 2, i_CurrentChecker.Column + 2].Equals('\0'))
                {
                    char rightDiagonalSquare = i_BoardGame[i_CurrentChecker.Row - 1, i_CurrentChecker.Column + 1];

                    if (i_CurrentChecker.CheckerOwner.PlayersCheckerSymbol == 'X')
                    {
                        if (rightDiagonalSquare == 'O' || rightDiagonalSquare == 'Q')
                        {
                            i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column + 2, i_CurrentChecker.Row - 2));
                        }
                    }
                    else if (rightDiagonalSquare == 'X' || rightDiagonalSquare == 'Z')
                    {
                        i_CurrentChecker.PossibleValidMoves.Add(((i_CurrentChecker.Column + 2, i_CurrentChecker.Row - 2)));
                    }
                }
            }

            if (i_CurrentChecker.Column > 1 && i_CurrentChecker.Row > 1)
            {
                if (i_BoardGame[i_CurrentChecker.Row - 2, i_CurrentChecker.Column - 2].Equals('\0'))
                {
                    char leftDiagonalSquare = i_BoardGame[i_CurrentChecker.Row - 1, i_CurrentChecker.Column - 1];

                    if (i_CurrentChecker.CheckerOwner.PlayersCheckerSymbol == 'X')
                    {
                        if (leftDiagonalSquare == 'O' || leftDiagonalSquare == 'Q')
                        {
                            i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column - 2, i_CurrentChecker.Row - 2));
                        }
                    }
                    else if (leftDiagonalSquare == 'X' || leftDiagonalSquare == 'Z')
                    {
                        i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column - 2, i_CurrentChecker.Row - 2));
                    }
                }
            }
        }

        /* Check if there is a valid empty square move for checker. For kings and 'O' */
        internal static void FindValidForwardEmptySpace(char[,] i_BoardGame, Checker i_CurrentChecker)
        {
            int sizeOfBoard = i_BoardGame.GetLength(0);

            if (i_CurrentChecker.Column > 0 && i_CurrentChecker.Row < sizeOfBoard - 1)
            {
                char leftDiagonalSquare = i_BoardGame[i_CurrentChecker.Row + 1, i_CurrentChecker.Column - 1];

                if (leftDiagonalSquare == '\0')
                {
                    i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column - 1, i_CurrentChecker.Row + 1));
                }
            }

            if (i_CurrentChecker.Column < sizeOfBoard - 1 && i_CurrentChecker.Row < sizeOfBoard - 1)
            {
                char rightDiagonalSquare = i_BoardGame[i_CurrentChecker.Row + 1, i_CurrentChecker.Column + 1];
                if (rightDiagonalSquare == '\0')
                {
                    (int, int) validMovement = (i_CurrentChecker.Column + 1, i_CurrentChecker.Row + 1);
                    i_CurrentChecker.PossibleValidMoves.Add(validMovement);
                }
            }
        }

        /* Check if there is a valid empty square move for checker - for kings and 'X' */
        internal static void FindValidBackwardEmptySpace(char[,] i_BoardGame, Checker i_CurrentChecker)
        {
            int sizeOfBoard = i_BoardGame.GetLength(0);

            if (i_CurrentChecker.Column > 0 && i_CurrentChecker.Row > 0)
            {
                char leftDiagonalSquare = i_BoardGame[i_CurrentChecker.Row - 1, i_CurrentChecker.Column - 1];

                if (leftDiagonalSquare == '\0')
                {
                    i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column - 1, i_CurrentChecker.Row - 1));
                }
            }

            if (i_CurrentChecker.Column < sizeOfBoard - 1 && i_CurrentChecker.Row > 0)
            {

                char rightDiagonalSquare = i_BoardGame[i_CurrentChecker.Row - 1, i_CurrentChecker.Column + 1];

                if (rightDiagonalSquare == '\0')
                {
                    i_CurrentChecker.PossibleValidMoves.Add((i_CurrentChecker.Column + 1, i_CurrentChecker.Row - 1));
                }
            }
        }

        // If the checker becomes a king - update its status
        internal static void UpdateIfCheckerIsKing(Checker i_CurrentlyMovedChecker, int i_SizeOfBoard, char[,] i_BoardGame)
        {
            char symbolOfChecker = i_CurrentlyMovedChecker.CheckerOwner.PlayersCheckerSymbol;

            //Checks what is your symbol and update that you are a king.
            if ((symbolOfChecker == 'X' && i_CurrentlyMovedChecker.Row == 0) || (symbolOfChecker == 'O' && i_CurrentlyMovedChecker.Row == i_SizeOfBoard - 1))
            {
                i_CurrentlyMovedChecker.IsKing = true;
                char checkersPlayerSymbol = i_CurrentlyMovedChecker.CheckerOwner.PlayersCheckerSymbol;
                char playerSymbol = 'X';
                int checkersColumn = i_CurrentlyMovedChecker.Column;
                int checkersRow = i_CurrentlyMovedChecker.Row;

                if (!checkersPlayerSymbol.Equals(playerSymbol))
                {
                    playerSymbol = 'Q';
                }
                else
                {
                    playerSymbol = 'Z';
                }

                i_BoardGame[i_CurrentlyMovedChecker.Row, i_CurrentlyMovedChecker.Column] = playerSymbol;
            }
        }

        /* If the player captured, then we should find the checker and update its property and remove it from board. */
        internal static void UpdateIfCaptureInMove(Char[,] i_BoardGame, int i_CurrentColumn, int i_CurrentRow, int i_NextColumn, int i_NextRow, Player i_CurrentPlayer, Player i_OpponentPlayer)
        {
            bool captureChecker = Math.Abs(i_CurrentRow - i_NextRow) == 2;

            if (captureChecker)
            {
                int capturedCheckerRow;
                int capturedCheckerColumn;

                if (i_CurrentRow < i_NextRow)
                {
                    capturedCheckerRow = i_NextRow - 1;
                }
                else
                {
                    capturedCheckerRow = i_NextRow + 1;
                }

                if (i_CurrentColumn < i_NextColumn)
                {
                    capturedCheckerColumn = i_NextColumn - 1;
                }
                else
                {
                    capturedCheckerColumn = i_NextColumn + 1;
                }

                Checker capturedChecker = FindCheckerInPlayerCheckerArray(i_OpponentPlayer.PlayersCheckers, capturedCheckerColumn, capturedCheckerRow);
                
                i_BoardGame[capturedCheckerRow, capturedCheckerColumn] = '\0';
                capturedChecker.IsCaptured = true;
                capturedChecker.Row = 0;
                capturedChecker.Column = 0;
                capturedChecker.CheckerOwner.PlayerLeftCheckers--;
            }
        }

        // Checks if the following checker can capture again (double capturing)
        public static bool CanCheckerCaptureAgain(char[,] i_BoardGame, Player i_CurrentPlayer, int i_CurrentColumn, int i_CurrentRow)
        {
            bool captureAgain = false;
            Checker checkerThatCaptured = FindCheckerInPlayerCheckerArray(i_CurrentPlayer.PlayersCheckers, i_CurrentColumn, i_CurrentRow);
            List<Checker> checkersThatCanEat = UpdateTheCheckersValidMoves(i_BoardGame, i_CurrentPlayer);

            // After getting the next checkers that can eat, check if the current checker can eat
            if (checkersThatCanEat.Contains(checkerThatCaptured))
            {
                captureAgain = true;
            }

            return captureAgain;
        }

        // Count left kings in order to calculate the score after the game finishes
        internal static int CountLeftKings(Player i_currentPlayer)
        {
            int numOfKingsAlive = 0;

            foreach (Checker checker in i_currentPlayer.PlayersCheckers)
            {
                if (!checker.IsCaptured && checker.IsKing)
                {
                    numOfKingsAlive++;
                }
            }

            return numOfKingsAlive;
        }

        /* Returns a message with the winning status */
        public static string IsGameFinished(Player i_FirstPlayer, Player i_SecondPlayer, char[,] i_BoardGame)
        {
            string playerWinStatusOutput = "no";
            int firstPlayerLeftKings = CountLeftKings(i_FirstPlayer);
            int secondPlayerLeftKings = CountLeftKings(i_SecondPlayer);
            int totalFirstPlayerPoints = firstPlayerLeftKings * 4 + (i_FirstPlayer.PlayerLeftCheckers - firstPlayerLeftKings);
            int totalSecondPlayerPoint = secondPlayerLeftKings * 4 + (i_SecondPlayer.PlayerLeftCheckers - secondPlayerLeftKings);
            int totalScore = Math.Abs(totalFirstPlayerPoints - totalSecondPlayerPoint);

            // If the first player won, print a message with its new points
            if (DidPlayerWin(i_FirstPlayer, i_SecondPlayer, i_BoardGame))
            {
                i_FirstPlayer.TotalPlayerScore += totalScore;

                playerWinStatusOutput = "Player 1 won!\nAnother round?";
            }
            // If the second player won, print a message with its new points
            else if (DidPlayerWin(i_SecondPlayer, i_FirstPlayer, i_BoardGame))
            {
                i_FirstPlayer.TotalPlayerScore += totalScore;
                playerWinStatusOutput = "Player 2 won!\nAnother round?";
            }
            // If there is a tie, print a message with its new points
            else if (IsTie(i_FirstPlayer, i_SecondPlayer, i_BoardGame))
            {
                playerWinStatusOutput = "Tie!\nAnother round?";
            }

            return playerWinStatusOutput;
        }

        // Check if the player won
        internal static bool DidPlayerWin(Player i_PossiblePlayerWin, Player i_PossiblePlayerLose, char[,] i_BoardGame)
        {
            int numOfCheckers = i_PossiblePlayerWin.PlayersCheckers.Length;
            bool didPlayerWin = true;

            // If the opponent has 0 left checkers - then the current player won
            if (i_PossiblePlayerLose.PlayerLeftCheckers <= 0)
            {
                didPlayerWin = true;
            }
            // If the opponent has no more moves to make - then he won
            else
            {
                // Check for the opponent valid moves
                List<Checker> validMoves = UpdateTheCheckersValidMoves(i_BoardGame, i_PossiblePlayerLose);

                for (int i = 0; i < numOfCheckers; i++)
                {
                    if (!i_PossiblePlayerLose.PlayersCheckers[i].IsCaptured && i_PossiblePlayerLose.PlayersCheckers[i].PossibleValidMoves.Count != 0)
                    {
                        didPlayerWin = false;
                        break;
                    }
                }
            }

            return didPlayerWin;
        }

        /* Check if there are no valid moves for both players */
        internal static bool IsTie(Player i_FirstPlayer, Player i_SecondPlayer, char[,] i_BoardGame)
        {
            int numOfCheckers = i_FirstPlayer.PlayersCheckers.Length;
            bool isTie = true;
            UpdateTheCheckersValidMoves(i_BoardGame, i_FirstPlayer);
            UpdateTheCheckersValidMoves(i_BoardGame, i_SecondPlayer);

            for (int i = 0; i < numOfCheckers; i++)
            {
                if (i_FirstPlayer.PlayersCheckers[i].PossibleValidMoves.Count != 0 || i_SecondPlayer.PlayersCheckers[i].PossibleValidMoves.Count != 0)
                {
                    isTie = false;
                    break;
                }
            }

            return isTie;
        }

        /* Play computer's next move */
        public static void PlayComputer(char[,] i_BoardGame, Player i_ComputerPlayer, Player i_UserPlayer)
        {
            List<Checker> checkersThatCanEat = UpdateTheCheckersValidMoves(i_BoardGame, i_ComputerPlayer);
            // Find a random checker to move
            int numOfCheckers = i_ComputerPlayer.PlayersCheckers.Length;
            Random randomChecker = new Random();
            int indexChecker = randomChecker.Next(0, numOfCheckers);
            Checker currentChecker = i_ComputerPlayer.PlayersCheckers[indexChecker];
            int numberOfCheckersThatCanEat = checkersThatCanEat.Count;
            int numberOfPossibleMoves = 0;
            int numOfOpponentCheckers = i_UserPlayer.PlayerLeftCheckers;

            // If there are checkers that can eat - and the indexChecker is larger than the list of checkers that can eat, generate a new number  
            if (numberOfCheckersThatCanEat != 0)
            {
                if (numberOfCheckersThatCanEat <= indexChecker)
                {
                    indexChecker = randomChecker.Next(0, numberOfCheckersThatCanEat);
                }

                currentChecker = checkersThatCanEat[indexChecker];
            }

            // If The chosen checker doesn't have valid moves, try to find another checker
            else
            {
                numberOfPossibleMoves = currentChecker.PossibleValidMoves.Count;

                while (numberOfPossibleMoves <= 0)
                {
                    indexChecker = randomChecker.Next(0, numOfCheckers);
                    currentChecker = i_ComputerPlayer.PlayersCheckers[indexChecker];
                    numberOfPossibleMoves = currentChecker.PossibleValidMoves.Count;
                }
            }

            // Randomly choose a possible next move
            Random checkerMove = new Random();
            int randomMove = checkerMove.Next(0, numberOfPossibleMoves);
            (int, int) nextCheckersMove = currentChecker.PossibleValidMoves[randomMove];
            int nextCheckerColumn = nextCheckersMove.Item1;
            int nextCheckerRow = nextCheckersMove.Item2;
            int currentCheckerColumn = currentChecker.Column;
            int currentCheckerRow = currentChecker.Row;

            currentChecker.Column = nextCheckerColumn;
            currentChecker.Row = nextCheckerRow;

            // Save the changed moves in the checker object properties
            char currentCharColumn = (char)(currentCheckerColumn + 'A');
            char currentCharRow = (char)(currentCheckerRow + 'a');
            char nextCharColumn = (char)(nextCheckerColumn + 'A');
            char nextCharRow = (char)(nextCheckerRow + 'a');
            char[] currentArraySquare = { currentCharColumn, currentCharRow };
            char[] nextArraySquare = { nextCharColumn, nextCharRow };
            string previousCheckerMove = new string(currentArraySquare);
            string nextCheckerMove = new string(nextArraySquare);

            i_ComputerPlayer.PlayerPreviousMove = previousCheckerMove;
            i_ComputerPlayer.PlayerNextMove = nextCheckerMove;

            // Update the board after computer's move
            bool isValidMove = ManageValidityMethods(currentCheckerColumn, currentCheckerRow, nextCheckerColumn, nextCheckerRow, i_ComputerPlayer, i_UserPlayer, i_BoardGame);
            bool captureOpponent = i_UserPlayer.PlayerLeftCheckers == numOfOpponentCheckers;

            UpdateBoard(i_BoardGame, currentCheckerColumn, currentCheckerRow, nextCheckerColumn, nextCheckerRow, i_ComputerPlayer, i_UserPlayer);
            // Check if checker can make another capturing - if so, don't pass the turn
            if (!captureOpponent)
            {
                bool playerCanCaptureAgain = GameManager.CanCheckerCaptureAgain(i_BoardGame, i_ComputerPlayer, currentCheckerColumn, currentCheckerRow);

                if (playerCanCaptureAgain)
                {
                    PlayComputer(i_BoardGame, i_ComputerPlayer, i_UserPlayer);
                }
            }
        }
    }
}

