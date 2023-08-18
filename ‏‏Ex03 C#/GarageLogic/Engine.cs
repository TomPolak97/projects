using System;

namespace GarageLogic
{
    public abstract class Engine
    {
        public float UpdateRemainingEnergyPercentage(float i_currentEnergy, float i_MaxEnergy)
        {
            return (i_currentEnergy / i_MaxEnergy) * 100;
        }

        public abstract void FillEnergy(float i_EnergyToAdd);
    }
}