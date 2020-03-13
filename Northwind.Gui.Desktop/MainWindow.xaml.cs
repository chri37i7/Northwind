using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Northwind.DataAccess;
using Northwind.Entities;

namespace Northwind.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        #region Master Events

        private void Button_SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if(button_SaveOrder.IsEnabled == true)
            {
                if(datePicker_OrderDate.SelectedDate == null || datePicker_RequiredDate.SelectedDate == null)
                {
                    MessageBox.Show("Please set all dates", "All Dates Not Set", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(comboBox_Customer.SelectedItem == null)
                {
                    MessageBox.Show("Please select the customer", "No Customer Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(comboBox_Employee == null)
                {
                    MessageBox.Show("Please select the employee", "No Employee Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(textBox_Freight.Text == string.Empty || textBox_ShipAddress.Text == string.Empty || textBox_ShipCity.Text == string.Empty || textBox_ShipCountry.Text == string.Empty || textBox_ShipName.Text == string.Empty || textBox_ShipPostalCode.Text == string.Empty || textBox_ShipRegion.Text == string.Empty || textBox_ShipVia.Text == string.Empty)
                {
                    MessageBox.Show("Please fill out all boxes", "All Boxes Not Filled", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    List<OrderDetail> orderDetails = new List<OrderDetail>();

                    Order newOrder = new Order(
                        viewModel.SelectedCustomer.CustomerID,
                        viewModel.SelectedEmployee.EmployeeID,
                        datePicker_OrderDate.SelectedDate ?? DateTime.Now,
                        datePicker_RequiredDate.SelectedDate ?? DateTime.Now,
                        datePicker_ShippedDate.SelectedDate ?? DateTime.Now,
                        Convert.ToInt32(textBox_ShipVia.Text),
                        Convert.ToDecimal(textBox_Freight.Text),
                        textBox_ShipName.Text,
                        textBox_ShipAddress.Text,
                        textBox_ShipCity.Text,
                        textBox_ShipRegion.Text,
                        textBox_ShipPostalCode.Text,
                        textBox_ShipCountry.Text,
                        orderDetails);

                    if(!viewModel.Orders.Contains(newOrder))
                    {
                        viewModel.Orders.Add(newOrder);
                        viewModel.SelectedOrder = newOrder; 
                    }
                }
            }
        }

        private void Button_EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedOrder != null)
            {
                if(datePicker_OrderDate.IsEnabled == false)
                {
                    // Combo boxes
                    comboBox_Customer.IsEnabled = true;
                    comboBox_Employee.IsEnabled = true;

                    // Order Date DatePickers
                    datePicker_OrderDate.IsEnabled = true;
                    datePicker_RequiredDate.IsEnabled = true;
                    datePicker_ShippedDate.IsEnabled = true;

                    // Shipping Detail TextBoxes
                    textBox_Freight.IsReadOnly = false;
                    textBox_ShipAddress.IsReadOnly = false;
                    textBox_ShipCity.IsReadOnly = false;
                    textBox_ShipCountry.IsReadOnly = false;
                    textBox_ShipName.IsReadOnly = false;
                    textBox_ShipPostalCode.IsReadOnly = false;
                    textBox_ShipRegion.IsReadOnly = false;
                    textBox_ShipVia.IsReadOnly = false;

                    // Details
                    button_SaveOrder.IsEnabled = true;
                    button_NewOrder.IsEnabled = false;
                    button_EditOrder.IsEnabled = false;
                    button_NewOrderDetail.IsEnabled = true;
                }
                if(viewModel.SelectedOrderDetail != null)
                {
                    button_EditOrderDetail.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please select an order", "No Order Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_NewOrder_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel != null)
            {
                // Set selected to null
                listView_Orders.SelectedItem = null;
                listView_OrderDetails.SelectedItem = null;

                // Order Date DatePickers
                datePicker_OrderDate.DisplayDate = DateTime.Now;
                datePicker_OrderDate.IsEnabled = true;
                datePicker_RequiredDate.IsEnabled = true;
                datePicker_RequiredDate.DisplayDate = DateTime.Now;
                datePicker_ShippedDate.IsEnabled = true;
                datePicker_ShippedDate.DisplayDate = DateTime.Now;

                // Combo boxes
                comboBox_Customer.IsEnabled = true;
                comboBox_Customer.SelectedItem = null;
                comboBox_Employee.IsEnabled = true;
                comboBox_Employee.SelectedItem = null;

                // Shipping Detail TextBoxes
                textBox_Freight.IsReadOnly = false;
                textBox_ShipAddress.IsReadOnly = false;
                textBox_ShipCity.IsReadOnly = false;
                textBox_ShipCountry.IsReadOnly = false;
                textBox_ShipName.IsReadOnly = false;
                textBox_ShipPostalCode.IsReadOnly = false;
                textBox_ShipRegion.IsReadOnly = false;
                textBox_ShipVia.IsReadOnly = false;

                // Order Detail TextBoxes
                textBox_ProductID.IsReadOnly = true;
                textBox_UnitPrice.IsReadOnly = true;
                textBox_Quantity.IsReadOnly = true;
                textBox_Discount.IsReadOnly = true;

                // Buttons
                button_NewOrder.IsEnabled = false;
                button_EditOrder.IsEnabled = false;
                button_SaveOrder.IsEnabled = true;
            }
        }

        private void ListView_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the SelectedOrder is not null, disable the DatePickers, and TextBoxes
            if(viewModel.SelectedOrder != null)
            {
                // Set selected Items to the correct Customer, and Employee found in the SelectedOrder properties
                comboBox_Customer.SelectedItem = viewModel.Customers.Where(c => c.CustomerID == viewModel.SelectedOrder.CustomerID).ToList()[0];
                comboBox_Employee.SelectedItem = viewModel.Employees.Where(c => c.EmployeeID == viewModel.SelectedOrder.EmployeeID).ToList()[0];

                // Order Date DatePickers
                datePicker_OrderDate.IsEnabled = false;
                datePicker_RequiredDate.IsEnabled = false;
                datePicker_ShippedDate.IsEnabled = false;

                // Shipping Detail TextBoxes
                textBox_Freight.IsReadOnly = true;
                textBox_ShipAddress.IsReadOnly = true;
                textBox_ShipCity.IsReadOnly = true;
                textBox_ShipCountry.IsReadOnly = true;
                textBox_ShipName.IsReadOnly = true;
                textBox_ShipPostalCode.IsReadOnly = true;
                textBox_ShipRegion.IsReadOnly = true;
                textBox_ShipVia.IsReadOnly = true;

                // Order Detail TextBoxes
                textBox_ProductID.IsReadOnly = true;
                textBox_UnitPrice.IsReadOnly = true;
                textBox_Quantity.IsReadOnly = true;
                textBox_Discount.IsReadOnly = true;

                // ComboBoxes
                comboBox_Customer.IsEnabled = false;
                comboBox_Employee.IsEnabled = false;

                // Buttons
                button_EditOrder.IsEnabled = true;
                button_NewOrder.IsEnabled = true;
                button_SaveOrder.IsEnabled = false;
                button_NewOrderDetail.IsEnabled = false;
            }
            else if(viewModel.SelectedOrder == null)
            {
                // Deselect anything in the comboboxes and disable
                comboBox_Customer.SelectedItem = null;
                comboBox_Employee.SelectedItem = null;
                comboBox_Customer.IsEnabled = false;
                comboBox_Employee.IsEnabled = false;

                // Buttons
                button_EditOrder.IsEnabled = false;
                button_SaveOrder.IsEnabled = false;
                button_NewOrder.IsEnabled = true;
                button_SaveOrderDetail.IsEnabled = false;
                button_DeleteOrderDetail.IsEnabled = false;
                button_NewOrderDetail.IsEnabled = false;
                button_EditOrderDetail.IsEnabled = false;
            }
        }

        #endregion

        #region Detail Events
        private void Button_EditOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedOrderDetail != null)
            {
                if(textBox_ProductID.IsReadOnly == true)
                {
                    // Order Detail TextBoxes
                    textBox_ProductID.IsReadOnly = false;
                    textBox_UnitPrice.IsReadOnly = false;
                    textBox_Quantity.IsReadOnly = false;
                    textBox_Discount.IsReadOnly = false;

                    // Buttons
                    button_SaveOrderDetail.IsEnabled = true;
                    button_DeleteOrderDetail.IsEnabled = true;
                    button_NewOrderDetail.IsEnabled = false;
                    button_EditOrderDetail.IsEnabled = false;
                }
            }
            else
            {
                if(viewModel.SelectedOrder != null)
                {
                    MessageBox.Show("Please select an order detail", "No Order Detail Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please select an order", "No Order Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Button_SaveOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if(textBox_ProductID.Text == string.Empty || textBox_UnitPrice.Text == string.Empty || textBox_Quantity.Text == string.Empty || textBox_Discount.Text == string.Empty)
            {
                MessageBox.Show("Please fill out all order details", "All Details Not Filled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if(viewModel.SelectedOrder != null)
                {
                    OrderDetail orderDetail = new OrderDetail(viewModel.SelectedOrder.OrderID, Convert.ToInt32(textBox_ProductID.Text), Convert.ToDecimal(textBox_UnitPrice.Text), Convert.ToSByte(textBox_Quantity.Text), Convert.ToUInt64(textBox_Discount.Text));

                    viewModel.SelectedOrder.OrderDetails.Add(orderDetail);
                }
                else
                {
                    MessageBox.Show("Please select an order, to add an order detail to.", "Cannot Add Order Detail", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_NewOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedOrder != null)
            {
                // Deselect the SelectedItem
                listView_OrderDetails.SelectedItem = null; ;

                // Enable TextBoxes
                textBox_ProductID.IsReadOnly = false;
                textBox_UnitPrice.IsReadOnly = false;
                textBox_Quantity.IsReadOnly = false;
                textBox_Discount.IsReadOnly = false;

                // Buttons
                button_SaveOrderDetail.IsEnabled = true;
                button_NewOrderDetail.IsEnabled = false;
            }
        }

        private void Button_DeleteOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            // Repository object for quering
            Repository repository = new Repository();

            // SQL Query
            string query = $"DELETE FROM [Order Details] WHERE ProductID = {viewModel.SelectedOrderDetail.ProductID}";

            // Execute query
            repository.Execute(query);

            // Remove the OrderDetail from the ViewModel
            viewModel.SelectedOrder.OrderDetails.RemoveAll(c => c.ProductID == viewModel.SelectedOrderDetail.ProductID);
        }

        private void ListView_OrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(viewModel.SelectedOrder != null)
            {
                if(button_SaveOrder.IsEnabled == true)
                {
                    button_EditOrderDetail.IsEnabled = true;
                    button_NewOrderDetail.IsEnabled = true;
                }
                if(button_SaveOrder.IsEnabled == false)
                {
                    button_EditOrderDetail.IsEnabled = false;
                    button_NewOrderDetail.IsEnabled = false;
                    button_SaveOrderDetail.IsEnabled = false;
                    button_DeleteOrderDetail.IsEnabled = false;
                }
                if(viewModel.SelectedOrderDetail == null)
                {
                    button_EditOrderDetail.IsEnabled = false;
                    button_SaveOrderDetail.IsEnabled = false;
                    button_DeleteOrderDetail.IsEnabled = false;
                }
                if(viewModel.SelectedOrderDetail != null)
                {
                    button_SaveOrderDetail.IsEnabled = false;
                    button_DeleteOrderDetail.IsEnabled = false;
                }
            }

            // Order Detail TextBoxes
            textBox_ProductID.IsReadOnly = true;
            textBox_UnitPrice.IsReadOnly = true;
            textBox_Quantity.IsReadOnly = true;
            textBox_Discount.IsReadOnly = true;

            // make sure there is at least one item
            if(e.AddedItems.Count > 0)
            {
                // cast object
                OrderDetail firstItem = e.AddedItems[0] as OrderDetail;

                // if not null
                if(firstItem != null)
                {
                    // Create object
                    OrderDetail orderDetail = new OrderDetail(firstItem.OrderID, firstItem.ProductID, firstItem.UnitPrice, firstItem.Quantity, firstItem.Discount);

                    // Set SelectedOrderDetail
                    viewModel.SelectedOrderDetail = orderDetail;
                }
            }
        }
        #endregion
    }
}