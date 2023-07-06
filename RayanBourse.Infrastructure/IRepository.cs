using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayanBourse.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id, string[] includes=null);
        IEnumerable<TEntity> GetAll(string[] includes=null);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Save(TEntity entity);

        void Delete(TEntity entity);
        void Update(TEntity entity);

        public List<TEntity> FindWithIncludes(Expression<Func<TEntity, bool>> predicate, string[] includes);

    }
}
