using System;

namespace GarageLogic
{
    public class Enums
    {
        public enum eState
        {
            InRepair,
            Fixed,
            Payed
        }

        public enum eColor
        {
            Red,
            White,
            Green,
            Blue
        }

        public enum eLicenseType
        {
            A,
            A1,
            B1,
            BB
        }

        public enum eDoorsNumber
        {
            Two,
            Three,
            Four,
            Five
        }

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public enum eEngineType
        {
            Fuel,
            Electric
        }

        public enum eVehicleType
        {
            Car,
            MotorCycle,
            Truck
        }

        public enum eFreeze
        {
            Yes,
            No
        }
    }
}
