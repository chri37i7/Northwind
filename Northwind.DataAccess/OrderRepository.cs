using Microsoft.EntityFrameworkCore;
using Northwind.DataAccess.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.DataAccess
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(NorthwindDbContext context) : base(context) { }
        public OrderRepository() : base() { }

        public new Order GetBy(int id)
        {
            return context.Orders.Where(o => o.OrderId == id)
                .Include("Customer")
                .Include("OrderDetails")
                .Include("Products")
                .FirstOrDefault();
        }

        public new IEnumerable<Order> GetAll()
        {
            return context.Set<Order>()
                .Include("Customer")
                .Include("OrderDetails");
        }
    }
}