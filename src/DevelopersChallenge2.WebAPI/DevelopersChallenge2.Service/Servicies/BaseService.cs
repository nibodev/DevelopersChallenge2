using DevelopersChallenge2.Domain;
using DevelopersChallenge2.Repository.Interfaces;
using DevelopersChallenge2.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Service.Servicies
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        private readonly IBaseRepository<T> repository;
        public BaseService(IBaseRepository<T> repository) => this.repository = repository;

        public virtual async Task<IList<T>> Get()
        {
            return await this.repository.Get();
        }

        public virtual async Task<IList<T>> GetByCondition(Expression<Func<T, bool>> where)
        {
            return await this.repository.GetByCondition(where);
        }

        public virtual async Task<T> GetById(int id)
        {
            var result = await this.repository.GetByCondition(t => t.Id == id);
            return result.FirstOrDefault();
        }

        public virtual async Task Post(T entity)
        {
            await this.repository.Post(entity);
        }

        public virtual async Task Put(T entity)
        {
            await this.repository.Put(entity);
        }

        public virtual async Task Delete(int id)
        {
            await this.repository.Delete(id);
        }

        public virtual async Task DeleteList(List<T> entities)
        {
            await this.repository.DeleteList(entities);
        }
    }
}
