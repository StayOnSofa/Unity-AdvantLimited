using System;
using System.Collections.Generic;
using System.Linq;
using BusinessClicker.SaveStorage;
using Leopotam.EcsLite;
using UnityEngine;

namespace BusinessClicker.Logic
{
    public class EcsBusinessLogic : MonoBehaviour
    {
        public event Action OnRefresh;
        
        [SerializeField] private BusinessContainerScriptable _config;

        private EcsWorld _world;
        private EcsSystems _systems;

        private BusinessSaveLogic _saveLogic;
        private Wallet _wallet;
        
        public IEnumerable<Business> GetBusinesses()
        {
            var businessPool = _world.GetPool<Business>();
            var filter = _world.Filter<Business>().End();
            
            foreach (var entity in filter)
                yield return businessPool.Get(entity);
        }

        public IEnumerable<(int index, Business component)> GetTupleBusinesses()
        {
            var businessPool = _world.GetPool<Business>();
            var filter = _world.Filter<Business>().End();
            
            foreach (var entity in filter)
                yield return (entity, businessPool.Get(entity));
        }

        public int GetMoney()
            => _wallet.Money;
        
        public void Constructor(ILocalStorage storage)
        {
            _saveLogic = new BusinessSaveLogic(storage);
            
            _world = new EcsWorld();
            _wallet = _saveLogic.LoadOrCreateWallet();
            
            _systems = new EcsSystems(_world, _wallet);
            _systems.Add(new BusinessIncomeSystem());

            LoadOrCreateBusinesses();
            _systems.Init();
        }

        private void LoadOrCreateBusinesses()
        {
            var businessPool = _world.GetPool<Business>();

            if (!_saveLogic.TryLoadBusinesses(out var businesses))
                businesses = _config.GetBusinesses();

            foreach (var b in businesses)
            {
                int entity = _world.NewEntity();
                    
                ref Business business = ref businessPool.Add(entity);
                business = b;
            }
        }
        
        private void Update()
        {
            if (_systems == null)
                return;
            
            _systems.Run();
            OnRefresh?.Invoke();
        }

        private void SaveProgress()
        {
            _saveLogic.SaveWallet(_wallet);
            _saveLogic.SaveBusinesses(GetBusinesses().ToArray());
        }

        private void DestroyAllEntities()
        {
            int[] array = null;
            int count = _world.GetAllEntities(ref array);

            for (int i = 0; i < count; i++)
            {
                var index = array[i];
                _world.DelEntity(index);
            }
        }

        public void ClearProgress()
        {
            _saveLogic.ClearProgress();
            _wallet.Money = 0;

            DestroyAllEntities();
            LoadOrCreateBusinesses();
            
            OnRefresh?.Invoke();
        }

        private void OnDestroy()
        {
            SaveProgress();
            
            _systems?.Destroy();
            _world?.Destroy();
        }

        public void TryLevelUp(int entityIndex)
        {
            var businessPool = _world.GetPool<Business>();
            ref var business = ref businessPool.Get(entityIndex);

            int money = GetMoney();
            int needToUpgrade = business.CurrentLevelPrice();

            if (money >= needToUpgrade)
            {
                money -= needToUpgrade;
                business.Level += 1;
                
                _wallet.Money = money;
            }
            
            OnRefresh?.Invoke();
        }

        public void TryUpgrade(int entityIndex, BusinessUpgrade toBuy)
        {
            var businessPool = _world.GetPool<Business>();
            ref var business = ref businessPool.Get(entityIndex);
            
            if (business.Upgrade1.Equals(toBuy))
            {
                TryBuyUpgrade(ref business.Upgrade1);
                return;
            }

            if (business.Upgrade2.Equals(toBuy))
                TryBuyUpgrade(ref business.Upgrade2);
        }

        private void TryBuyUpgrade(ref BusinessUpgrade upgrade)
        {
            int money = GetMoney();
            int price = upgrade.Price;

            if (money >= price)
            {
                money -= price;
                upgrade.Purchased = true;
                
                _wallet.Money = money;
            }
        }
    }
}