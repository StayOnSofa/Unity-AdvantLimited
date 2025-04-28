using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BusinessClicker.Logic
{
    [CreateAssetMenu(fileName = "BusinessContainer", menuName = "BusinessClicker/BusinessContainer", order = 2)]
    public class BusinessContainerScriptable : ScriptableObject
    {
        [field: SerializeField] public BusinessScriptable[] Scriptable { private set; get; }
        
        public Business[] GetBusinesses()
        {
            return ToEnumerable().ToArray();
            
            IEnumerable<Business> ToEnumerable()
            {
                foreach (var s in Scriptable)
                    yield return s.ToBusiness();
            }
        }
    }
}