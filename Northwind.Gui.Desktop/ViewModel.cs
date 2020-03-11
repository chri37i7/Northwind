using Northwind.DataAccess;
using Northwind.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Northwind.Gui.Desktop
{
    public class ViewModel
    {
        // Fields
        private readonly Repository repository;

        // Constructor
        public ViewModel()
        {
            // Initialize repository
            repository = new Repository();

            // Get all orders from the database
            List<Order> orders = repository.GetAllOrders();

            // Initialize ObServableCollection
            Orders = new ObservableCollection<Order>(orders);
        }

        // ViewModel Properties
        public ObservableCollection<Order> Orders { get; set; }
        public Order SelectedOrder { get; set; }
    }
}