using Restaurant.DAO.MySQL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly EmployeeDAOImpl _employeeDAOImpl;
        public LoginWindow()
        {
            InitializeComponent();
            _employeeDAOImpl = new EmployeeDAOImpl();
            RestaurantDaoImpl restaurantDaoImpl = new RestaurantDaoImpl();
            Models.Restaurant restaurant = restaurantDaoImpl.GetInfo();
            if (restaurant != null)
            {
                WelcomeLabel.Content = "Welcome to " + restaurant.Name;
                ContactLabel.Content = "Contact: " + restaurant.Telephone;
                AddressLabel.Content = "Address: " + restaurant.Address;
            }
        
        }


        private void SingInButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                new WarningWindow("Nepotpuni podaci!").ShowDialog();
            }
            else
            {
      
                Employee employee = _employeeDAOImpl.FindByUsernameAndPassword(UsernameTextBox.Text, PasswordTextBox.Password);
                if(employee == null)
                {
                    new WarningWindow("Netačni kredencijali!").ShowDialog();
                    UsernameTextBox.Clear();
                    PasswordTextBox.Clear();
                    return;
                }
                else
                {
                    if(employee.IsManager == 1)
                    {   
                        new ManagerWindow(employee,this).ShowDialog();
                        

                    }
                    else
                    {                        
                        new WorkerWindow(employee,this).ShowDialog();
                        
                    }
                    

                }
            }
           
        }
    }
}
