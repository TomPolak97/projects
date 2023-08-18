using System;
using static GarageLogic.Enums;

namespace GarageLogic
{

    public class Car : Vehicle
    {
        private const float k_CarMaxAirPressure = 29f;
        private const int k_CarAmountOfWheels = 4;
        private const eFuelType k_CarFuelType = eFuelType.Octan95;
        private const float k_CarMaxFuelTank = 38f;
        private const float k_CarMaxBatteryTime = 3.3f;
        private eColor m_Color;
        private eDoorsNumber m_DoorsNumber;

        public Car(string i_LisenceID, string i_ModelName, Engine i_Engine)
            : base(i_LisenceID, i_ModelName, i_Engine)
        { 
            base.WheelsList = AddWheels(k_CarAmountOfWheels, k_CarMaxAirPressure); 
        }

        public static float CarMaxAirPressure
        {
            get
            {
                return k_CarMaxAirPressure;
            }
        }

        public static int CarAmountOfWheels
        {
            get
            {
                return k_CarAmountOfWheels;
            }
        }

        public static eFuelType CarFuelType
        {
            get
            {
                return k_CarFuelType;
            }
        }

        public static float CarMaxFuelTank
        {
            get
            {
                return k_CarMaxFuelTank;
            }
        }

        public static float CarMaxBatteryTime
        {
            get
            {
                return k_CarMaxBatteryTime;
            }
        }

        public eColor CarColor
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public eDoorsNumber DoorsNumber
        {
            get
            {
                return m_DoorsNumber;
            }
            set
            {
                m_DoorsNumber = value;
            }
        }

        public override string ToString()
        {
            string carDisplay = String.Format(@"
{0}
Color: {1}
number of doors: {2}", base.ToString(), this.CarColor.ToString(), this.DoorsNumber.ToString());

            return carDisplay;
        }
    }
}