using Northwind.DataAccess.Entities.Models;
using System.Collections.Generic;

namespace Northwind.DataAccess
{
    public interface IBaseRepository<T>
    {
        NorthwindDbContext Context { get; set; }
        IEnumerable<T> GetAll();
        T GetBy(int id);
        void Update(T t);
        void Add(T t);
        void Delete(T t);
    }
}