using System;
using BusinessClicker.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BusinessClicker.UI.Views
{
    public class BusinessView : MonoBehaviour
    {
        public event Action<int> OnTryLevelUp;
        public event Action<int, BusinessUpgrade> OnTryUpgrade;
        
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Slider _timer;
        [Space] 
        [SerializeField] private GameObject _purchasedView;
        [SerializeField] private GameObject _unpurchasedView;
        [Space] 
        [SerializeField] private string _levelFormat = "lvl: {0}";
        [SerializeField] private TextMeshProUGUI _levelTitle;
        [Space] 
        [SerializeField] private string _buyLevelFormat = "Buy a level for {0}$";
        [SerializeField] private TextMeshProUGUI _buyLevelTitle;
        [SerializeField] private Button _levelUpButton;
        [Space]
        [SerializeField] private string _incomeFormat = "Income: {0}";
        [SerializeField] private TextMeshProUGUI _incomeTitle;
        [Space]
        [SerializeField] private UpgradeView _upgradeView1;
        [SerializeField] private UpgradeView _upgradeView2;
        
        private int _businessIndex;
        
        public void Constructor(int currentMoney, (int, Business) businessTuple)
        {
            _businessIndex = businessTuple.Item1;
            var business = businessTuple.Item2;

            int levelPrice = business.CurrentLevelPrice();
            bool hasEnough = currentMoney >= levelPrice;

            _levelUpButton.interactable = hasEnough;
            _buyLevelTitle.text = string.Format(_buyLevelFormat, levelPrice);
            _levelTitle.text =  string.Format(_levelFormat, business.Level);
            _incomeTitle.text = string.Format(_incomeFormat, business.Income());
            
            _title.text = business.Title;
            _timer.value = business.Timer / business.IncomeDelayInSeconds;
            
            _purchasedView.SetActive(business.IsPurchased());
            _unpurchasedView.SetActive(!business.IsPurchased());
            
            _upgradeView1.Constructor(currentMoney, _businessIndex, business.Upgrade1);
            _upgradeView2.Constructor(currentMoney, _businessIndex, business.Upgrade2);
        }

        private void OnEnable()
        {
            _levelUpButton.onClick.AddListener(LevelButtonClick);
         
            _upgradeView1.OnTryBuyUpgrade += UpgradeButtonClick;
            _upgradeView2.OnTryBuyUpgrade += UpgradeButtonClick;
        }

        private void OnDisable()
        {
            _levelUpButton.onClick.RemoveListener(LevelButtonClick);
            
            _upgradeView1.OnTryBuyUpgrade -= UpgradeButtonClick;
            _upgradeView2.OnTryBuyUpgrade -= UpgradeButtonClick;
        }

        private void LevelButtonClick()
        {
            OnTryLevelUp?.Invoke(_businessIndex);
        }
        
        private void UpgradeButtonClick(int index, BusinessUpgrade upgrade)
        {
            OnTryUpgrade?.Invoke(index, upgrade);
        }
    }
}