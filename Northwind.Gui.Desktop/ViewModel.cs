using Northwind.DataAccess;
using Northwind.DataAccess.Entities.Models;
using Northwind.Entities;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Northwind.Gui.Desktop
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        protected ObservableCollection<Order> orders;
        protected Order selectedOrder;
        protected OrderDetail selectedOrderDetail;
        protected ObservableCollection<Customer> customers;
        protected Customer selectedCustomer;
        protected ObservableCollection<Employee> employees;
        protected Employee selectedEmployee;
        #endregion

        OrderRepository orderRepository;
        EmployeeRepository employeeRepository;
        CustomerRepository customerRepository;

        #region Constructor
        public ViewModel() { }
        #endregion

        #region Properties
        public virtual ObservableCollection<Order> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                if(orders != value)
                {
                    orders = value;

                    NotifyPropertyChanged();
                }
            }
        }

        public virtual Order SelectedOrder
        {
            get
            {
                return selectedOrder;
            }
            set
            {
                if(selectedOrder != value)
                {
                    selectedOrder = value;

                    NotifyPropertyChanged();

                    orderRepository.Update();
                }
            }
        }

        public virtual OrderDetail SelectedOrderDetail
        {
            get
            {
                return selectedOrderDetail;
            }
            set
            {
                if(selectedOrderDetail != value)
                {
                    selectedOrderDetail = value;

                    NotifyPropertyChanged();
                }
            }
        }

        public virtual ObservableCollection<Customer> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                if(customers != value)
                {
                    customers = value;

                    NotifyPropertyChanged();
                }
            }
        }

        public virtual Customer SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                if(selectedCustomer != value)
                {
                    selectedCustomer = value;

                    NotifyPropertyChanged();

                    customerRepository.Update();
                }
            }
        }

        public virtual ObservableCollection<Employee> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                if(employees != value)
                {
                    employees = value;

                    NotifyPropertyChanged();
                }
            }
        }

        public virtual Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                if(selectedEmployee != value)
                {
                    selectedEmployee = value;

                    NotifyPropertyChanged();

                    employeeRepository.Update();
                }
            }
        }
        #endregion

        #region Initialization Method
        public virtual async Task InitializeAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    // Orders
                    RepositoryFactory<OrderRepository, Order> orderFactory = RepositoryFactory<OrderRepository, Order>.GetInstance();
                    orderRepository = orderFactory.Create();
                    IEnumerable<Order> orders = orderRepository.GetAll();



                    // Employees
                    RepositoryFactory<EmployeeRepository, Employee> employeeFactory = RepositoryFactory<EmployeeRepository, Employee>.GetInstance();
                    employeeRepository = employeeFactory.Create();
                    IEnumerable<Employee> employees = employeeRepository.GetAll();

                    // Customers
                    RepositoryFactory<CustomerRepository, Customer> customerFactory = RepositoryFactory<CustomerRepository, Customer>.GetInstance();
                    customerRepository = customerFactory.Create();
                    IEnumerable<Customer> customers = customerRepository.GetAll();

                    // Initialize ObservableCollections
                    Orders = new ObservableCollection<Order>(orders);
                    Customers = new ObservableCollection<Customer>(customers);
                    Employees = new ObservableCollection<Employee>(employees);
                });
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}