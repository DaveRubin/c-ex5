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
        private bool m_isQuitSelected = false;
        private int m_PlayerSelectedColumn = -1;

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
            m_PlayerSelectedColumn = col;
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
                GameView.ShowWonScreen(m_board, m_players[m_currentPlayerIndex].r_name);
                Console.ReadLine();
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
                    PlayerMove(0);
                }
            }
        }

        private void ShowGameTieScreen()
        {
            GameView.ShowTieScreen();
            ShowRestartScreen();
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
            GameView.ShowWinScreen(winner);
            ShowRestartScreen();
        }

        /// <summary>
        /// Show PlayAgain screen and wait for user input
        /// exit game or continue to another accordingly
        /// </summary>
        private void ShowRestartScreen()
        {
            bool restartGameUserSelection = GameView.RestartGameMessage(m_players);
            if (restartGameUserSelection)
            {
                PlayAgain();
            }
            else
            {
                ExitGame();
            }
        }

        /// <summary>
        /// When exiting game show good bye screeen
        /// </summary>
        private void ExitGame()
        {
            GameView.ShowGoodByeScreen();
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
//            int selectedColumn = i_ColumnSelected;
//            /// get input from AI
//            if (!m_players[m_currentPlayerIndex].IsHuman)
//            {
//                /// selectedColumn = m_players[m_currentPlayerIndex].SelectColumn(ref m_board);
//                Board.eSlotState opponentPieceType = Board.eSlotState.Player1;
//                selectedColumn = AI.SelectMove(ref m_board, Board.eSlotState.Player2, opponentPieceType);
//            }
//
//            Board.eSlotState playerPieceType = (m_currentPlayerIndex == 0)
//                                                   ? Board.eSlotState.Player1
//                                                   : Board.eSlotState.Player2;
//            m_board.AddPieceToColumn(i_ColumnSelected, playerPieceType);
//            m_BoardViewForm.SetToken(i_column, targetRow, playerPieceType);
//            while (!m_board.AddPieceToColumn(selectedColumn, playerPieceType))
//            {
//                selectedColumn = m_PlayerSelectedColumn;
//            }
        }

        public enum eGameMode
        {
            TwoPlayers,
            ManVsMachine
        }
    }
}
