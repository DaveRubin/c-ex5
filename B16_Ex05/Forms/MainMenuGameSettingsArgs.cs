namespace B16_Ex05.Forms
{
    using System;
    public class MainMenuGameSettingsArgs : EventArgs
    {

        private int m_Rows;
        private int m_Columns;
        private bool m_IsPlayerHuman;
        private string m_Player1Name;
        private string m_Player2Name;

        public int Rows
        {
            get
            {
                return m_Rows;
            }
            set
            {
                m_Rows = value;
            }
        }

        public int Columns
        {
            get
            {
                return m_Columns;
            }
            set
            {
                m_Columns = value;
            }
        }
        public bool IsPlayerHuman
        {
            get
            {
                return m_IsPlayerHuman;
            }
            set
            {
                m_IsPlayerHuman = value;
            }
        }
        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }
            set
            {
                m_Player1Name = value;
            }
        }
        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }
            set
            {
                m_Player2Name = value;
            }
        }
    }
}