using System;
using static GarageLogic.Enums;

namespace GarageLogic
{ 
    public class ValidationTests
    {
        public static bool IsConatainOnlyNumbers(string i_Input)
        { 
            bool ConatainOnlyNumbers = true;
  
            foreach (char c in i_Input)
            {
                if (!Char.IsNumber(c))
                {
                    ConatainOnlyNumbers = false;
                }
            }
           
            return ConatainOnlyNumbers;
        }

        public static bool IsConatainOnlyLetters(string i_Input)
        {
            bool ConatainOnlyLetters = true;

            foreach (char c in i_Input)
            {
                if (!Char.IsLetter(c))
                {
                    ConatainOnlyLetters = false;
                }
            }

            return ConatainOnlyLetters;
        }

        public static bool IsValidState(string i_Input)
        {
            bool valid = true;

            if (!Enum.TryParse<eState>(i_Input, out eState state))
            {
                valid = false;
                throw new FormatException("Your entered an invalid state, please enter again");
            }

            return valid;
        }

        public static bool IsValidColor(string i_Input)
        {
            bool validArgumentColor = false;
            eColor color;

            foreach (object item in Enum.GetNames(typeof(Enums.eColor)))
            {
                Enum.TryParse(i_Input, out color);
                if (item.Equals(color.ToString()))
                {
                    validArgumentColor = true;
                }
            }
            if (!validArgumentColor)
            {
                throw new ArgumentException("you didn't mention a valid color, please try again");
            }

            return validArgumentColor;
        }

        public static bool IsValidLicenseType(string i_Input)
        {
            bool validArgumentLicenseType = false;
            eLicenseType license;

            foreach (object item in Enum.GetNames(typeof(Enums.eLicenseType)))
            {
                Enum.TryParse(i_Input, out license);
                if (item.Equals(license.ToString()))
                {
                    validArgumentLicenseType = true;
                }
            }
            if (!validArgumentLicenseType)
            {
                throw new ArgumentException("you didn't mention a valid option, please try again");
            }

            return validArgumentLicenseType;
        }

        public static bool IsVehicleElectric(Vehicle i_Vehicle)
        {
            bool valid = true;

            if (i_Vehicle.Engine is Fuel)
            {
                valid = false;
                //throw new ArgumentException("You cant fill up electric car with fuel");
            }

            return valid;
        }

        public static bool IsValideDoorsNumber(string i_Input)
        {
            bool validDoorsNumber = false;
            eDoorsNumber doors;

            foreach (object item in Enum.GetNames(typeof(Enums.eDoorsNumber)))
            {
                Enum.TryParse(i_Input, out doors);
                if (item.Equals(doors.ToString()))
                {
                    validDoorsNumber = true;
                }
            }
            if (!validDoorsNumber)
            {
                throw new ArgumentException("you didn't mention a valid option, please try again");
            }

            return validDoorsNumber;
        }

        public static bool IsCorrectFuelType(Vehicle i_Vehicle, string i_FuelType)
        {
            bool isValid = true;
            eFuelType fuelType;

            if (!Enum.TryParse(i_FuelType, out fuelType))
            {
                isValid = false;
                throw new FormatException("Please choose a valid fuel type from the menu");
            }
            if (!(i_Vehicle.Engine as Fuel).FuelType.Equals(fuelType))
            {
                isValid = false;
                throw new ArgumentException("You chose the wrong fuel type. Please try again");
            }

            return isValid;
        }

        public static bool ParseFloat(string i_Amount)
        {
            float amount;
            bool succeeded = true;

            if(!float.TryParse(i_Amount, out amount))
            {
                succeeded = false;
                throw new FormatException("Please enter a valid amount");
            }

            return succeeded;
        }

        public static bool IsValidEngineType(string i_Input)
        {
            bool valid = true;

            if (!Enum.TryParse<eEngineType>(i_Input, out eEngineType engine))
            {
                valid = false;
                throw new ArgumentException("Please enter a valid engine type");
            }

            return valid;
        }
        public static bool IsValidVehicleType(string i_Input)
        {
            bool valid = true;
            
            if (!Enum.TryParse<eVehicleType>(i_Input, out eVehicleType type))
            {
                valid = false;
                throw new ArgumentException("Please enter a valid vehicle type");
            }

            return valid;
        }

        public static bool IsValidVState(string i_Input)
        {
            eState validState;

            return Enum.TryParse(i_Input, out validState);
        }

        public static bool ValueOutOfRange(float i_Amount, float i_Min, float i_Max)
        {
            bool valid = false;

            if (i_Amount < i_Min || i_Amount > i_Max)
            {
                valid = true;
                throw new ValueOutOfRangeException(i_Amount, i_Min, i_Max);
            }

            return valid;
        }

        public static bool ValidCurrentEnergy(float i_Amount, float i_Max)
        {
            bool valid = true;

            if (i_Amount < 0 || i_Amount > i_Max)
            {
                valid = false;
                throw new ValueOutOfRangeException(0, i_Max);
            }

            return valid;
        }

        public static bool IsValidFrozen(string i_Frozen)
        {
            bool valid = true;
           
            if(!Enum.TryParse<eFreeze>(i_Frozen, out eFreeze freeze))
            {
                valid = false;
                throw new ArgumentException("Please enter 0 or 1");
            }

            return valid;
        }
    }
}