using System;
using System.Collections.Generic;
using BusinessClicker.Logic;
using UnityEngine;

namespace BusinessClicker.UI.Views
{
    public class BusinessContentList : MonoBehaviour
    {
        public event Action<int> OnTryLevelUp;
        public event Action<int, BusinessUpgrade> OnTryUpgrade;
        
        [SerializeField] private RectTransform _content;
        [SerializeField] private BusinessView _businessViewPrefab;
        
        private List<BusinessView> _views = new();
        
        public void Constructor(int currentMoney, IEnumerable<(int, Business)> components)
        {
            int index = 0;
            
            foreach (var component in components)
            {
                BusinessView view = (_views.Count <= index ? CreateBusinessView() : _views[index]);
                view.Constructor(currentMoney, component);
                
                index += 1;
            }
        }

        private BusinessView CreateBusinessView()
        {
            var view = Instantiate(_businessViewPrefab, _content);
            
            view.OnTryLevelUp += TryLevelUpButtonClick;
            view.OnTryUpgrade += TryUpgradeButtonClick;
            
            _views.Add(view);
            return view;
        }

        private void ClearViews()
        {
            foreach (var view in _views)
            {
                view.OnTryLevelUp -= TryLevelUpButtonClick;
                view.OnTryUpgrade -= TryUpgradeButtonClick;
                
                Destroy(view.gameObject);
            }

            _views.Clear();
        }

        private void TryLevelUpButtonClick(int entityIndex)
        {
            OnTryLevelUp?.Invoke(entityIndex);
        }
        
        private void TryUpgradeButtonClick(int entityIndex, BusinessUpgrade upgrade)
        {
            OnTryUpgrade?.Invoke(entityIndex, upgrade);
        }

        private void OnDestroy()
            => ClearViews();
    }
}