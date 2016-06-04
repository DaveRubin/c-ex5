using System;
using System.Drawing;
using System.Windows.Forms;

namespace B16_Ex05.Forms
{

    public partial class MainMenuForm : Form
    {
        private readonly int r_minSizeVal;
        private readonly int r_maxSizeVal;

        public MainMenuGameSettingsArgs GameSettings
        {
            get
            {
                return new MainMenuGameSettingsArgs
                           {
                               Columns = (int)NumericCols.Value,
                               Rows = (int)NumericRows.Value,
                               IsPlayerHuman = CheckboxPlayer2Human.Checked,
                               Player1Name = TextboxPlayer1Name.Text,
                               Player2Name = TextboxPlayer2Name.Text
                           };
            }
        }

        public MainMenuForm(int i_MinSizeValue, int i_MaxSizeValue)
        {
            r_minSizeVal = i_MinSizeValue;
            r_maxSizeVal = i_MaxSizeValue;
            InitializeComponent();
            SetUpPlayerEvents();
            SetNumericLimits();
            AddAcceptButtonHandler();
        }

        private void AddAcceptButtonHandler()
        {
            ButtonStart.Click += ButtonStart_Click;
        }

        /// <summary>
        /// When ButtonStart is clicked set DialogRresult to OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SetNumericLimits()
        {
            NumericCols.Minimum = r_minSizeVal;
            NumericCols.Maximum = r_maxSizeVal;
            NumericRows.Minimum = r_minSizeVal;
            NumericRows.Maximum = r_maxSizeVal;
        }

        private void SetUpPlayerEvents()
        {
            TextboxPlayer2Name.Enabled = false;
            TextboxPlayer2Name.Text = GameTexts.k_ComputerName;
            CheckboxPlayer2Human.CheckedChanged += CheckboxPlayer2Human_CheckedChanged;
        }

        /// <summary>
        /// When checking\unchecking the player checkbox, enable\disable accordingly the text box 
        /// and change to default computer name when nessecary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckboxPlayer2Human_CheckedChanged(object sender, System.EventArgs e)
        {
            TextboxPlayer2Name.Enabled = CheckboxPlayer2Human.Checked;
            if (!CheckboxPlayer2Human.Checked)
            {
                TextboxPlayer2Name.Text = GameTexts.k_ComputerName;
            }
        }
    }
}