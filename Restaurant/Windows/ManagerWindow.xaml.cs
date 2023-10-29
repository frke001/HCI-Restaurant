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
using System.Windows.Shapes;

namespace Restaurant.Windows
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            /*pageFrame.Navigate(new MenuPage());*/
            pageFrame.Content = new MenuPage();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Navigate(new EmployeesPage());
        }

        private void BillsButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Navigate(new BillsPage());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Navigate(new SettingsPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
            
        }
    }
}
