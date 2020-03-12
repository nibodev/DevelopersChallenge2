using DevelopersChallenge2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevelopersChallenge2.Domain;
using System.Linq;

namespace DevelopersChallenge2.Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly DevelopersChallenge2Context context;
        public BaseRepository(DevelopersChallenge2Context context)
        {
            this.context = context;
        }

        public async Task<IList<T>> Get()
        {
            return this.context.Set<T>().ToList();
        }

        public async Task<IList<T>> GetByCondition(Expression<Func<T, bool>> where)
        {
            return this.context.Set<T>().Where(where).ToList();
        }

        public async Task Post(T entity)
        {
            this.context.Set<T>().Add(entity);
            this.context.SaveChanges();
        }

        public async Task Put(T entity)
        {
            this.context.Set<T>().Update(entity);
            this.context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            if (this.context.Set<T>().Find(id) != null)
            {
                T entity = this.context.Set<T>().Find(id);
                this.context.Set<T>().Remove(entity);
                this.context.SaveChanges();
            }
        }

        public async Task DeleteList(List<T> entities)
        {
            if (entities.Count > 0)
            {
                this.context.Set<T>().RemoveRange(entities);
                this.context.SaveChanges();
            }
        }
    }
}
