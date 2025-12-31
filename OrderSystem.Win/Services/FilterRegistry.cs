namespace OrderSystem.Win.Services
{
    public class FilterRegistry
    {
        private readonly Dictionary<(Type, string), object> map = [];

        public void Register<T>(string key, Func<T, bool> filter)
        {
            map[(typeof(T), key)] = filter;
        }

        public object? Resolve(Type entityType, string key)
        {
            return map.TryGetValue((entityType, key), out object? function) ? function : null;
        }
    }
}