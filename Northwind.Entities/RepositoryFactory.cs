using Northwind.DataAccess;
using Northwind.DataAccess.Entities.Models;

namespace Northwind.Entities
{
    public class RepositoryFactory<TRepository, TEntity>
        where TRepository : IBaseRepository<TEntity>, new()
        where TEntity : class
    {
        protected static RepositoryFactory<TRepository, TEntity> instance;
        protected NorthwindDbContext context;

        public RepositoryFactory() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual TRepository Create()
        {
            if(context is null)
            {
                context = new NorthwindDbContext();
            }
            TRepository repo = new TRepository();
            repo.Context = context;
            return repo;
        }

        /// <summary>
        /// Gets the instance of the <see cref="RepositoryFactory{T}"/> if it exitsts, else it creates it.
        /// </summary>
        /// <returns>An instance of <see cref="RepositoryFactory{T}"/></returns>
        public static RepositoryFactory<TRepository, TEntity> GetInstance()
        {
            if(instance is null)
            {
                instance = new RepositoryFactory<TRepository, TEntity>();
            }
            return instance;
        }

        /// <summary>
        /// Disposes the <see cref="context"/> object
        /// </summary>
        public virtual void KillContext()
        {
            context.Dispose();
        }
    }
}