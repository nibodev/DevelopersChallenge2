using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<IList<T>> Get();
        Task<IList<T>> GetByCondition(Expression<Func<T, bool>> where);
        Task<T> GetById(int id);
        Task Post(T entity);
        Task Put(T entity);
        Task Delete(int id);
        Task DeleteList(List<T> entities);
    }
}
