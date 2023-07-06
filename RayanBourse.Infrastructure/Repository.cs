using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RayanBourse.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();

        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).AsNoTracking();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public async Task<TEntity> Save(TEntity entity)
        {
            try
            {
                 _dbContext.Set<TEntity>().Add(entity);               
                 await _dbContext.SaveChangesAsync();


            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }



        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();

        }





        public IEnumerable<TEntity> GetAll(string[] includes=null)
        {
            IQueryable<TEntity> entityQuery = _dbContext.Set<TEntity>();
            return entityQuery.ToList();

        }

        public TEntity Get(int id,string[] includes=null)
        {
            IQueryable<TEntity> entityQuery = _dbContext.Set<TEntity>().Where(x=>id==id).AsNoTracking();
            return entityQuery.FirstOrDefault();

        }

        public List<TEntity> FindWithIncludes(Expression<Func<TEntity, bool>> predicate, string[] includes)
        {

            IQueryable<TEntity> entityQuery = _dbContext.Set<TEntity>().Where(predicate);

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return entityQuery.ToList();
        }
    }
}
