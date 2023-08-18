using System;
using static GarageLogic.Enums;

namespace GarageLogic
{

    public class VehicleCreation
    {
        public static Vehicle CreateVehicle(string i_LicenseID, string i_ModelName, eVehicleType i_VehicleType, eEngineType i_EngineType)
        {
            Vehicle vehicle = null;
            Engine engine = createEngine(i_VehicleType, i_EngineType);

            if (eVehicleType.Car.Equals(i_VehicleType))
            {
                vehicle = new Car(i_LicenseID, i_ModelName, engine);
            }
            else if (eVehicleType.MotorCycle.Equals(i_VehicleType))
            {
                vehicle = new MotorCycle(i_LicenseID, i_ModelName, engine);
                vehicle.AddWheels(MotorCycle.MotorCycleAmountOfWheels, MotorCycle.MotorCycleMaxAirPressure);
            }
            else if (eVehicleType.Truck.Equals(i_VehicleType))
            {
                vehicle = new Truck(i_LicenseID, i_ModelName, engine);
                vehicle.AddWheels(Truck.TruckAmountOfWheels, Truck.TruckMaxAirPressure);
            }
            vehicle.VehicleType = i_VehicleType;
            vehicle.EngineType = i_EngineType;

            return vehicle;
        }
        
        private static Engine createEngine(eVehicleType i_VehicleType, eEngineType i_EngineType)
        {
            Engine engine = null;

            if (eEngineType.Fuel.Equals(i_EngineType))
            {
                if (eVehicleType.Car.Equals(i_VehicleType))
                {
                    engine = new Fuel(Car.CarMaxFuelTank, 0f, Car.CarFuelType);
                    
                }
                else if ((eVehicleType.MotorCycle.Equals(i_VehicleType)))
                {
                    engine = new Fuel(MotorCycle.MotorCycleMaxFuelTank, 0f, MotorCycle.MotorCycleFuelType);
                }
                else if ((eVehicleType.Truck.Equals(i_VehicleType)))
                {
                    engine = new Fuel(Truck.TruckMaxFuelTank, 0f, Truck.TruckFuelType);
                }
            }
            else
            {
                if (eVehicleType.Car.Equals(i_VehicleType))
                {
                    engine = new Electric(Car.CarMaxBatteryTime, 0f);
                }
                else if (eVehicleType.MotorCycle.Equals(i_VehicleType))
                {
                    engine = new Electric(MotorCycle.MotorCycleMaxBatteryTime, 0f);
                }
            }

            return engine;
        }
    }
}
