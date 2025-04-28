using System;
using System.Collections;
using System.Collections.Generic;
using BusinessClicker.Logic;
using BusinessClicker.SaveStorage;
using BusinessClicker.UI;
using UnityEngine;
using UnityEngine.Events;

namespace BusinessClicker 
{
    public class AppDependencies : MonoBehaviour
    {
        [field: SerializeField ]public UnityEvent OnFinish { private set; get; }
     
        [field: SerializeField] public UIContainer UIContainer { get; private set; }
        [field: SerializeField] public EcsBusinessLogic EcsBusinessLogic  { get; private set; }

        public ILocalStorage LocalStorage { private set; get; }

        private void Awake()
            => Setup();
        
        private void Start()
            => OnFinish?.Invoke();

        private void Setup()
        {
            LocalStorage = new JsonStorage();
            
            UIContainer.Constructor(this);
            EcsBusinessLogic.Constructor(LocalStorage);
        }
    }
}