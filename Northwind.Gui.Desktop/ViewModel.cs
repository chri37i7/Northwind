using Northwind.DataAccess;
using Northwind.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            List<Customer> customers = repository.GetAllCustomers();
            List<Employee> employees = repository.GetAllEmployees();

            // Initialize ObServableCollection
            Orders = new ObservableCollection<Order>(orders);
            Customers = new ObservableCollection<Customer>(customers);
            Employees = new ObservableCollection<Employee>(employees);
        }

        // ViewModel Properties
        public virtual ObservableCollection<Order> Orders { get; set; }
        public virtual Order SelectedOrder { get; set; }
        public virtual OrderDetail SelectedOrderDetail { get; set; }
        public virtual ObservableCollection<Customer> Customers { get; set; }
        public virtual Customer SelectedCustomer { get; set; }
        public virtual ObservableCollection<Employee> Employees { get; set; }
        public virtual Employee SelectedEmployee { get; set; }
    }
}