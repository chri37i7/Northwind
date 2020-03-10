using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Northwind.Gui.Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for MasterSelectorUserControl.xaml
    /// </summary>
    public partial class MasterSelectorUserControl : UserControl
    {
        private List<ToggleButton> toggleButtons;
        private HrUserControl hrUserControl;

        public MasterSelectorUserControl()
        {
            InitializeComponent();

            // Add all toggleButtons to the list:
            toggleButtons = new List<ToggleButton>();
            foreach(object item in stackPanelForToggleButtons.Children)
            {
                if((item as ToggleButton) != null)
                {
                    toggleButtons.Add(item as ToggleButton);
                }
            }
        }

        /// <summary>
        /// Ensures only one <see cref="ToggleButton"/> is checked at a time.
        /// </summary>
        /// <param name="toggleButton">The <see cref="ToggleButton"/> to set as checked.</param>
        private void Toggle(ToggleButton toggleButton)
        {
            if(toggleButton != null)
            {
                foreach(ToggleButton tb in toggleButtons)
                {
                    tb.IsChecked = false;
                }
                toggleButton.IsChecked = true;
            }
        }

        private void ToggleButton_HR_Click(object sender, RoutedEventArgs e)
        {
            Toggle(sender as ToggleButton);
            if(hrUserControl is null)   // only null the first time - i.e. only one HrUserControl object is ever created, so when the button is clicked, state is preserved.
            {
                hrUserControl = new HrUserControl();
            }
            MainWindow.Instance.SetDetailsUserControlTo(hrUserControl);
        }

        private void ToggleButton_Products_Click(object sender, RoutedEventArgs e)
        {
            Toggle(sender as ToggleButton);
        }

        private void ToggleButton_Invoices_Click(object sender, RoutedEventArgs e)
        {
            Toggle(sender as ToggleButton);
        }

        private void ToggleButton_Orders_Click(object sender, RoutedEventArgs e)
        {
            Toggle(sender as ToggleButton);
        }
    }
}