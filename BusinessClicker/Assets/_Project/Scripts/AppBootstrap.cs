using BusinessClicker.UI;
using BusinessClicker.UI.Views;
using UnityEngine;

namespace BusinessClicker
{
    public class AppBootstrap : MonoBehaviour
    {
        [SerializeField] private AppDependencies _appDependencies;
        [SerializeField] private MenuView _menuPrefab;

        private UIContainer _uiContainer => _appDependencies.UIContainer;
        
        public void Setup()
        {
            _uiContainer.Show(_menuPrefab);
        }
    }
}