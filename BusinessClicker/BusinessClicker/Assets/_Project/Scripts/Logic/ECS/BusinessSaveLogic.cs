using System;
using BusinessClicker.SaveStorage;

namespace BusinessClicker.Logic
{
    public class BusinessSaveLogic
    {
        private const string MoneySaveKey = "money";
        private const string BusinessesSaveKey = "businesses";

        private readonly ILocalStorage _localStorage;
        
        public BusinessSaveLogic(ILocalStorage storage)
        {
            _localStorage = storage;
        }
        
        public Wallet LoadOrCreateWallet()
        {
            if (_localStorage.TryGetValue(MoneySaveKey, out Wallet wallet))
                return wallet;

            return new Wallet {Money = 0};
        }
        
        public void SaveWallet(Wallet wallet)
        {
            _localStorage.SaveValue(MoneySaveKey, wallet);
        }

        
        [Serializable]
        public class JsonBusinessesArray
        {
            public Business[] array;
        }
        
        public bool TryLoadBusinesses(out Business[] businesses)
        {
            businesses = null;
            
            if (_localStorage.TryGetValue(BusinessesSaveKey, out JsonBusinessesArray jsonArray))
            {
                businesses = jsonArray.array;
                return true;
            }

            return false;
        }

        public void SaveBusinesses(Business[] array)
            => _localStorage.SaveValue(BusinessesSaveKey, new JsonBusinessesArray {array = array});

        public void ClearProgress()
        {
            _localStorage.ClearValue(MoneySaveKey);
            _localStorage.ClearValue(BusinessesSaveKey);
        }
    }
}