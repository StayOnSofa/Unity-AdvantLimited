using Leopotam.EcsLite;
using UnityEngine;

namespace BusinessClicker.Logic
{
    public class BusinessIncomeSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();

            var businessPool = world.GetPool<Business>();
            var businessFilter = world.Filter<Business>().End();

            var money = systems.GetShared<Wallet>();
            
            foreach (var businessEntity in businessFilter)
            {
                ref var business = ref businessPool.Get(businessEntity);

                if (!business.IsPurchased())
                    continue;
                
                business.Timer += Time.deltaTime;

                if (business.Timer >= business.IncomeDelayInSeconds)
                {
                    money.Money += business.Income();
                    business.Timer = 0;
                }
            }
        }
    }
}