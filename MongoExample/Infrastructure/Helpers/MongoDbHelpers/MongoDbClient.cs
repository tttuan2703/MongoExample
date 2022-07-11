using MongoDB.Driver;
using MongoExample.Infrastructure.Helpers.Contracts;

namespace MongoExample.Infrastructure.Helpers.MongoDbHelpers
{
    public class MongoDbClient : IMongoDbClient
    {
        protected readonly IMongoDbAccessConnection _dbAccessUtility;
        protected MongoClient _dbClient;

        public MongoDbClient(IMongoDbAccessConnection dbAccessUtility)
        {
            _dbAccessUtility = dbAccessUtility;
            _dbClient = new MongoClient(_dbAccessUtility.BuildConnectionString());
        }

        public virtual IMongoDatabase GetDatabase()
        {
            return _dbClient.GetDatabase(_dbAccessUtility.GetDatabaseName());
        }
    }
}
