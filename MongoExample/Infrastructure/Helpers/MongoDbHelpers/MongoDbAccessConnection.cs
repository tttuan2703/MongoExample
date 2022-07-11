using MongoExample.Infrastructure.Helpers.Contracts;
using MongoExample.Infrastructure.Helpers.Models;
using MongoExample.Infrastructure.Helpers.Services;

namespace MongoExample.Infrastructure.Helpers.MongoDbHelpers
{
    public class MongoDbAccessConnection : IMongoDbAccessConnection
    {
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public virtual MongoDbConnection? GetConfigurationValue()
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            var configService = DependencyInjectionHelper.ResolveService<ConfigurationApp>();
            var result = configService.GetValueKey<MongoDbConnection>("MongoDb");

            if (string.IsNullOrEmpty(result?.ConnectionStringFormat))
            {
                Console.Write("No connectstring. Can't get value connect to database.");
            }

            return result;
        }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public virtual string? GetDatabaseName()
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        => GetConfigurationValue()?.DatabaseName;

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public virtual string? BuildConnectionString()
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            var cnn = GetConfigurationValue();
            return cnn?.ConnectionStringFormat?
                        .Replace("DatabaseName", cnn.DatabaseName);
        }
    }
}
