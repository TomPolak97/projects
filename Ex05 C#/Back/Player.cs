using System;

namespace Back
{
    public class Player
    {
        private readonly String r_Name;
        private char m_XOrO;

        public Player(String i_Name, char i_XOrO)
        {
            r_Name = i_Name;
            m_XOrO = i_XOrO;
        }

        public char XOrO
        {
            get
            {
                return m_XOrO;
            }
            set
            {
                m_XOrO = value;
            }
        }

        public String Name
        {
            get
            {
                return r_Name;
            }
        }
    }
}

