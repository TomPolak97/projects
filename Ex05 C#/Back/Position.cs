using System;

namespace Back
{
    public class Position
    {
        private int m_Col;
        private int m_Row;
        private bool m_IsOccupied;
        private char m_XorO;
        private bool m_IsKing;
    
        public Position(int i_Row, int i_Col)
        {
            m_Col = i_Col;
            m_Row = i_Row;
            m_IsOccupied = false;
            m_IsKing = false;
        }

        public int Col 
        {
            get
            {
                return m_Col;
            }
            set
            {
                m_Col = value;
            }
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public char XorO
        {
            get
            {
                return m_XorO;
            }
            set
            {
                m_XorO = value;
            }
        }

        public bool Occupied
        {
            get
            {
                return m_IsOccupied;
            }
            set
            {
                m_IsOccupied = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }
            set
            {
                m_IsKing = value;
            }
        }
    }
}
