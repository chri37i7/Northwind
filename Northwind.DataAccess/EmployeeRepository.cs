using Northwind.DataAccess.Entities.Models;

namespace Northwind.DataAccess
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(NorthwindDbContext context) : base(context) { }
        public EmployeeRepository() : base() { }
    }
}