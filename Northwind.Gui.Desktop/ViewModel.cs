using Northwind.DataAccess;
using Northwind.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Northwind.Gui.Desktop
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        protected Repository repository;
        protected ObservableCollection<Order> orders;
        protected Order selectedOrder;
        protected OrderDetail selectedOrderDetail;
        protected ObservableCollection<Customer> customers;
        protected Customer selectedCustomer;
        protected ObservableCollection<Employee> employees;
        protected Employee selectedEmployee;
        #endregion

        #region Constructor
        public ViewModel()
        {
        }
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
                }
            }
        }
        #endregion

        #region Initialization Method
        public virtual async Task InitializeAsync()
        {
            // Initialize repository
            repository = new Repository();

            // Initialize ObservableCollections
            Orders = new ObservableCollection<Order>(await repository.GetAllOrdersAsync());
            Customers = new ObservableCollection<Customer>(await repository.GetAllCustomersAsync());
            Employees = new ObservableCollection<Employee>(await repository.GetAllEmployeesAsync());
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}