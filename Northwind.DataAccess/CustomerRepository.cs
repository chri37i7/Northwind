using Northwind.DataAccess.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DataAccess
{
public    class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(NorthwindDbContext context) : base(context) { }
        public CustomerRepository() : base() { }
    }
}
