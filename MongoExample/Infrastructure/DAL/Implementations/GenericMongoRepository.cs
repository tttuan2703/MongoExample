using MongoDB.Bson;
using MongoDB.Driver;
using MongoExample.Infrastructure.DAL.Contracts;
using MongoExample.Infrastructure.DAL.Models;
using MongoExample.Infrastructure.Helpers.Contracts;
using System.Linq.Expressions;
using static MongoExample.Infrastructure.Helpers.Common.ReflectionHelper;

namespace MongoExample.Infrastructure.DAL.Implementations
{
    public class GenericMongoRepository<T> : IAsyncRepository<T> where T : class
    {
        #region Members

        protected IMongoDbClient _dbClient;

        #endregion Members

        #region Contructors

        public GenericMongoRepository(IMongoDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        #endregion Contructors

        #region IRepository

        protected IMongoCollection<T> GetDbCollection()
        {
            var db = _dbClient.GetDatabase();
            return db.GetCollection<T>(typeof(T).Name);
        }

        public virtual IEnumerable<T> GetAll()
        {
            var collection = GetDbCollection();
            return collection.Find(_ => true).ToList();
        }

        public virtual T Get(Expression<Func<T, bool>> specification)
        {
            var collection = GetDbCollection();
            return collection.Find(specification).FirstOrDefault();
        }

        public virtual T Get(object id)
        {
            var collection = GetDbCollection();
            return collection.Find(Builders<T>.Filter.Eq("_id", CheckConvertId(id))).FirstOrDefault();
        }

        public virtual long Count(Expression<Func<T, bool>> specification)
        {
            var collection = GetDbCollection();
            return collection.CountDocuments(specification);
        }

        public virtual long Count(FilterDefinition<T> specification)
        {
            var collection = GetDbCollection();
            return collection.CountDocuments(specification);
        }

        public virtual IEnumerable<T> FindDoc(Expression<Func<T, bool>> specification)
        {
            var collection = GetDbCollection();
            return collection.Find(specification).ToList();
        }

        public virtual bool Exists(Expression<Func<T, bool>> specification)
        {
            var count = Count(specification);
            return (count > 0);
        }

        public void Add(T item)
        {
            var collection = GetDbCollection();
            collection.InsertOne(item);
        }

        public void AddRange(IEnumerable<T> item)
        {
            var collection = GetDbCollection();
            collection.InsertMany(item);
        }

        public void Update(T item)
        {
            var collection = GetDbCollection();
            var update = Builders<T>.Update;
            var updates = new List<UpdateDefinition<T>>();

            // get array propertise of type T
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                // Set update for field of type except Id field
                if (prop.Name != "Id")
                {
                    var valueOfProp = typeof(T).GetPropertyValueByName(item, prop.Name);
                    updates.Add(update.Set(prop.Name, valueOfProp));
                }
            }

            var builderId = BuilderIdFilder(typeof(T).GetPropertyValueByName(item, "Id"));
            collection.UpdateOne(builderId, update.Combine(updates));
        }

        public IEnumerable<T> SearchFilterElements(FilterDefinition<T> filter, long pageIndex, long pageSize, IList<SortByInfos> sortBy)
        {
            var collection = GetDbCollection();
            var arraySearch = collection.Find(filter);
            var sortArray = SortByField(arraySearch, sortBy);

            var numSkip = (pageIndex - 1) * pageSize;
            return sortArray.Skip((int)numSkip).Limit((int)pageSize).ToList();
        }

        public IEnumerable<T> SearchFilterElements(Expression<Func<T, bool>> expression, long pageIndex, long pageSize, IList<SortByInfos> sortBy)
        {
            var collection = GetDbCollection();
            var arraySearch = collection.Find(expression);
            var sortArray = SortByField(arraySearch, sortBy);

            var numSkip = (pageIndex - 1) * pageSize;
            return sortArray.Skip((int)numSkip).Limit((int)pageSize).ToList();
        }

        public void DeleteOne(FilterDefinition<T> filter)
        {
            var collection = GetDbCollection();
            collection.DeleteOne(filter);
        }

        public void DeleteMany(FilterDefinition<T> filter)
        {
            var collection = GetDbCollection();
            collection.DeleteMany(filter);
        }

        #endregion IRepository

        #region IMongoRepository

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var collection = GetDbCollection();
            var data = await collection.FindAsync(_ => true);
            return data.ToList();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> specification)
        {
            var collection = GetDbCollection();
            var item = await collection.FindAsync(specification);
            return item.FirstOrDefault();
        }

        public async Task<T> GetAsync(object id)
        {
            var collection = GetDbCollection();
            var item = await collection.FindAsync(Builders<T>.Filter.Eq("_id", CheckConvertId(id)));
            return item.FirstOrDefault();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> specification)
        {
            var collection = GetDbCollection();
            return await collection.CountDocumentsAsync(specification);
        }

        public async Task<long> CountAsync(FilterDefinition<T> specification)
        {
            var collection = GetDbCollection();
            return await collection.CountDocumentsAsync(specification);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> specification)
        {
            var count = await CountAsync(specification);
            return (count > 0);
        }

        public async Task<IEnumerable<T>> FindDocsAsync(Expression<Func<T, bool>> specification)
        {
            var collection = GetDbCollection();
            var array = await collection.FindAsync(specification);
            return array.ToList();
        }

        public async Task<IEnumerable<T>> FindDocsAsync(FilterDefinition<T> specification)
        {
            var collection = GetDbCollection();
            var array = await collection.FindAsync(specification);
            return array.ToList();
        }

        #endregion IMongoRepository

        #region Common

        public static ObjectId CheckConvertId(object id)
        {
            if (ObjectId.TryParse(id.ToString(), out ObjectId objectId))
            {
                return objectId;
            }
            else
            {
                return ObjectId.Empty;
            }
        }

        protected static BsonDocument BuilderIdFilder(object id)
        {
            return new BsonDocument("_id", BsonValue.Create(id));
        }

        protected static IFindFluent<T, T> SortByField(IFindFluent<T, T> query, IList<SortByInfos> sortDocs)
        {
            if (sortDocs != null && sortDocs.Count > 0)
            {
                var final = new List<SortDefinition<T>>();
                foreach (var sort in sortDocs)
                {
                    if (sort.IsAscending)
                    {
                        final.Add(Builders<T>.Sort.Ascending(sort.FieldName));
                    }
                    else
                    {
                        final.Add(Builders<T>.Sort.Descending(sort.FieldName));
                    }
                }
                query = query.Sort(Builders<T>.Sort.Combine(final));
            }
            return query;
        }

        #endregion Common
    }
}
