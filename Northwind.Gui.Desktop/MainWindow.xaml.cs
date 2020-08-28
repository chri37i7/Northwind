using Northwind.DataAccess.Entities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                Order order = new Order()
                {
                    CustomerId = viewModel.SelectedCustomer.CustomerId,
                    EmployeeId = viewModel.SelectedEmployee.EmployeeId,
                    OrderDate = datePicker_OrderDate.SelectedDate ?? DateTime.Now,
                    RequiredDate = datePicker_RequiredDate.SelectedDate ?? DateTime.Now,
                    ShippedDate = datePicker_ShippedDate.SelectedDate ?? DateTime.Now,
                    ShipVia = Convert.ToInt32(textBox_ShipVia.Text),
                    Freight = Convert.ToDecimal(textBox_Freight.Text),
                    ShipName = textBox_ShipName.Text,
                    ShipAddress = textBox_ShipAddress.Text,
                    ShipCity = textBox_ShipCity.Text,
                    ShipRegion = textBox_ShipRegion.Text,
                    ShipPostalCode = textBox_ShipPostalCode.Text,
                    ShipCountry = textBox_ShipCountry.Text,
                    OrderDetails = viewModel.SelectedOrder.OrderDetails
                };

                viewModel.SelectedOrder = null;
                viewModel.SelectedOrder = order;
            }
            else
            {
                try
                {
                    List<OrderDetail> orderDetails = new List<OrderDetail>();

                    Order newOrder = new Order()
                    {
                        CustomerId = viewModel.SelectedCustomer.CustomerId,
                        EmployeeId = viewModel.SelectedEmployee.EmployeeId,
                        OrderDate = datePicker_OrderDate.SelectedDate ?? DateTime.Now,
                        RequiredDate = datePicker_RequiredDate.SelectedDate ?? DateTime.Now,
                        ShippedDate = datePicker_ShippedDate.SelectedDate ?? DateTime.Now,
                        ShipVia = Convert.ToInt32(textBox_ShipVia.Text),
                        Freight = Convert.ToDecimal(textBox_Freight.Text),
                        ShipName = textBox_ShipName.Text,
                        ShipAddress = textBox_ShipAddress.Text,
                        ShipCity = textBox_ShipCity.Text,
                        ShipRegion = textBox_ShipRegion.Text,
                        ShipPostalCode = textBox_ShipPostalCode.Text,
                        ShipCountry = textBox_ShipCountry.Text,
                        OrderDetails = orderDetails
                    };

#warning
                    //viewModel.Orders.Add(await repository.InsertOrderAsync(newOrder));
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
                if(viewModel.SelectedOrder.Customer != null && viewModel.SelectedOrder.Employee != null)
                {
                    // Set selected Items to the correct Customer, and Employee found in the SelectedOrder properties
                    comboBox_Customer.SelectedItem = viewModel.Customers.FirstOrDefault(c => c.CustomerId == viewModel.SelectedOrder.CustomerId);
                    comboBox_Employee.SelectedItem = viewModel.Employees.FirstOrDefault(c => c.EmployeeId == viewModel.SelectedOrder.EmployeeId);

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
                        OrderDetail updatedOrderDetail = new OrderDetail()
                        {

                            OrderId = viewModel.SelectedOrder.OrderId,
                            ProductId = Convert.ToInt32(textBox_ProductID.Text),
                            UnitPrice = Convert.ToDecimal(textBox_UnitPrice.Text),
                            Quantity = Convert.ToInt16(textBox_Quantity.Text),
                            Discount = Convert.ToSingle(textBox_Discount.Text)
                        };

                        // Insert into the DB
#warning
                        //await repository.UpdateOrderDetailAsync(updatedOrderDetail);
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
                        OrderDetail newOrderDetail = new OrderDetail()
                        {
                            OrderId = viewModel.SelectedOrder.OrderId,
                            ProductId = Convert.ToInt32(textBox_ProductID.Text),
                            UnitPrice = Convert.ToDecimal(textBox_UnitPrice.Text),
                            Quantity = Convert.ToInt16(textBox_Quantity.Text),
                            Discount = Convert.ToSingle(textBox_Discount.Text)
                        };

                        // Insert into the DB
#warning
                        //await repository.InsertOrderDetailAsync(newOrderDetail);
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
#warning
            //await repository.DeleteOrderDetailAsync(viewModel.SelectedOrderDetail);

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