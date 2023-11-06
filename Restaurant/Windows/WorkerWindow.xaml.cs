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
    /// Interaction logic for WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {

        private Employee employee;
        public WorkerWindow(Employee employee, LoginWindow loginWindow)
        {
            loginWindow.Close();
            InitializeComponent();
            SettingsPage = new SettingsPage(employee);
          
            this.employee = employee;
            if (employee.Theme == "Svijetla")
                SettingsPage.ThemeComboBox.SelectedIndex = 0;
            else if (employee.Theme == "Tamna")
                SettingsPage.ThemeComboBox.SelectedIndex = 1;
            else
                SettingsPage.ThemeComboBox.SelectedIndex = 2;
            if (employee.Language == "Engleski")
                SettingsPage.LanguageComboBox.SelectedIndex = 0;
            else
                SettingsPage.LanguageComboBox.SelectedIndex = 1;
        }
        public SettingsPage SettingsPage { get; set; }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            WorkerPageFrame.Navigate(SettingsPage);
            this.Title = "Settings";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            WorkerPageFrame.Navigate(new OrdersPage(employee)); 
            this.Title = "Bills/Orders";
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            WorkerPageFrame.Navigate(new ReservationPage());
            this.Title = "Reservations";
        }
    }
}
