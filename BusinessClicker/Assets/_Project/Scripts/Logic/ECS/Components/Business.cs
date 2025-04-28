using System;
using UnityEngine;

namespace BusinessClicker.Logic
{
    [Serializable]
    public struct Business
    {
        public string Title;

        public int Level;
        
        public int BasicIncome;
        public int BasicPrice;
        
        public BusinessUpgrade Upgrade1;
        public BusinessUpgrade Upgrade2;
        
        public int IncomeDelayInSeconds;
        
        [HideInInspector] public float Timer;

        public bool IsPurchased()
            => Level > 0;
        
        public int Income()
            => (int)(Level * BasicIncome * (1f + Upgrade1.GetUpgradePercentage() + Upgrade2.GetUpgradePercentage()));
        
        public int CurrentLevelPrice()
            =>  (Level + 1) * BasicPrice;
    }
}