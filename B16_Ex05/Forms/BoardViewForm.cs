using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex05.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class BoardViewForm : Form
    {
        internal Label Player1Label = new Label();
        internal Label Player2Label = new Label();
        internal Label Player1Score = new Label();
        internal Label Player2Score = new Label();
        internal const string k_WindowTitle = "4 IN A ROW!";
        internal const int k_ColumnSelectButtonWidth = 50;
        internal const int k_ColumnSelectButtonHeight = 40;
        internal const int k_GameButtonDimension = 50;
        internal const int k_Padding = 20;
        internal const char k_P1Symbol = 'O';
        internal const char k_P2Symbol = 'X';
        internal const char k_EmptySymbol = ' ';
        internal int m_NumOfColumns;
        internal int m_NumOfRows;
        internal Button[,] m_ButtonMatrix;
        internal Button[] m_ColumnSelectionButtonsArray;
        
        public delegate void ColumnSelectEventHandler (int col);
        public event ColumnSelectEventHandler OnColumnSelectPressed;


        /// <summary>
        /// Board View Form ctor
        /// </summary>
        /// <param name="MainMenuGameSettingsArgs args"></param>
        /// <returns></returns>
        public BoardViewForm(MainMenuGameSettingsArgs args)
        {
            m_NumOfColumns = args.Columns;
            m_NumOfRows = args.Rows;
            m_ButtonMatrix = new Button[m_NumOfColumns, m_NumOfRows];
            m_ColumnSelectionButtonsArray = new Button[m_NumOfColumns];
            Text = k_WindowTitle;
            CreateBoard(args.Player1Name, args.Player2Name);
            AutoSize = true;
        }

        /// <summary>
        /// Method to build form dynamically
        /// </summary>
        /// <param name="i_Player1Name"></param>
        /// <param name="i_Player2Name"></param>
        /// <returns></returns>
        internal void CreateBoard(string i_Player1Name, string i_Player2Name)
        {
            // create array for the column selection buttons
            for (int i = 0; i < m_NumOfColumns; i++)
            {
                m_ColumnSelectionButtonsArray[i] = new Button()
                {
                    Width = k_ColumnSelectButtonWidth,
                    Height = k_ColumnSelectButtonHeight,
                    Location = new Point(
                        i * k_ColumnSelectButtonWidth + (i + 2) * k_Padding,
                        k_Padding),
                    Text = (i + 1).ToString()
                };
                m_ColumnSelectionButtonsArray[i].Click += ColumnSelection_Clicked;
                Controls.Add(m_ColumnSelectionButtonsArray[i]);
            }

            int tagNum = 1;
            // bulid board button array
            for (int row = 0; row < m_NumOfRows; row++)
            {
                for (int col = 0; col < m_NumOfColumns; col++)
                {
                    m_ButtonMatrix[row, col] = new Button()
                    {
                        Width = k_GameButtonDimension,
                        Height = k_GameButtonDimension,
                        Location = new Point(
                            row * k_GameButtonDimension + (row + 2) * k_Padding,
                            m_ColumnSelectionButtonsArray[col].Bottom + col * k_GameButtonDimension + (col + 1) * k_Padding),
                        Text = k_EmptySymbol.ToString()
                    };
                    m_ButtonMatrix[row, col].Tag = tagNum++;
                    Controls.Add(m_ButtonMatrix[row, col]);
                }
            }
            // adding the name labels
            Player1Label.AutoSize = true;
            Player2Label.AutoSize = true;
            Player1Score.AutoSize = true;
            Player2Score.AutoSize = true;
            Player1Label.Text = i_Player1Name + ": ";
            Player2Label.Text = i_Player2Name + ": ";
            Player1Score.Text = " 0";
            Player2Score.Text = " 0";
            //Player1Label.Top = ClientSize.Height - k_Padding - Player1Label.Height;
            Player1Label.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            Player1Label.Left = m_ButtonMatrix[0, 0].Left;
            Player1Score.Left = Player1Label.Right;
            Player1Score.Anchor = (AnchorStyles.Bottom);
            //Player2Label.Top = ClientSize.Height - k_Padding - Player2Label.Height;
            Player2Label.Anchor = (AnchorStyles.Bottom);
            Player2Label.Left = Player1Label.Right + 2 * k_Padding;
            Player2Score.Left = Player2Label.Right;
            Player2Score.Anchor = (AnchorStyles.Bottom);
            Controls.Add(Player1Label);
            Controls.Add(Player2Label);
            Controls.Add(Player1Score);
            Controls.Add(Player2Score);
        }

        internal void EmptyBoardView()
        {
            for (int row = 0; row < m_NumOfRows; row++)
            {
                for (int col = 0; col < m_NumOfColumns; col++)
                {
                    m_ButtonMatrix[row, col].Text = k_EmptySymbol.ToString();
                }
            }
        }

        internal void EnableAllButtons()
        {
            for (int i = 0; i < m_NumOfColumns; i++)
            {
                m_ColumnSelectionButtonsArray[i].Enabled = true;
            }
        }

        internal void UpdatePlayersScore(int i_Player1Score, int i_Player2Score)
        {
            Player1Score.Text = i_Player1Score.ToString();
            Player2Score.Text = i_Player2Score.ToString();
        }
        
        internal void SetToken(int col, int row, Board.eSlotState i_PieceType)
        {
            char symbolToSet = ' ';
            if (i_PieceType == Board.eSlotState.Player1)
            {
                symbolToSet = k_P1Symbol;
            }
            else if (i_PieceType == Board.eSlotState.Player2)
            {
                symbolToSet = k_P2Symbol;
            }
            m_ButtonMatrix[col, row].Text = symbolToSet.ToString();
        }

        /// <summary>
        /// Function to update board state  
        /// </summary>
        /// <param name="i_SlotMatrix"></param>
        internal void UpdateBoard(Board.eSlotState[,] i_SlotMatrix)
        {
            for (int row = 0; row < m_NumOfRows; row++)
            {
                for (int col = 0; col < m_NumOfColumns; col++)
                {
                    char symbolToSet = ' ';
                    if (i_SlotMatrix[col, row] == Board.eSlotState.Player1)
                    {
                        symbolToSet = k_P1Symbol;
                    }
                    else if (i_SlotMatrix[col, row] == Board.eSlotState.Player2)
                    {
                        symbolToSet = k_P2Symbol;
                    }
                    m_ButtonMatrix[col, row].Text = symbolToSet.ToString();
                }
            }
        }

        /// <summary>
        /// When column selection clicked 
        /// dispatch OnColumnSelectionPressed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ColumnSelection_Clicked(object sender, System.EventArgs e)
        {
            int columnSelected = Int16.Parse((sender as Button).Text);
            OnColumnSelectPressed(--columnSelected);
        }
    }
}
