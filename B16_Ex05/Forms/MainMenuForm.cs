namespace B16_Ex05.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MainMenuForm : Form
    {
        private const string k_WindoeTitle = "Game settings";
        private const int k_SelectorWidth = 50;
        private const int k_BoardSizeSectionTop = 100;
        private const int k_Padding = 20;
        private const int k_InnerExtraPadding = 10;
        private const int k_TextBoxesLeftValue = 100;
        private const int k_StartButtonWidth = 100;

        private readonly int r_minSizeVal;
        private readonly int r_maxSizeVal;
        private TextBox m_Player1NameTextBox;
        private TextBox m_Player2NameTextBox;
        private CheckBox m_IsHumanPlayerCheckBox;
        private NumericUpDown m_RowsSelector;
        private NumericUpDown m_ColumnsSelector;

        public delegate void GameSettingsEventHandler(MainMenuGameSettingsArgs args);

        public event GameSettingsEventHandler OnStartPressed;

        public MainMenuForm(int i_MinSizeValue, int i_MaxSizeValue)
        {
            r_minSizeVal = i_MinSizeValue;
            r_maxSizeVal = i_MaxSizeValue;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            Text = k_WindoeTitle;

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

            playersTitleLabel.Text = GameTexts.k_MainMenuFormPlayersTitleLabel;
            playersTitleLabel.Left = k_Padding;
            playersTitleLabel.Top = k_Padding;

            Player1Label.Text = GameTexts.k_MainMenuFormPlayer1Label;
            Player1Label.Left = k_Padding + k_InnerExtraPadding;
            Player1Label.Top = playersTitleLabel.Top + playersTitleLabel.Height;

            m_Player1NameTextBox.Left = k_TextBoxesLeftValue;
            m_Player1NameTextBox.Top = Player1Label.Top;

            m_IsHumanPlayerCheckBox.Top = Player1Label.Top + Player1Label.Height;
            m_IsHumanPlayerCheckBox.Left = k_Padding + k_InnerExtraPadding;
            m_IsHumanPlayerCheckBox.Text = GameTexts.k_MainMenuFormPlayer2Label;

            m_Player2NameTextBox.Left = k_TextBoxesLeftValue;
            m_Player2NameTextBox.Top = m_IsHumanPlayerCheckBox.Top;
            m_Player2NameTextBox.Enabled = false;
            m_Player2NameTextBox.Text = GameTexts.k_ComputerName;

            Controls.Add(m_Player2NameTextBox);
            Controls.Add(m_IsHumanPlayerCheckBox);
            Controls.Add(m_Player1NameTextBox);
            Controls.Add(playersTitleLabel);
            Controls.Add(Player1Label);

            m_IsHumanPlayerCheckBox.CheckedChanged += IsHumanPlayerCheckBox_CheckedChanged;
        }

        /// <summary>
        /// When checking\unchecking the player checkbox, enable\disable accordingly the text box 
        /// and change to default computer name when nessecary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IsHumanPlayerCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            m_Player2NameTextBox.Enabled = m_IsHumanPlayerCheckBox.Checked;
            if (!m_IsHumanPlayerCheckBox.Checked)
            {
                m_Player2NameTextBox.Text = GameTexts.k_ComputerName;
            }
        }

        private void CreateBoardSizeSection()
        {
            Label boardSizeSectionLabel = new Label();
            m_RowsSelector = CreateSelector(
                GameTexts.k_MainMenuFormRowsLabel,
                k_Padding + k_InnerExtraPadding,
                k_BoardSizeSectionTop + boardSizeSectionLabel.Height);

            m_ColumnsSelector = CreateSelector(
                GameTexts.k_MainMenuFormColumnsLabel,
                k_Padding + k_InnerExtraPadding + 120,
                k_BoardSizeSectionTop + boardSizeSectionLabel.Height);

            boardSizeSectionLabel.Text = GameTexts.k_MainMenuFormBoardSizeTitleLabel;
            boardSizeSectionLabel.Top = k_BoardSizeSectionTop;
            boardSizeSectionLabel.Left = k_Padding;

            Controls.Add(boardSizeSectionLabel);
        }

        /// <summary>
        /// Helper function to create numeric up down component + matching label at a certain position
        /// </summary>
        /// <param name="i_Label"></param>
        /// <param name="i_Left"></param>
        /// <param name="i_Top"></param>
        /// <returns></returns>
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
            startButton.Text = GameTexts.k_MainMenuStartButtonText;
            startButton.Width = k_StartButtonWidth;

            startButton.Left = ClientSize.Width /2 - startButton.Width / 2;
            startButton.Top = ClientSize.Height - startButton.Height - k_Padding;
            startButton.Click += StartButton_Clicked;
            Controls.Add(startButton);
        }

        /// <summary>
        /// When start clicked , gather information from components 
        /// dispatch OnStartPressed event
        /// and close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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