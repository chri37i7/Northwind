using Northwind.DataAccess.Entities.Models;
using System.Collections.Generic;

namespace Northwind.DataAccess
{
    #warning Gør lort Async
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected NorthwindDbContext context;

        public BaseRepository(NorthwindDbContext context)
        {
            Context = context;
        }

        public BaseRepository() { }

        public virtual NorthwindDbContext Context
        {
            get { return context; }
            set { context = value; }
        }

        public void Add(T t)
        {
            context.Set<T>().Add(t);
            context.SaveChanges();
        }

        public void Delete(T t)
        {
            context.Set<T>().Remove(t);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public virtual T GetBy(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}