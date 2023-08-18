using System;
using System.Collections.Generic;
using System.Text;
using static GarageLogic.Enums;

namespace GarageLogic
{

    public class Garage
    {
        private List<Vehicle> m_VehicleList = new List<Vehicle>();
        
        public List<Vehicle> VehicleList
        {
            get
            {
                return m_VehicleList;
            }
            set
            {
                m_VehicleList = value;
            }
        }

        public void AddVehicle(Vehicle i_Vehice)
        {
            VehicleList.Add(i_Vehice);
            i_Vehice.State = eState.InRepair;
        }

        public static Vehicle FindVehicle(string i_LicenseID, Garage i_Garage)
        {
            Vehicle foundVehicle = null;

            foreach (Vehicle vehicle in i_Garage.VehicleList)
            {
                if (vehicle.LicenseID.Equals(i_LicenseID))
                {
                    foundVehicle = vehicle;
                }
            }

            return foundVehicle;
        }

        public void DisplayVehicleListByStatus()
        {
            StringBuilder vehiclesStringBuilder = new StringBuilder();

            vehiclesStringBuilder.Append("[");
            foreach (object item in Enum.GetNames(typeof(Enums.eState)))
            {
                foreach(Vehicle vehicle in this.VehicleList)
                {
                    if (item.Equals(vehicle.State.ToString()))
                    {
                        vehiclesStringBuilder.Append(vehicle.LicenseID);
                        vehiclesStringBuilder.Append(",");
                    }
                }
            }

            vehiclesStringBuilder.Append("]");
            Console.WriteLine(vehiclesStringBuilder);
        }
        
        public void ChangeVehicleState(string i_LicenseID, eState i_eState,Garage i_Garage)
        {
            Vehicle vehicle = FindVehicle(i_LicenseID, i_Garage);

            if (vehicle != null)
            {
                vehicle.State = i_eState;
            }
        }

        public void FillAir(string i_LicenseID, float amount, Garage i_Garage)
        {
            Vehicle vehicle = FindVehicle(i_LicenseID, i_Garage);

            if (vehicle != null)
            {
                foreach (Wheel wheel in vehicle.WheelsList)
                {
                    wheel.Pump(amount);
                }
            }
        }

        public static void FillTank(string i_LicenseID, eFuelType i_eFuelType, float i_FuelToAdd, Garage i_Garage)
        {
            Vehicle vehicle = FindVehicle(i_LicenseID, i_Garage);
            vehicle.Engine.FillEnergy(i_FuelToAdd);
        }

        public static void ChargeBattery(string i_LicenseID, float i_MinutesToAdd, Garage i_Garage)
        {
            Vehicle vehicle = FindVehicle(i_LicenseID, i_Garage);
            vehicle.Engine.FillEnergy(i_MinutesToAdd);
        }

        public void DisplayAllVehicles()
        {
            StringBuilder vehiclesStringBuilder = new StringBuilder();

            vehiclesStringBuilder.Append("[");
            foreach (Vehicle vehicle in this.VehicleList)
            {
                vehiclesStringBuilder.Append(vehicle.LicenseID);
                vehiclesStringBuilder.Append(",");
            }

            vehiclesStringBuilder.Append("]");
            Console.WriteLine(vehiclesStringBuilder);
        }
    }
}

