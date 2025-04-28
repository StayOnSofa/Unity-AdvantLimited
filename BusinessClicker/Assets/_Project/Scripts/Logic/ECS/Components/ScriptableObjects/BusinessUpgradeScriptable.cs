using System;
using UnityEngine;

namespace BusinessClicker.Logic
{
    [CreateAssetMenu(fileName = "BusinessUpgrade", menuName = "BusinessClicker/BusinessUpgrade", order = 0)]
    public class BusinessUpgradeScriptable : ScriptableObject
    {
        [field: SerializeField] public string Title { private set; get; }
        [field: Space]
        [field: SerializeField] public int Price { private set; get; }
        [field: SerializeField] public float UpgradePercentage { private set; get; }

        public BusinessUpgrade ToUpgrade()
        {
            return new BusinessUpgrade()
            {
                Title =  Title,
                Price =  Price,
                UpgradePercentage = UpgradePercentage,
                Purchased = false,
            };
        }
    }
}