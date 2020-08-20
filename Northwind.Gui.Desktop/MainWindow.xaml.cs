using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private Repository repository;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnLoadedAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                // Initialize viewModel
                viewModel = new ViewModel();

                // Assign viewModel to DataContext
                DataContext = viewModel;

                // Initialize viewModel Observeable Collections
                await viewModel.InitializeAsync();

                // Initialize repository
                repository = new Repository();

                // Run InitializeAsync to test connection to DB
                await repository.InitializeAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Master Events

        private async void Button_SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedOrder != null)
            {
                List<OrderDetail> orderDetails = new List<OrderDetail>();

                Order updatedOrder = new Order(
                    viewModel.SelectedOrder.OrderID,
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

                // Remove the old order
                viewModel.Orders.Remove(viewModel.SelectedOrder);
                // Add the new order
                viewModel.Orders.Add(updatedOrder);
                // Select the new order
                listView_Orders.SelectedItem = updatedOrder;
                // Update the order in the DB
                await repository.UpdateOrderAsync(updatedOrder);
            }
            else
            {
                try
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

                    viewModel.Orders.Add(await repository.InsertOrderAsync(newOrder));
                    listView_Orders.SelectedItem = newOrder;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error occured", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_EditOrder_Click(object sender, RoutedEventArgs e)
        {
            // Enable
            button_SaveOrder.IsEnabled = true;
            comboBox_Customer.IsEnabled = true;
            comboBox_Employee.IsEnabled = true;
            datePicker_OrderDate.IsEnabled = true;
            datePicker_RequiredDate.IsEnabled = true;
            datePicker_ShippedDate.IsEnabled = true;
            textBox_ShipName.IsReadOnly = false;
            textBox_ShipAddress.IsReadOnly = false;
            textBox_ShipCity.IsReadOnly = false;
            textBox_ShipRegion.IsReadOnly = false;
            textBox_ShipPostalCode.IsReadOnly = false;
            textBox_ShipCountry.IsReadOnly = false;
            textBox_ShipVia.IsReadOnly = false;
            textBox_Freight.IsReadOnly = false;

            // Disable
            button_NewOrder.IsEnabled = false;
            button_EditOrder.IsEnabled = false;
        }

        private void Button_NewOrder_Click(object sender, RoutedEventArgs e)
        {
            // Reset SelectedItem
            listView_Orders.SelectedItem = null;

            // Enable
            button_SaveOrder.IsEnabled = true;
            comboBox_Customer.IsEnabled = true;
            comboBox_Employee.IsEnabled = true;
            datePicker_OrderDate.IsEnabled = true;
            datePicker_RequiredDate.IsEnabled = true;
            datePicker_ShippedDate.IsEnabled = true;
            textBox_ShipName.IsReadOnly = false;
            textBox_ShipAddress.IsReadOnly = false;
            textBox_ShipCity.IsReadOnly = false;
            textBox_ShipRegion.IsReadOnly = false;
            textBox_ShipPostalCode.IsReadOnly = false;
            textBox_ShipCountry.IsReadOnly = false;
            textBox_ShipVia.IsReadOnly = false;
            textBox_Freight.IsReadOnly = false;

            // Disable
            button_NewOrder.IsEnabled = false;
        }

        private void ListView_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable
            button_NewOrder.IsEnabled = true;

            // Disable
            button_SaveOrder.IsEnabled = false;
            comboBox_Customer.IsEnabled = false;
            comboBox_Employee.IsEnabled = false;
            datePicker_OrderDate.IsEnabled = false;
            datePicker_RequiredDate.IsEnabled = false;
            datePicker_ShippedDate.IsEnabled = false;
            textBox_ShipName.IsReadOnly = true;
            textBox_ShipAddress.IsReadOnly = true;
            textBox_ShipCity.IsReadOnly = true;
            textBox_ShipRegion.IsReadOnly = true;
            textBox_ShipPostalCode.IsReadOnly = true;
            textBox_ShipCountry.IsReadOnly = true;
            textBox_ShipVia.IsReadOnly = true;
            textBox_Freight.IsReadOnly = true;

            // If the SelectedOrder is not null, disable the DatePickers, and TextBoxes
            if(viewModel.SelectedOrder != null)
            {
                // Set selected Items to the correct Customer, and Employee found in the SelectedOrder properties
                comboBox_Customer.SelectedItem = viewModel.Customers.Where(c => c.CustomerID == viewModel.SelectedOrder.CustomerID).ToList()[0];
                comboBox_Employee.SelectedItem = viewModel.Employees.Where(c => c.EmployeeID == viewModel.SelectedOrder.EmployeeID).ToList()[0];

                // Enable
                button_EditOrder.IsEnabled = true;
                button_NewOrderDetail.IsEnabled = true;
            }
            else
            {
                // Reset 
                comboBox_Customer.SelectedItem = null;
                comboBox_Employee.SelectedItem = null;

                // Disable
                button_EditOrder.IsEnabled = false;
                button_NewOrderDetail.IsEnabled = false;
            }
        }

        #endregion

        #region Detail Events
        private void Button_EditOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            // Enable
            button_SaveOrderDetail.IsEnabled = true;
            button_DeleteOrderDetail.IsEnabled = true;
            textBox_ProductID.IsReadOnly = false;
            textBox_UnitPrice.IsReadOnly = false;
            textBox_Quantity.IsReadOnly = false;
            textBox_Discount.IsReadOnly = false;

            // Disable
            button_NewOrderDetail.IsEnabled = false;
            button_EditOrderDetail.IsEnabled = false;
        }

        private async void Button_SaveOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedOrder != null)
            {
                if(viewModel.SelectedOrderDetail != null)
                {
                    // Update
                    try
                    {
                        // Create object
                        OrderDetail updatedOrderDetail = new OrderDetail(
                            viewModel.SelectedOrder.OrderID,
                            Convert.ToInt32(textBox_ProductID.Text),
                            Convert.ToDecimal(textBox_UnitPrice.Text),
                            Convert.ToInt16(textBox_Quantity.Text),
                            Convert.ToSingle(textBox_Discount.Text));

                        // Insert into the DB
                        await repository.UpdateOrderDetailAsync(updatedOrderDetail);
                        // Remove old data from ViewModel
                        viewModel.SelectedOrder.OrderDetails.Remove(viewModel.SelectedOrderDetail);
                        // Add new data to viewModel
                        viewModel.SelectedOrder.OrderDetails.Add(updatedOrderDetail);
                        // Set as selected
                        listView_OrderDetails.SelectedItem = updatedOrderDetail;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "An error occured", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // New OrderDetail
                    try
                    {
                        // Create object
                        OrderDetail newOrderDetail = new OrderDetail(
                            viewModel.SelectedOrder.OrderID,
                            Convert.ToInt32(textBox_ProductID.Text),
                            Convert.ToDecimal(textBox_UnitPrice.Text),
                            Convert.ToInt16(textBox_Quantity.Text),
                            Convert.ToSingle(textBox_Discount.Text));

                        // Insert into the DB
                        await repository.InsertOrderDetailAsync(newOrderDetail);
                        // Add to the ViewModel
                        viewModel.SelectedOrder.OrderDetails.Add(newOrderDetail);
                        // Set as selected
                        listView_OrderDetails.SelectedItem = newOrderDetail;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "An error occured", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order first.", "No Order Selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_NewOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            // Reset SelectedItem
            listView_OrderDetails.SelectedItem = null;

            // Enable
            button_SaveOrderDetail.IsEnabled = true;
            textBox_ProductID.IsReadOnly = false;
            textBox_UnitPrice.IsReadOnly = false;
            textBox_Quantity.IsReadOnly = false;
            textBox_Discount.IsReadOnly = false;

            // Disable
            button_NewOrderDetail.IsEnabled = false;
            button_EditOrderDetail.IsEnabled = false;
        }

        private async void Button_DeleteOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            // Delete the order detail
            await repository.DeleteOrderDetailAsync(viewModel.SelectedOrderDetail);

            // Remove the OrderDetail from the ViewModel
            viewModel.SelectedOrder.OrderDetails.Remove(viewModel.SelectedOrderDetail);
        }

        private void ListView_OrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable
            button_NewOrderDetail.IsEnabled = true;

            // Disable
            button_SaveOrderDetail.IsEnabled = false;
            button_DeleteOrderDetail.IsEnabled = false;
            textBox_ProductID.IsReadOnly = true;
            textBox_UnitPrice.IsReadOnly = true;
            textBox_Quantity.IsReadOnly = true;
            textBox_Discount.IsReadOnly = true;

            if(viewModel.SelectedOrderDetail != null)
            {
                // Enable
                button_EditOrderDetail.IsEnabled = true;
            }
            else
            {
                // Disable
                button_EditOrderDetail.IsEnabled = false;
            }
        }
        #endregion
    }
}