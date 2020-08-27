using Northwind.DataAccess.Entities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.DataAccess
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(NorthwindDbContext context) : base(context) { }
        public EmployeeRepository() : base() { }
    }
}
