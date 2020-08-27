using Northwind.DataAccess.Entities.Models;

namespace Northwind.DataAccess
{
    public    class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(NorthwindDbContext context) : base(context) { }
        public CustomerRepository() : base() { }
    }
}