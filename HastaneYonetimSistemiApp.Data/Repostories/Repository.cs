using HastaneYonetimSistemiApp.Data.Context;
using HastaneYonetimSistemiApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Repostories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly HastaneYonetimSistemiDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(HastaneYonetimSistemiDbContext db)
        {
            _db = db; 
            _dbSet =db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {

            entity.CreatedTime = DateTime.Now;
            _dbSet.Add(entity);
            
            
        }

        public void Delete(TEntity entity, bool softDeleted = true)
        {
            if (softDeleted)
            {
                entity.ModifiedTime = DateTime.Now;
                entity.IsDeleted = true;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity); 

        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbSet : _dbSet.Where(predicate);
        }

        public TEntity GetId(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedTime= DateTime.Now;
            _dbSet.Update(entity);
            
        }
    }
}
