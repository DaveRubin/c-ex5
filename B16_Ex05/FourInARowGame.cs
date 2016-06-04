using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex05
{
    using System.Windows.Forms;

    using B16_Ex05.Forms;

    internal class FourInARowGame
    {
        public const int k_MinDimension = 4;
        public const int k_MaxDimension = 8;

        private Board m_board;
        private BoardViewForm m_BoardViewForm;
        private List<Player> m_players;
        private int m_currentPlayerIndex;
        private eGameMode m_gameMode;

        public FourInARowGame()
        {
            MainMenuForm mainMenuForm = new MainMenuForm(k_MinDimension, k_MaxDimension);
            mainMenuForm.ShowDialog();
            if (mainMenuForm.DialogResult == DialogResult.OK)
            {
                OnStart(mainMenuForm.GameSettings);
            }
        }

        void OnStart(MainMenuGameSettingsArgs i_GameSettings)
        {
            // Console.WriteLine(args);
            // from here we should initialize the game
            InitializeBoardForm(i_GameSettings);
            InitializeBoard(i_GameSettings.Columns, i_GameSettings.Rows, m_BoardViewForm);
            InitializeGameMode(i_GameSettings.IsPlayerHuman, i_GameSettings.Player1Name, i_GameSettings.Player2Name);
            
            StartGame();
        }

        /// <summary>
        /// Get Board dimensions from user and initialize it
        /// </summary>
        private void InitializeBoard(int i_Columns, int i_Rows, BoardViewForm i_BoardView) 
        {
            // initialize board
            m_board = new Board(i_Columns, i_Rows, i_BoardView);
        }

        /// <summary>
        /// Create the Board View Form from setting argument 
        /// <param name="MainMenuGameSettingsArgs args"></param>
        /// </summary>
        private void InitializeBoardForm(MainMenuGameSettingsArgs args)
        {
            m_BoardViewForm = new BoardViewForm(args);
            m_BoardViewForm.OnColumnSelectPressed += m_BoardViewForm_OnColumnSelectPressed;
        }

        void m_BoardViewForm_OnColumnSelectPressed(int col)
        {
            TakeTurn(col);
        }

        /// <summary>
        /// Get the game mode from user and initialize players
        /// </summary>
        private void InitializeGameMode(bool i_IsPlayer2Human, string i_Player1Name, string i_Player2Name)
        {
            bool isPlayer1Human = true;
            m_players = new List<Player>();
            m_players.Add(new Player(i_Player1Name, isPlayer1Human, Board.eSlotState.Player1));
            m_players.Add(new Player(i_Player2Name, i_IsPlayer2Human, Board.eSlotState.Player2));
        }

        /// <summary>
        /// Start the game for the first time
        /// </summary>
        private void StartGame()
        {
            //Open board view and start game logic
            m_BoardViewForm.ShowDialog();
        }

        /// <summary>
        /// Game turn sequence
        /// </summary>
        private void TakeTurn(int i_ColSelected)
        {
            //GameView.ShowTurnScreen(m_board, m_players[m_currentPlayerIndex].r_name);
            PlayerMove(i_ColSelected);
            
            if (CheckIfWin())
            {
                ShowGameWinScreen();
            }
            else if (m_board.IsFull)
            {
                ShowGameTieScreen();
            }
            else
            {
                m_currentPlayerIndex = (m_currentPlayerIndex + 1) % 2;
                if (!m_players[m_currentPlayerIndex].IsHuman)
                {
                    int selectedColumn = ComputerColumnSelection();
                    TakeTurn(selectedColumn);
                }
            }
        }

        private void ShowGameTieScreen()
        {
            ShowEndingMessageBox(GameTexts.k_TieMainText, GameTexts.k_TieWindowTitle);
        }

        private void PlayAgain()
        {
            m_board.EmptyBoard();
            m_currentPlayerIndex = 0;
            //TakeTurn();
        }

        /// <summary>
        /// Congradulate winner
        /// </summary>
        private void ShowGameWinScreen()
        {
            Player winner = m_players[m_currentPlayerIndex];
            winner.Score++;
            m_BoardViewForm.UpdatePlayersScore(m_players[0].Score, m_players[1].Score);
            string winText = string.Format(GameTexts.k_WinScreenTemplate, winner.r_name);
            ShowEndingMessageBox(winText, GameTexts.k_WinWindowTitle);
        }

        private void ShowEndingMessageBox(string i_MainBoxText, string i_WindowTitle)
        {
            DialogResult result = MessageBox.Show(
                i_MainBoxText,
                i_WindowTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PlayAgain();
            }
        }

        /// <summary>
        /// Check board for winner
        /// </summary>
        /// <returns></returns>
        private bool CheckIfWin()
        {
            return BoardAnalyzer.IsGameWon(ref m_board);
        }

        /// <summary>
        /// The player should select a column
        /// </summary>
        private void PlayerMove(int i_ColumnSelected)
        {
            //int selectedColumn = i_ColumnSelected;
            //if (!m_players[m_currentPlayerIndex].IsHuman)
            //{
            //    selectedColumn = ComputerColumnSelection();
            //}

            Board.eSlotState playerPieceType = (m_currentPlayerIndex == 0)
                                                   ? Board.eSlotState.Player1
                                                   : Board.eSlotState.Player2;
            m_board.AddPieceToColumn(i_ColumnSelected, playerPieceType);
        }

        private int ComputerColumnSelection()
        {
            Board.eSlotState opponentPieceType = Board.eSlotState.Player1;
            return AI.SelectMove(ref m_board, Board.eSlotState.Player2, opponentPieceType);
        }

        public enum eGameMode
        {
            TwoPlayers,
            ManVsMachine
        }
    }
}
