using MongoExample.Infrastructure.Helpers.Models;

namespace MongoExample.Infrastructure.Helpers.Contracts
{
    public interface IMongoDbAccessConnection
    {
        public MongoDbConnection GetConfigurationValue();

        public string GetDatabaseName();

        public string BuildConnectionString();
    }
}
