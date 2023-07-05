using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
            return _dbContext.Set<TEntity>().Where(predicate);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public void Save(TEntity entity)
        {
            try
            {
                 _dbContext.Set<TEntity>().Add(entity);               
                  _dbContext.SaveChanges();


            }
            catch (Exception e)
            {

                throw e;
            }
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
            IQueryable<TEntity> entityQuery = _dbContext.Set<TEntity>().Where(x=>id==id);
            return entityQuery.FirstOrDefault();

        }


    }
}
