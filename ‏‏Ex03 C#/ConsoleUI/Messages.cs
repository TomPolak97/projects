using System;
using static GarageLogic.Enums;

namespace ConsoleUI
{
    public class Messages
    {
        public static void PrintFirstMenu()
        {
            string PrintFirstMenu = String.Format(@"hello, please choose one of the following option:

- for add a new vehicle to the garage - Please press '1'

- for displaing all vehicles in the garage (showing their lisence id) - Please press '2'

- for displaing all vehicles in the garage (showing their lisence id), sorting by state in the garage - Please press '3'

- for changing your vehcile's state - Please press '4'

- for fill air in your vehicle's wheels - Please press '5'

- for fill your tank - Please press '6'

- for recharge your battery - Please press '7'

- for getting full data about your vehicle - Please press '8'");
            Console.WriteLine(PrintFirstMenu);
        }

        public static void EnterLicenseId()
        {
            Console.WriteLine("pleae enter your licence ID");
        }

        public static void FailedLicense()
        {
            Console.WriteLine("your licence ID isn't valid, please try again");
        }

        public static void FailedState()
        {
            Console.WriteLine("your state isn't valid. Please choose a valid state from the menu");
        }
      
        public static void EnterAmount()
        {
            Console.WriteLine("please enter the amount you want to add");
        }

        public static void EnterModelName()
        {
            Console.WriteLine("pleae enter your ModelName");
        }

        public static void ChangedState(eState i_State)
        {
            Console.WriteLine("Your vehicle's status changed to " + i_State.ToString());
        }

        public static void FilledFuel(float i_CurrentFuelAmount)
        {
            Console.WriteLine("Your vehicle's fuel amount is now " + i_CurrentFuelAmount.ToString());
        }

        public static void FilledBattery(float i_CurrentBatteryAmount)
        {
            Console.WriteLine("Your vehicle's battery amount is now " + i_CurrentBatteryAmount.ToString());
        }

        public static void StateMenu()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - In repair
1 - Fixed
2 - Payed");
            Console.WriteLine(options);
        }

        public static void ColorMenu()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - Red
1 - White
2 - Green
3 - Blue");
            Console.WriteLine(options);
        }

        public static void LicenseTypeMenu()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - A
1 - A1
2 - B1
3 - BB");
            Console.WriteLine(options);
        }

        public static void DoorsNumberMenu()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - Two
1 - Three
2 - Four
3 - Five");
            Console.WriteLine(options);
        }

        public static void FuelTypeMenu()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - Soler
1 - Octan95
2 - Octan96
3 - Octan98");
            Console.WriteLine(options);
        }

        public static void EngineTypeMenu()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - Fuel
1 - Electric");
            Console.WriteLine(options);
        }

        public static void VehicleTypeMenu()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - Car
1 - MotorCycle
2 - Truck");
            Console.WriteLine(options);
        }

        public static void FailedFillAir()
        {
            Console.WriteLine("your input is invalid, please enter a valid amount of preasure to add");
        }

        public static void EnterOwnerName()
        {
            Console.WriteLine("please enter your name");
        }

        public static void FailedOwnerName()
        {
            Console.WriteLine("you entered a invalid name, please try again");
        }

        public static void EnterOwnerPhoneNumber()
        {
            Console.WriteLine("please enter your phone number");
        }

        public static void FailedOwnerPhoneNumber()
        {
            Console.WriteLine("you entered a invalied phone number, please try again");
        }

        public static void ManuValidFroze()
        {
            string options = String.Format(@"
Please choose one of the following:
0 - yes, i want to contain cool cargo in my truck
1 - no, i don't want to contain cool cargo in my truck");
            Console.WriteLine(options);
        }

        public static void EnterCargoVolume()
        {
            Console.WriteLine("please enter your cargo volume");
        }

        public static void EnterCurrentFuel()
        {
            Console.WriteLine("please enter your current amount of fuel in your vehcile");
        }

        public static void VehicleInGarage()
        {
            Console.WriteLine("Your car has entered the garage successfully !!!");
        }
        public static void EnterCurrentBatteryLeft()
        {
            Console.WriteLine("please enter your current battery time left in your vehcile");
        }
        public static void EnterEngineVolume()
        {
            Console.WriteLine("please enter the engine volume of your motorcycle");
        }

        public static void FailedEngineVolume()
        {
            Console.WriteLine("you entered a wrong engine volume. please try again");
        }
    }
}