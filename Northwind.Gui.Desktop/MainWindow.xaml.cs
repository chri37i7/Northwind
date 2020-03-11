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

        #region Button Events
        private void Button_EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.SelectedOrder != null)
            {
                if(datePicker_OrderDate.IsEnabled == false)
                {
                    // DatePickers
                    datePicker_OrderDate.IsEnabled = true;
                    datePicker_RequiredDate.IsEnabled = true;
                    datePicker_ShippedDate.IsEnabled = true;

                    // TextBoxes
                    textBox_Freight.IsReadOnly = false;
                    textBox_ShipAddress.IsReadOnly = false;
                    textBox_ShipCity.IsReadOnly = false;
                    textBox_ShipCountry.IsReadOnly = false;
                    textBox_ShipName.IsReadOnly = false;
                    textBox_ShipPostalCode.IsReadOnly = false;
                    textBox_ShipRegion.IsReadOnly = false;
                    textBox_ShipVia.IsReadOnly = false;
                }
                else
                {
                    // DatePickers
                    datePicker_OrderDate.IsEnabled = false;
                    datePicker_RequiredDate.IsEnabled = false;
                    datePicker_ShippedDate.IsEnabled = false;

                    // TextBoxes
                    textBox_Freight.IsReadOnly = true;
                    textBox_ShipAddress.IsReadOnly = true;
                    textBox_ShipCity.IsReadOnly = true;
                    textBox_ShipCountry.IsReadOnly = true;
                    textBox_ShipName.IsReadOnly = true;
                    textBox_ShipPostalCode.IsReadOnly = true;
                    textBox_ShipRegion.IsReadOnly = true;
                    textBox_ShipVia.IsReadOnly = true;
                }
            }
            else
            {
                MessageBox.Show("Please select an order", "No Order Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_SaveOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_NewOrder_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectedOrder = default;

            // DatePickers
            datePicker_OrderDate.SelectedDate = DateTime.Now;
            datePicker_OrderDate.IsEnabled = true;
            datePicker_RequiredDate.SelectedDate = DateTime.Now;
            datePicker_RequiredDate.IsEnabled = true;
            datePicker_ShippedDate.SelectedDate = DateTime.Now;
            datePicker_ShippedDate.IsEnabled = true;

            // TextBoxes
            textBox_Freight.Text = string.Empty;
            textBox_Freight.IsReadOnly = false;
            textBox_ShipAddress.Text = string.Empty;
            textBox_ShipAddress.IsReadOnly = false;
            textBox_ShipCity.Text = string.Empty;
            textBox_ShipCity.IsReadOnly = false;
            textBox_ShipCountry.Text = string.Empty;
            textBox_ShipCountry.IsReadOnly = false;
            textBox_ShipName.Text = string.Empty;
            textBox_ShipName.IsReadOnly = false;
            textBox_ShipPostalCode.Text = string.Empty;
            textBox_ShipPostalCode.IsReadOnly = false;
            textBox_ShipRegion.Text = string.Empty;
            textBox_ShipRegion.IsReadOnly = false;
            textBox_ShipVia.Text = string.Empty;
            textBox_ShipVia.IsReadOnly = false;
        }
        #endregion

        private void ListView_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //viewModel.SelectedOrderDetails = viewModel.SelectedOrder.OrderDetails;

            if(viewModel != null)
            {
                if(viewModel.SelectedOrder == null)
                {
                    // DatePickers
                    datePicker_OrderDate.SelectedDate = DateTime.Now;
                    datePicker_RequiredDate.SelectedDate = DateTime.Now;
                    datePicker_ShippedDate.SelectedDate = DateTime.Now;

                    // TextBoxes
                    textBox_Freight.Text = string.Empty;
                    textBox_ShipAddress.Text = string.Empty;
                    textBox_ShipCity.Text = string.Empty;
                    textBox_ShipCountry.Text = string.Empty;
                    textBox_ShipName.Text = string.Empty;
                    textBox_ShipPostalCode.Text = string.Empty;
                    textBox_ShipRegion.Text = string.Empty;
                    textBox_ShipVia.Text = string.Empty;
                } 
            }
        }
    }
}