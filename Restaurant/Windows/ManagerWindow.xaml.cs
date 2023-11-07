using Restaurant.Models;
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
        private Employee employee;
        public ManagerWindow(Employee employee,LoginWindow loginWindow)
        {
            loginWindow.Close();
            InitializeComponent();
            SettingsPage = new SettingsPage(employee);
             
            this.employee = employee; 
            if(employee.Theme == "Svijetla")
                SettingsPage.ThemeComboBox.SelectedIndex = 0;
            else if(employee.Theme == "Tamna")
                SettingsPage.ThemeComboBox.SelectedIndex = 1;
            else
                SettingsPage.ThemeComboBox.SelectedIndex = 2;
            if (employee.Language == "Engleski")
                SettingsPage.LanguageComboBox.SelectedIndex = 0;
            else
                SettingsPage.LanguageComboBox.SelectedIndex = 1;
           
        }
        public SettingsPage SettingsPage { get; set; }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            /*pageFrame.Navigate(new MenuPage());*/
            pageFrame.Content = new MenuPage();
            this.Title = "Articles";
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Navigate(new EmployeesPage());
            this.Title = "Employees";
        }

        private void BillsButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Navigate(new BillsPage());
            this.Title = "Bills";
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Navigate(SettingsPage);
            this.Title = "Settings";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
            
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Navigate(new ArticleCatgeoryPage());
        }
    }
}
