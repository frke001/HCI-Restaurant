using Restaurant.DAO;
using Restaurant.DAO.MySQL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private readonly EmployeeDAOImpl _employeeDAO;
        public ObservableCollection<Employee> employees;
        public EmployeesPage employeesPage;
        public AddEmployeeWindow(ObservableCollection<Employee> employees, EmployeesPage employeesPage)
        {
            InitializeComponent();
            _employeeDAO = new EmployeeDAOImpl();
           
            this.employees = employees;
            this.employeesPage = employeesPage;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(SurnameTextBox.Text) || string.IsNullOrWhiteSpace(AddressTextBox.Text)
              || string.IsNullOrWhiteSpace(TelephoneTextBox.Text) || string.IsNullOrWhiteSpace(SalaryTextBox.Text) || string.IsNullOrWhiteSpace(UsernameTextBox.Text)
              || string.IsNullOrWhiteSpace(PasswordTextBox.Text) || string.IsNullOrWhiteSpace(JmbTextBox.Text) || TypeComboBox.SelectedIndex == -1)
            {
                if (AppUtil.currentLanguage == "English")
                {
                    var w = new WarningWindow("Incomplete data");
                    w.Title = "Warning";
                    w.ShowDialog();
                }
                else
                {
                    var w = new WarningWindow("Nepotpuni podaci!");
                    w.Title = "Upozorenje";
                    w.ShowDialog();
                }
            }
            else
            {
               
                try
                {
                    int salary = int.Parse(SalaryTextBox.Text);
                    sbyte type = sbyte.Parse(TypeComboBox.SelectedIndex == 0 ? "1" : "0");
                    /*string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
                    if(!Regex.IsMatch(PasswordTextBox.Text, passwordPattern))
                    {
                        new WarningWindow("Loznika nevalidna!");
                    }*/
                    if (JmbTextBox.Text.Length != 13 || !long.TryParse(JmbTextBox.Text, out _)) 
                    {
                        if (AppUtil.currentLanguage == "English")
                        {
                            var w = new WarningWindow("Invalid Id");
                            w.Title = "Warning";
                            w.ShowDialog();
                        }
                        else
                        {
                            var w = new WarningWindow("Nevalidan JMB!");
                            w.Title = "Upozorenje";
                            w.ShowDialog();
                        }
                        
                        return;
                    }
          
                    Employee toAdd = new Employee()
                    {
                        Jmb = JmbTextBox.Text,
                        Name = NameTextBox.Text,
                        Salary = salary,
                        Prezime = SurnameTextBox.Text,
                        Address = AddressTextBox.Text,
                        Telephone = TelephoneTextBox.Text,
                        Username = UsernameTextBox.Text,
                        Password = HashPassword(PasswordTextBox.Text),
                        IsManager = type
                    };
                    Employee temp = _employeeDAO.Add(toAdd);   
                    if(temp != null)
                    {
                        var em = _employeeDAO.GetAll();
                        employeesPage.EmployeesGrid.ItemsSource = em;                       
                        this.Close();
                        new SuccessNotificationWindow().ShowDialog();
                        
                    }else
                    {
                        if (AppUtil.currentLanguage == "English")
                        {
                            var w = new WarningWindow("Employee already exists!");
                            w.Title = "Warning";
                            w.ShowDialog();
                        }
                        else
                        {
                            var w = new WarningWindow("Zaposleni postoji!");
                            w.Title = "Upozorenje";
                            w.ShowDialog();
                        }
                    }
                    

                }
                catch (Exception ex)
                {
                    if (AppUtil.currentLanguage == "English")
                    {
                        var w = new WarningWindow("Invalid salary");
                        w.Title = "Warning";
                        w.ShowDialog();
                    }
                    else
                    {
                        var w = new WarningWindow("Nevalidna plata!");
                        w.Title = "Upozorenje";
                        w.ShowDialog();
                    }
                }


            }
        }
        private static string HashPassword(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedString;
            }
        }
    }
}
