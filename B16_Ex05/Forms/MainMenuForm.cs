namespace B16_Ex05.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MainMenuForm : Form
    {
        private TextBox m_Player1NameTextBox;
        private TextBox m_Player2NameTextBox;
        private CheckBox m_IsHumanPlayerCheckBox;
        private NumericUpDown m_RowsSelector;
        private NumericUpDown m_ColumnsSelector;

        private const int k_SelectorWidth = 50;
        private const int k_BoardSizeSectionTop = 100;
        private const int k_Padding = 20;
        private const int k_InnerExtraPadding = 10;
        private const int k_TextBoxesLeftValue = 100;

        private readonly int r_minSizeVal;
        private readonly int r_maxSizeVal;

        public delegate void GameSettingsEventHandler(MainMenuGameSettingsArgs args);

        public event GameSettingsEventHandler OnStartPressed;

        public MainMenuForm(int i_MinSizeValue, int i_MaxSizeValue)
        {
            r_minSizeVal = i_MinSizeValue;
            r_maxSizeVal = i_MaxSizeValue;

            CreatePlayersSection();
            CreateBoardSizeSection();
            CreateStartSection();
        }
        private void CreatePlayersSection()
        {
            Label playersTitleLabel = new Label();
            Label Player1Label = new Label();

            m_Player1NameTextBox = new TextBox();
            m_Player2NameTextBox = new TextBox();
            m_IsHumanPlayerCheckBox = new CheckBox();

            playersTitleLabel.Text = "Players:";
            playersTitleLabel.Left = k_Padding;
            playersTitleLabel.Top = k_Padding;

            Player1Label.Text = "Player1 :";
            Player1Label.Left = k_Padding + k_InnerExtraPadding;
            Player1Label.Top = playersTitleLabel.Top + playersTitleLabel.Height;

            m_Player1NameTextBox.Left = k_TextBoxesLeftValue;
            m_Player1NameTextBox.Top = Player1Label.Top;

            m_IsHumanPlayerCheckBox.Top = Player1Label.Top + Player1Label.Height;
            m_IsHumanPlayerCheckBox.Left = k_Padding + k_InnerExtraPadding;
            m_IsHumanPlayerCheckBox.Text = "Player2 :";

            m_Player2NameTextBox.Left = k_TextBoxesLeftValue;
            m_Player2NameTextBox.Top = m_IsHumanPlayerCheckBox.Top;
            m_Player2NameTextBox.Enabled = false;
            m_Player2NameTextBox.Text = "Computer";

            Controls.Add(m_Player2NameTextBox);
            Controls.Add(m_IsHumanPlayerCheckBox);
            Controls.Add(m_Player1NameTextBox);
            Controls.Add(playersTitleLabel);
            Controls.Add(Player1Label);

            m_IsHumanPlayerCheckBox.CheckedChanged += IsHumanPlayerCheckBox_CheckedChanged;
        }

        void IsHumanPlayerCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            m_Player2NameTextBox.Enabled = m_IsHumanPlayerCheckBox.Checked;
            if (!m_IsHumanPlayerCheckBox.Checked)
            {
                m_Player2NameTextBox.Text = "Computer";
            }
        }

        private void CreateBoardSizeSection()
        {
            Label boardSizeSectionLabel = new Label();
            m_RowsSelector = CreateSelector("Rows:", k_Padding + k_InnerExtraPadding, k_BoardSizeSectionTop + boardSizeSectionLabel.Height);
            m_ColumnsSelector = CreateSelector("Columns:", k_Padding + k_InnerExtraPadding + 120, k_BoardSizeSectionTop + boardSizeSectionLabel.Height);

            boardSizeSectionLabel.Text = "Board size";
            boardSizeSectionLabel.Top = k_BoardSizeSectionTop;
            boardSizeSectionLabel.Left = k_Padding;

            Controls.Add(boardSizeSectionLabel);
        }

        private NumericUpDown CreateSelector(string i_Label ,int i_Left, int i_Top)
        {
            NumericUpDown selector = new NumericUpDown();
            Label selectorLabel = new Label();

            selectorLabel.Location = new Point(i_Left,i_Top + 3);
            selectorLabel.Text = i_Label;
            selectorLabel.Width = 50;

            selector.Width = k_SelectorWidth;
            selector.Minimum = r_minSizeVal;
            selector.Maximum = r_maxSizeVal;
            selector.Text = i_Label;
            selector.Top = i_Top;
            selector.Left = selectorLabel.Width + i_Left;

            Controls.Add(selector);
            Controls.Add(selectorLabel);

            return selector;
        }

        private void CreateStartSection()
        {
            Button startButton = new Button();
            startButton.Text = "Start!";
            startButton.Width = 100;

            startButton.Left = ClientSize.Width /2 - startButton.Width / 2;
            startButton.Top = ClientSize.Height - startButton.Height - k_Padding;
            startButton.Click += StartButton_Clicked;
            Controls.Add(startButton);
        }


        void StartButton_Clicked(object sender, System.EventArgs e)
        {
            MainMenuGameSettingsArgs settings = new MainMenuGameSettingsArgs();

            settings.Columns = (int)m_ColumnsSelector.Value;
            settings.Rows = (int)m_RowsSelector.Value;
            settings.IsPlayerHuman = m_IsHumanPlayerCheckBox.Checked;
            settings.Player1Name = m_Player1NameTextBox.Text;
            settings.Player2Name = m_Player2NameTextBox.Text;

            if (OnStartPressed != null)
            {
                OnStartPressed(settings);
            }
            Close();
        }
    }
}