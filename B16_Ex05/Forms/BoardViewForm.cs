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
        private const string k_WindowTitle = "4 IN A ROW!";
        private const int k_ColumnSelectButtonWidth = 50;
        private const int k_ColumnSelectButtonHeight = 40;
        private const int k_GameButtonDimension = 50;
        private const int k_Padding = 20;
        private const char k_P1Symbol = 'O';
        private const char k_P2Symbol = 'X';
        private const char k_EmptySymbol = ' ';
        private int m_NumOfColumns;
        private int m_NumOfRows;
        private Button[,] m_ButtonMatrix;
        private Button[] m_ColumnSelectionButtonsArray;
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
            Label Player1Label = new Label();
            Label Player2Label = new Label();
            Player1Label.Text = i_Player1Name + ": ";
            Player2Label.Text = i_Player2Name + ": ";
            Player1Label.Top = m_ButtonMatrix[m_NumOfRows - 1, 0].Bottom + k_Padding;
            Player1Label.Left = m_ButtonMatrix[0, 0].Left;
            Player2Label.Top = Player1Label.Top;
            Player2Label.Left = Player1Label.Right + 3 * k_Padding;
            Controls.Add(Player1Label);
            Controls.Add(Player2Label);

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

        /// <summary>
        /// When column selection clicked 
        /// dispatch OnColumnSelectionPressed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ColumnSelection_Clicked(object sender, System.EventArgs e)
        {
            int columnSelected = Int16.Parse((sender as Button).Text);
            OnColumnSelectPressed(columnSelected);
        }
    }
}
