using MongoDB.Driver;

namespace MongoExample.Infrastructure.Helpers.Contracts
{
    public interface IMongoDbClient
    {
        public IMongoDatabase GetDatabase();
    }
}
