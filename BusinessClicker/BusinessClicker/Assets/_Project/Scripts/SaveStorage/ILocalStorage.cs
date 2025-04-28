namespace BusinessClicker.SaveStorage
{
    public interface ILocalStorage
    {
        public bool TryGetValue<T>(string key, out T result);
        public void SaveValue<T>(string key, T value);
        public void ClearValue(string key);
        public void ClearSaves();
        public bool HasSaves();
    }
}