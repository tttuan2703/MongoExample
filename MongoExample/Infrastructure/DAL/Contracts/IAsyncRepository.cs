using MongoDB.Driver;
using MongoExample.Infrastructure.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoExample.Infrastructure.DAL.Contracts
{
    public interface IAsyncRepository<T> where T : class
    {
        #region Common


        IEnumerable<T> GetAll();

        T Get(Expression<Func<T, bool>> specification);

        T Get(object id);

        long Count(Expression<Func<T, bool>> specification);

        long Count(FilterDefinition<T> specification);

        IEnumerable<T> FindDoc(Expression<Func<T, bool>> specification);

        bool Exists(Expression<Func<T, bool>> specification);

        /// <summary>Adds item into the repository.</summary>
        /// <param name="item">The item.</param>
        void Add(T item);

        void AddRange(IEnumerable<T> item);

        public void Update(T item);

        public void DeleteOne(FilterDefinition<T> filter);

        public void DeleteMany(FilterDefinition<T> filter);

        IEnumerable<T> SearchFilterElements(FilterDefinition<T> filter, long pageIndex, long pageSize, IList<SortByInfos> sortBy);

        IEnumerable<T> SearchFilterElements(Expression<Func<T, bool>> expression, long pageIndex, long pageSize, IList<SortByInfos> sortBy);

        #endregion

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> specification);

        Task<T> GetAsync(object id);

        Task<long> CountAsync(Expression<Func<T, bool>> specification);

        Task<long> CountAsync(FilterDefinition<T> specification);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> specification);

        Task<IEnumerable<T>> FindDocsAsync(Expression<Func<T, bool>> specification);

        Task<IEnumerable<T>> FindDocsAsync(FilterDefinition<T> specification);

    }
}
