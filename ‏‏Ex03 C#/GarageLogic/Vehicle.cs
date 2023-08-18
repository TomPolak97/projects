using System;
using System.Collections.Generic;
using static GarageLogic.Enums;

namespace GarageLogic
{
    public class Vehicle
    {
        private string m_OwnerName;
        private string m_OwnerPhone;
        private string m_ModelName;
        private string m_LicenseID;
        private Engine m_Engine;
        private eState m_eState;
        private eVehicleType m_eVehicleType;
        private eEngineType m_EngineType;
        private List<Wheel> m_WheelsList;

        public Vehicle(string i_LicenseID, string i_ModelName, Engine i_Engine)
        {
            this.m_WheelsList = new List<Wheel>(); 
            this.m_LicenseID = i_LicenseID;
            this.m_ModelName = i_ModelName;
            this.m_Engine = i_Engine;
            this.m_eState = eState.InRepair;
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }
            set
            {
                m_OwnerPhone = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseID
        {
            get
            {
                return m_LicenseID;
            }
            set
            {
                m_LicenseID = value;
            }
        }

        public eState State
        {
            get
            {
                return m_eState;
            }
            set
            {
                m_eState = value;
            }
        }

        public eEngineType EngineType
        {
            get
            {
                return m_EngineType;
            }
            set
            {
                m_EngineType = value;
            }
        }

        public List<Wheel> WheelsList
        {
            get
            {
                return m_WheelsList;
            }
            set
            {
                m_WheelsList = value;
            }
        }

        public Engine Engine
        {
            get 
            {
                return m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }

        public eVehicleType VehicleType
        {
            get
            {
                return m_eVehicleType;
            }
            set
            {
                m_eVehicleType = value;
            }
        }

        public List <Wheel> AddWheels(int i_NumOfWheels, float i_MaxAirPressure)
        {
            List <Wheel> wheels = new List <Wheel>();
            
            for(int i = 0; i < i_NumOfWheels; i++)
            {
                wheels.Add(new Wheel(i_MaxAirPressure));
            }

            return wheels;
        }

        public override string ToString()
        {
            string vehicleDisplay = string.Format(@"
License ID: {0}
Model name: {1}
Owner name: {2}
Status in garage: {3}
{4}
Energy state: {5}
", this.LicenseID, this.ModelName, this.OwnerName, this.State.ToString(), this.WheelsList[0].ToString(), this.Engine.ToString());

            return vehicleDisplay;
        }
    }
}
