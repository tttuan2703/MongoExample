namespace MongoExample.Infrastructure.Helpers.Contracts
{
    public interface IConfigurationApp
    {
        public string GetValueKey(string key);

        public T? GetValueKey<T>(string key) where T : class;
    }
}
