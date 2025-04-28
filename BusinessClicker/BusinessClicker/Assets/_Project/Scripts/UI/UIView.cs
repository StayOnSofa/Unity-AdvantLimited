using UnityEngine;

namespace BusinessClicker.UI
{
    public class UIView : MonoBehaviour
    {
        protected AppDependencies Dependencies;
        
        public void Constructor(AppDependencies appDependencies)
        {
            Dependencies = appDependencies;
        }
        
        public void SetActive(bool state)
            =>  gameObject.SetActive(state);
    }
}