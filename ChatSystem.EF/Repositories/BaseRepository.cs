using ChatSystem.Core.Interfaces;
using ChatSystem.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContxt contxt;

        public BaseRepository(ApplicationDbContxt contxt)
        {
            this.contxt = contxt;
        }
        public T Add(T entity)
        {
            contxt.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            contxt.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return contxt.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return contxt.Set<T>().Find(id);
        }

        public T Update(T entity)
        {
            contxt.Update(entity);
            return entity;
        }
    }
}
