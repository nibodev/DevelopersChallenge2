using DevelopersChallenge2.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Repository.Interfaces
{
    interface IBaseRepository<T> where T: Entity
    {
        Task<IList<T>> Get();
        Task<IList<T>> GetByCondition(Expression<Func<T, bool>> where);
        Task Post(T entity);
        Task Put(T entity);
        Task Delete(int id);
        Task DeleteList(List<T> entities);
    }
}
