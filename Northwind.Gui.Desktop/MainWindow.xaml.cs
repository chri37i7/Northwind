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
        private static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();
            masterSelectorUserControl.Content = new UserControls.MasterSelectorUserControl();
            instance = this;
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            string text = $"Assembly version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
            MessageBox.Show(text, "Om Northwind", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void SetDetailsUserControlTo(UserControl userControl)
        {
            detailsUserControl.Content = userControl;
        }

        public static MainWindow Instance
        {
            get
            {
                return instance;
            }
        }
    }
}