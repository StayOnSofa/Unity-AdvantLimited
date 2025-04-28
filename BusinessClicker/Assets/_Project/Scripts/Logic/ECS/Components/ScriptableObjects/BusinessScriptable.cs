using UnityEngine;

namespace BusinessClicker.Logic
{
    [CreateAssetMenu(fileName = "Business", menuName = "BusinessClicker/Business", order = 1)]
    public class BusinessScriptable : ScriptableObject
    {
        [field: SerializeField] public string Title { private set; get; }
        [field: Space]
        [field: SerializeField] public int StartingLevel { private set; get; }
        [field: SerializeField] public int DefaultIncome { private set; get; }
        [field: SerializeField] public int DefaultPrice { private set; get; }
        [field: SerializeField] public int IncomeDelayInSeconds { private set; get; }
        [field: Space]
        [field: SerializeField] public BusinessUpgradeScriptable Upgrade1 { private set; get; }
        [field: SerializeField] public BusinessUpgradeScriptable Upgrade2 { private set; get; }

        public Business ToBusiness()
        {
            return new Business
            {
                Title = Title,
                Level = StartingLevel,

                Upgrade1 = Upgrade1.ToUpgrade(),
                Upgrade2 = Upgrade2.ToUpgrade(),

                IncomeDelayInSeconds = IncomeDelayInSeconds,
              
                BasicIncome = DefaultIncome,
                BasicPrice = DefaultPrice,
            };
        }
    }
}