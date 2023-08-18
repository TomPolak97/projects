using System;

namespace GarageLogic
{

    public class ValueOutOfRangeException : Exception
    {
        float m_MaxValue;
        float m_MinValue;

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
            set
            {
                m_MaxValue = value;
            }
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
            set
            {
                m_MinValue = value;
            }
        }

        public ValueOutOfRangeException(float i_AmountToAdd, float i_MinValue, float i_MaxValue)
          : base(String.Format("Invalid value: {0}. The value has to be in range: {1}-{2}",
              i_AmountToAdd, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
          : base(String.Format("An error occured. You must insert a value between {0} and {1}", i_MinValue, i_MaxValue))
     
        { }
    }
}
