using System;
using static GarageLogic.Enums;
namespace GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_TruckMaxAirPressure = 24f;
        private const int k_TruckAmountOfWheels = 16;
        private const eFuelType k_TruckFuelType = eFuelType.Soler;
        private const float k_TruckMaxFuelTank = 120;
        private eFreeze m_Freeze;
        private float m_CargoVolume;

        public Truck(string i_LisenceID, string i_ModelName, Engine i_Engine)
            : base(i_LisenceID, i_ModelName, i_Engine)
        {
            base.WheelsList = AddWheels(k_TruckAmountOfWheels, k_TruckMaxAirPressure); 
        }

        public static float TruckMaxAirPressure
        {
            get
            {
                return k_TruckMaxAirPressure;
            }
        }

        public static int TruckAmountOfWheels
        {
            get
            {
                return k_TruckAmountOfWheels;
            }
        }

        public static eFuelType TruckFuelType
        {
            get
            {
                return k_TruckFuelType;
            }
        }

        public static float TruckMaxFuelTank
        {
            get
            {
                return k_TruckMaxFuelTank;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                m_CargoVolume = value;
            }
        }

        public eFreeze Freeze
        {
            get
            {
                return m_Freeze;
            }
            set
            {
                m_Freeze = value;
            }
        }
        public override string ToString()
        {
            string truckDisplay = String.Format(@"
{0}
Can carry frozen items: {1}
Engine volume: {2}", base.ToString(), this.Freeze.ToString(), this.CargoVolume.ToString());

            return truckDisplay;
        }
    }
}