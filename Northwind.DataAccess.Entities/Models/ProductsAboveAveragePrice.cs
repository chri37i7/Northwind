using System;
using System.Collections.Generic;

#nullable disable

namespace Northwind.DataAccess.Entities.Models
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
