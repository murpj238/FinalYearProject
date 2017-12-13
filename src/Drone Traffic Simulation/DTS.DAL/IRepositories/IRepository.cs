using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DTS.DAL.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void AttachToContext(TEntity entity);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity GetSingle<T>(T id);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();
    }
}