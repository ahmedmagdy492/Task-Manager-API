using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Task_Manager.Data;
using Task_Manager.Models;

namespace Task_Manager.Repository
{
    public class Repository<TModel, TID> : IRepository<TModel, TID> where TModel : BaseModel<TID>
    {
        private readonly AppDBContext _context;
        private readonly DbSet<TModel> _dbSet;

        public Repository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
        }

        public IEnumerable<TModel> GetAll(Expression<Func<TModel, bool>> filter = null, string include = null)
        {
            IQueryable<TModel> dbSet = null;
            if (include != null)
            {
                dbSet = _dbSet.Include(include);
            }

            if (filter != null)
            {
                if(dbSet != null)
                    return dbSet.Where(filter).ToList();
                else
                    return _dbSet.Where(filter).ToList();
            }

            return _dbSet.ToList();
        }

        public TModel GetOne(Expression<Func<TModel, bool>> filter, string includeProp = null)
        {
            if(includeProp != null)
                return _dbSet.Include(includeProp).Where(filter).FirstOrDefault();
            return _dbSet.Where(filter).FirstOrDefault();
        }

        public void Add(TModel model)
        {
            _dbSet.Add(model);
        }

        public void Edit(TModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public void Remove(TModel model)
        {
            _dbSet.Remove(model);
        }
    }
}
