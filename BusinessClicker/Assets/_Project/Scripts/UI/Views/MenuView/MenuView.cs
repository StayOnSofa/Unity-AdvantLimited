using BusinessClicker.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BusinessClicker.UI.Views
{
    public class MenuView : UIView
    {
        [SerializeField] private BusinessContentList _contentList;
        [Space] 
        [SerializeField] private string _moneyFormat = "Balance: {0}$";
        [SerializeField] private TextMeshProUGUI _money;
        [SerializeField] private Button _clearProgressButton;
        
        private EcsBusinessLogic _businessLogic => Dependencies.EcsBusinessLogic;
        
        private void Start()
            => Refresh();

        private void OnEnable()
        {
            _businessLogic.OnRefresh += Refresh;
            
            _contentList.OnTryLevelUp += OnTryLevelUp;
            _contentList.OnTryUpgrade += OnTryUpgrade;
            
            _clearProgressButton.onClick.AddListener(ClearProgress);
        }
        
        private void OnDisable()
        {
            _businessLogic.OnRefresh -= Refresh;
            
            _contentList.OnTryLevelUp -= OnTryLevelUp;
            _contentList.OnTryUpgrade -= OnTryUpgrade;
            
            _clearProgressButton.onClick.RemoveListener(ClearProgress);
        }
        
        private void ClearProgress()
            => _businessLogic.ClearProgress();

        private void OnTryLevelUp(int entityIndex)
        {
            _businessLogic.TryLevelUp(entityIndex);
        }
        
        private void OnTryUpgrade(int entityIndex, BusinessUpgrade upgrade)
        {
            _businessLogic.TryUpgrade(entityIndex, upgrade);
        }

        private void Refresh()
        {
            _contentList.Constructor(_businessLogic.GetMoney(), _businessLogic.GetTupleBusinesses());
            _money.text = string.Format(_moneyFormat, _businessLogic.GetMoney());
        }
    }
}