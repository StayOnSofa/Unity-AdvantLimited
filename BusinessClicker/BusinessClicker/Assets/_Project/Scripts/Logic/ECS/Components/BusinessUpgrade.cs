using System;
using UnityEngine;

namespace BusinessClicker.Logic
{
    [Serializable]
    public struct BusinessUpgrade : IEquatable<BusinessUpgrade>
    {
        public string Title;
        public int Price;
        public float UpgradePercentage;

        [HideInInspector] public bool Purchased;
        
        public float GetUpgradePercentage()
            => Purchased ? UpgradePercentage : 0;

        public bool Equals(BusinessUpgrade other)
            => Title == other.Title && Price == other.Price && UpgradePercentage.Equals(other.UpgradePercentage);

        public override bool Equals(object obj)
            => obj is BusinessUpgrade other && Equals(other);
        
        public override int GetHashCode()
            => HashCode.Combine(Title, Price, UpgradePercentage, Purchased);
    }

}