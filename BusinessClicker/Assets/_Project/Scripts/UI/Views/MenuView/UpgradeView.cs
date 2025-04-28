using System;
using System.Collections;
using System.Collections.Generic;
using BusinessClicker.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    public event Action<int, BusinessUpgrade> OnTryBuyUpgrade;
    
    [SerializeField] private Image _colorLayer;
    [Space]
    [SerializeField] private Color _unbuyedColor;
    [SerializeField] private Color _buyedColor;
    [Space]
    [SerializeField] private Button _button;
    [Space]
    [SerializeField] private string _titleFormat;
    [SerializeField] private TextMeshProUGUI _title;
    [Space]
    [SerializeField] private string _priceFormat;
    [SerializeField] private TextMeshProUGUI _priceTitle;
    [Space]
    [SerializeField] private string _incomeFormat;
    [SerializeField] private TextMeshProUGUI _incomeTitle;
    [Space] 
    [SerializeField] private GameObject _buyedLayer;
    [SerializeField] private GameObject _unbuyedLayer;

    private int _entityId;
    private BusinessUpgrade _businessUpgrade;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(TryBuyUpgrade);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryBuyUpgrade);
    }

    private void TryBuyUpgrade()
    {
        OnTryBuyUpgrade?.Invoke(_entityId, _businessUpgrade);
    }

    public void Constructor(int money, int entityId, BusinessUpgrade upgrade)
    {
        bool hasEnough = money >= upgrade.Price;
        bool purchased = upgrade.Purchased;
        
        if (upgrade.Purchased)
            hasEnough = false;
        
        _buyedLayer.SetActive(purchased);
        _unbuyedLayer.SetActive(!purchased);
        
        _button.interactable = hasEnough;
        
        _entityId = entityId;
        _businessUpgrade = upgrade;
        
        _colorLayer.color = purchased ?  _buyedColor : _unbuyedColor;

        _title.text = String.Format(_titleFormat, upgrade.Title);
        _priceTitle.text = String.Format(_priceFormat, upgrade.Price);
        _incomeTitle.text = String.Format(_incomeFormat, (int)(upgrade.UpgradePercentage * 100f));
    }
}
