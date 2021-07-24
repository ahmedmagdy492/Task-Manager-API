using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Task_Manager.Models;

namespace Task_Manager.Repository
{
    public interface IRepository<TModel, TID> where TModel : BaseModel<TID>
    {
        void Add(TModel model);
        void Edit(TModel model);
        IEnumerable<TModel> GetAll(Expression<Func<TModel, bool>> filter = null, string include = null);
        TModel GetOne(Expression<Func<TModel, bool>> filter, string includeProp = null);
        void Remove(TModel model);
    }
}