using UnityEngine;

namespace BusinessClicker.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private RectTransform _viewLayer;
        private AppDependencies _dependencies;
        
        public void Constructor(AppDependencies appDependencies)
        {
            _dependencies = appDependencies;
        }

        public T Show<T>(T view) where T : UIView
        {
            view.SetActive(false);
            var instance = Instantiate(view, _viewLayer);
        
            instance.Constructor(_dependencies);
            instance.SetActive(true);
            view.SetActive(true);
            
            return instance;
        }
    }
}