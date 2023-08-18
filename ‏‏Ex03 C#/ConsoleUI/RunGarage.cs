using System;
using GarageLogic;
using static GarageLogic.Enums;

namespace ConsoleUI
{

    public class RunGarage
    {
        Garage garage = new Garage();

        public void Run()
        {
            Messages.PrintFirstMenu();
            string choice = Console.ReadLine();

            Console.Clear();
            switch (choice)
            {
                case "1":
                    addVehicleToGarage();
                    break;

                case "2":
                    displayAllVehicles();
                    break;

                case "3":
                    displayAllVehiclesByStatus();
                    break;

                case "4":
                    changeStatus();
                    break;

                case "5":
                    fillAir();
                    break;

                case "6":
                    fillTank();
                    break;

                case "7":
                    chargeBattery();
                    break;

                case "8":
                    displayVehicle();
                    break;
            }

            Console.WriteLine("Press enter to go back to menu");
            Console.ReadLine();
            Console.Clear();
            Run();
        }

        private void displayAllVehicles()
        {
            garage.DisplayAllVehicles();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private void displayAllVehiclesByStatus()
        {
            garage.DisplayVehicleListByStatus();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private void changeStatus()
        {
            int correctOption = 0;
            string correctInput;

            Messages.EnterLicenseId();
            string licenseID = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(licenseID) || Garage.FindVehicle(licenseID, garage) == null)
            {
                Messages.FailedLicense();
                licenseID = Console.ReadLine();
            }

            Messages.StateMenu();
            while (true)
            {
                correctOption = getOption(2);
                correctInput = correctOption.ToString();
                try
                {
                    if (ValidationTests.IsValidState(correctInput))
                    {
                        break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Enum.TryParse<eState>(correctInput, out eState state);
            garage.ChangeVehicleState(licenseID, state , garage);
            Messages.ChangedState(state);
        }

        private void addVehicleToGarage()
        {
            int correctOption = 0;

            Messages.EnterLicenseId();
            string licenseID = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(licenseID))
            {
                Messages.FailedLicense();
                licenseID = Console.ReadLine();
            }

            Vehicle vehicle = Garage.FindVehicle(licenseID, garage);
            while (!ValidationTests.IsConatainOnlyNumbers(licenseID) ||
                (vehicle = Garage.FindVehicle(licenseID, garage)) != null)
            {
                Messages.FailedLicense();
                licenseID = Console.ReadLine();
            }

            if (vehicle == null)
            {
                Messages.EnterModelName();
                string modelName = Console.ReadLine();
                Messages.VehicleTypeMenu();
                eVehicleType newVehicleType = eVehicleType.Car;
                eEngineType newEngineType = eEngineType.Fuel;
                while (true)
                {
                    correctOption = getOption(2);
                    string correctOptionByString = correctOption.ToString();
                    try
                    {
                        if (ValidationTests.IsValidVehicleType(correctOptionByString))
                        {
                            Enum.TryParse<eVehicleType>(correctOptionByString, out newVehicleType);
                            break;
                        }
                    }

                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Messages.EngineTypeMenu();
                while (true)
                {
                    correctOption = getOption(1);
                    string correctOptionByString = correctOption.ToString();
                    try
                    {
                        if (ValidationTests.IsValidEngineType(correctOptionByString))
                        {
                            Enum.TryParse<eEngineType>(correctOptionByString, out newEngineType);
                            break;
                        }
                    }

                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                vehicle = VehicleCreation.CreateVehicle(licenseID, modelName, newVehicleType, newEngineType);
                additonalQuestionsForAllVehicles(vehicle);
                gettingExtraDetailsAccordingToVehicleType(vehicle,newVehicleType);
                gettingExtraDetailsAccordingToEngineType(vehicle, newEngineType);
                garage.VehicleList.Add(vehicle);
                Messages.VehicleInGarage();
            }
        }

        private void fillAir()
        {
            Vehicle vehicle = null;
            float correctAmount = 0f;

            Messages.EnterLicenseId();
            string licenseID = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(licenseID) || (vehicle = Garage.FindVehicle(licenseID, garage)) == null)
            {
                Messages.FailedLicense();
                licenseID = Console.ReadLine();
            }

            currentAir(licenseID);
            while (true)
            {
                Messages.EnterAmount();
                string amount = Console.ReadLine();
                try
                {
                    if (ValidationTests.ParseFloat(amount))
                    {
                        float.TryParse(amount, out correctAmount);
                        if (!ValidationTests.ValueOutOfRange(correctAmount, 0f
                            , vehicle.WheelsList[0].MaxAirPressure - vehicle.WheelsList[0].CurrentAirPressure))
                        {
                            break;
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

                garage.FillAir(licenseID, correctAmount, garage);
                currentAir(licenseID);
        }
       
        private void fillTank()
        {
            Vehicle vehicle = null;
            float correctAmount = 0f;
            string fuelType;
            eFuelType validFuel; 

            Messages.EnterLicenseId();
            string licenseID = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(licenseID) || 
                (vehicle = Garage.FindVehicle(licenseID, garage)) == null || ValidationTests.IsVehicleElectric(vehicle))
            {
                Messages.FailedLicense();
                licenseID = Console.ReadLine();
            }

            while (true)
            {
                Messages.FuelTypeMenu();
                fuelType = Console.ReadLine();

                try
                {
                    if (ValidationTests.IsCorrectFuelType(vehicle, fuelType))
                    {
                        Messages.EnterAmount();
                        string amount = Console.ReadLine();
                        if (ValidationTests.ParseFloat(amount))
                        {
                            float.TryParse(amount, out correctAmount);

                            if (!ValidationTests.ValueOutOfRange(correctAmount, 0f,
                            vehicle.WheelsList[0].MaxAirPressure - vehicle.WheelsList[0].CurrentAirPressure))
                            {
                                break;
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Enum.TryParse<eFuelType>(fuelType, out validFuel);
            Garage.FillTank(licenseID, validFuel, correctAmount, garage);
            Messages.FilledFuel((vehicle.Engine as Fuel).CurrentFuel);
        }

        private void chargeBattery()
        {
            Vehicle vehicle = null;
            float correctAmount = 0f;
    
            Messages.EnterLicenseId();
            string licenseID = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(licenseID) ||
                (vehicle = Garage.FindVehicle(licenseID, garage)) == null || !ValidationTests.IsVehicleElectric(vehicle))
            {
                Messages.FailedLicense();
                licenseID = Console.ReadLine();
            }

            while (true)
            {
                Messages.EnterAmount();
                string amount = Console.ReadLine();

                try
                {
                    if (ValidationTests.ParseFloat(amount))
                    {
                        float.TryParse(amount, out correctAmount);
                        if (!ValidationTests.ValueOutOfRange(correctAmount, 0f,
                            vehicle.WheelsList[0].MaxAirPressure - vehicle.WheelsList[0].CurrentAirPressure))
                        {
                            break;
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Garage.ChargeBattery(licenseID, correctAmount, garage);
            Messages.FilledBattery((vehicle.Engine as Electric).CurrentBatteryTime);
        }

        private void displayVehicle()
        {
            Vehicle vehicle;

            Messages.EnterLicenseId();
            string licenseID = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(licenseID) || (vehicle = Garage.FindVehicle(licenseID, garage)) == null)
            {
                Messages.FailedLicense();
                licenseID = Console.ReadLine();
            }

            Console.WriteLine(vehicle.ToString());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private static int getOption(int i_MaxOption)
        {
            int option = 0;
            string input = Console.ReadLine();

            while (!int.TryParse(input, out option) || option < 0 || option > i_MaxOption)
            {
                Messages.FailedState();
                input = Console.ReadLine();
            }

            return option;
        }

        private void currentAir(string i_LicenseID)
        {
            Vehicle vehicle = Garage.FindVehicle(i_LicenseID, garage);
            Console.WriteLine("Your vehicle's current air pressure is " + vehicle.WheelsList[0].CurrentAirPressure);
        }

        private static void additonalQuestionsForAllVehicles(Vehicle i_Vehicle)
        {
            float airToAdd = 0f;

            Console.WriteLine("please enter the name of your wheels' manufacturer");
            string manufacturNameFromUser = Console.ReadLine();
            Console.WriteLine("please enter the current air pressure of your vehcile");
            bool isCorrectAirPressure = float.TryParse(Console.ReadLine(), out airToAdd);
            while (true)
            {
                if (isCorrectAirPressure)
                {
                    try
                    {
                        foreach (Wheel wheel in i_Vehicle.WheelsList)
                        {
                            wheel.Pump(airToAdd);
                            wheel.ManufacturerName = manufacturNameFromUser;
                        }
                        break;
                    }

                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        isCorrectAirPressure = float.TryParse(Console.ReadLine(), out airToAdd);
                    }
                }
                else
                {
                    Messages.FailedFillAir();
                    isCorrectAirPressure = float.TryParse(Console.ReadLine(), out airToAdd);
                }
            }

            //asking for owner's name
            Messages.EnterOwnerName();
            string OwnerName = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyLetters(OwnerName))
            {
                Messages.FailedOwnerName();
                OwnerName = Console.ReadLine();
            }

            i_Vehicle.OwnerName = OwnerName;
            //asking for owner's phone number
            Messages.EnterOwnerPhoneNumber();
            string phoneNumber = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(phoneNumber))
            {
                Messages.FailedOwnerPhoneNumber();
                phoneNumber = Console.ReadLine();
            }

            i_Vehicle.OwnerPhone = phoneNumber;
        }

        private static void gettingExtraDetailsAccordingToVehicleType(Vehicle i_Vehicle, eVehicleType i_VehicleType)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    carExtraDetails(i_Vehicle);
                    break;
                case eVehicleType.MotorCycle:
                    motorCycleExtraDetails(i_Vehicle);
                    break;
                case eVehicleType.Truck:
                    truckExtraDetails(i_Vehicle);
                    break;
            }
        }

        private static void gettingExtraDetailsAccordingToEngineType(Vehicle i_Vehicle,
            eEngineType i_EngineType)
        {
            switch (i_EngineType)
            {
                case eEngineType.Fuel:
                    fuelExtraDetails(i_Vehicle);
                    break;
                case eEngineType.Electric:
                    electricExtraDetails(i_Vehicle);
                    break;
            }
        }

        private static void carExtraDetails(Vehicle i_Vehicle)
        {
            //asking for number of doors - Car
            int CorrectOption = 0;

            Messages.DoorsNumberMenu();
            while (true)
            {
                CorrectOption = getOption(3);
                string CorrectOptionByString = CorrectOption.ToString();
                try
                {
                    if (ValidationTests.IsValideDoorsNumber(CorrectOptionByString))
                    {
                        Enum.TryParse<eDoorsNumber>(CorrectOptionByString, out eDoorsNumber doorsNum);
                        (i_Vehicle as Car).DoorsNumber = doorsNum;
                        break;
                    }
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //asking for color - Car
            Messages.ColorMenu();
            while (true)
            {
                CorrectOption = getOption(3);
                string CorrectOptionByString = CorrectOption.ToString();
                try
                {
                    if (ValidationTests.IsValidColor(CorrectOptionByString))
                    {
                        Enum.TryParse<eColor>(CorrectOptionByString, out eColor color);
                        (i_Vehicle as Car).CarColor = color;
                        break;
                    }
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void motorCycleExtraDetails(Vehicle i_Vehicle)
        {
            int CorrectOption = 0;

            Messages.LicenseTypeMenu();
            while (true)
            {
                CorrectOption = getOption(3);
                string CorrectOptionByString = CorrectOption.ToString();
                try
                {
                    if (ValidationTests.IsValidLicenseType(CorrectOptionByString))
                    {
                        Enum.TryParse<eLicenseType>(CorrectOptionByString, out eLicenseType licenseType);
                        (i_Vehicle as MotorCycle).LicenseType = licenseType;
                        break;
                    }
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // asking for engine volume - motorcycle
            Messages.EnterEngineVolume();
            string EngineVolume = Console.ReadLine();
            while (!ValidationTests.IsConatainOnlyNumbers(EngineVolume))
            {
                Messages.FailedEngineVolume();
                EngineVolume = Console.ReadLine();
            }

            (i_Vehicle as MotorCycle).EngineVolume = int.Parse(EngineVolume);
        }

        private static void truckExtraDetails(Vehicle i_Vehicle)
        {
            int CorrectOption = 0;
            float volume = 0f;

            //asking for imformation if contains frozen things - Truck
            Messages.ManuValidFroze();
            while (true)
            {
                CorrectOption = getOption(1);
                string CorrectOptionByString = CorrectOption.ToString();
                try
                {
                    if (ValidationTests.IsValidFrozen(CorrectOptionByString))
                    {
                        Enum.TryParse<eFreeze>(CorrectOptionByString, out eFreeze freeze);
                        (i_Vehicle as Truck).Freeze = freeze;
                        break;
                    }
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //asking for cargo volume - Truck
            Messages.EnterCargoVolume();
            while (true)
            {
                try
                {
                    string CargoVolume = Console.ReadLine();
                    if (ValidationTests.ParseFloat(CargoVolume))
                    {
                        float.TryParse(CargoVolume, out volume);
                        (i_Vehicle as Truck).CargoVolume = volume;
                        break;
                    }
                }

                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void fuelExtraDetails(Vehicle i_Vehicle)
        {
            float currentFuelFloat = 0f;

            Messages.EnterCurrentFuel();
            while (true)
            {
                try
                {
                    string currentFuelString = Console.ReadLine();
                    if (ValidationTests.ParseFloat(currentFuelString))
                    {
                        float.TryParse(currentFuelString, out currentFuelFloat);
                        if (i_Vehicle is Car)
                        {
                            if (ValidationTests.ValidCurrentEnergy(currentFuelFloat, Car.CarMaxFuelTank))
                            {
                                (i_Vehicle.Engine as Fuel).CurrentFuel = currentFuelFloat;
                                break;
                            }
                        }
                        else if (i_Vehicle is MotorCycle)
                        {
                            if (ValidationTests.ValidCurrentEnergy(currentFuelFloat, MotorCycle.MotorCycleMaxFuelTank))
                            {
                                (i_Vehicle.Engine as Fuel).CurrentFuel = currentFuelFloat;
                                break;
                            }
                        }
                        else if (i_Vehicle is Truck)
                        {
                            if (ValidationTests.ValidCurrentEnergy(currentFuelFloat, Truck.TruckMaxFuelTank))
                            {
                                (i_Vehicle.Engine as Fuel).CurrentFuel = currentFuelFloat;
                                break;
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void electricExtraDetails(Vehicle i_Vehicle)
        {
            float currentBatteryLeftFloat = 0f;

            Messages.EnterCurrentBatteryLeft();
            while (true)
            {
                try
                {
                    string currentBatteryLeftString = Console.ReadLine();
                    if (ValidationTests.ParseFloat(currentBatteryLeftString))
                    {
                        float.TryParse(currentBatteryLeftString, out currentBatteryLeftFloat);
                        if (i_Vehicle is Car)
                        {
                            if (ValidationTests.ValidCurrentEnergy(currentBatteryLeftFloat, Car.CarMaxBatteryTime))
                            {
                                (i_Vehicle.Engine as Electric).CurrentBatteryTime = currentBatteryLeftFloat;
                                break;
                            }
                        }
                        else if (i_Vehicle is MotorCycle)
                        {
                            if (ValidationTests.ValidCurrentEnergy(currentBatteryLeftFloat, MotorCycle.MotorCycleMaxBatteryTime))
                            {
                                (i_Vehicle.Engine as Electric).CurrentBatteryTime = currentBatteryLeftFloat;
                                break;
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
