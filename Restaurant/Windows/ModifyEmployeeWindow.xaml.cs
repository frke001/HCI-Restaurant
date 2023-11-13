using Restaurant.DAO;
using Restaurant.DAO.MySQL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for ModifyEmployeeWindow.xaml
    /// </summary>
    public partial class ModifyEmployeeWindow : Window
    {
        private Employee employee;
        private readonly EmployeeDAOImpl _employeeDAOImpl;
        public ObservableCollection<Employee> employees;
        public EmployeesPage employeesPage;
        public ModifyEmployeeWindow(Employee employee, ObservableCollection<Employee> employees, EmployeesPage employeesPage)
        {
            InitializeComponent();
            this.employee = employee;
            this._employeeDAOImpl = new EmployeeDAOImpl();
            this.employees = employees;
            NameTextBox.Text = employee.Name;
            SurnameTextBox.Text = employee.Prezime;
            TelephoneTextBox.Text = employee.Telephone;
            AddressTextBox.Text = employee.Address;
            SalaryTextBox.Text = employee.Salary.ToString();
            this.employeesPage = employeesPage;
            if (employee.IsManager == 1)
                TypeComboBox.SelectedIndex = 0;
            else TypeComboBox.SelectedIndex = 1;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(SurnameTextBox.Text) || string.IsNullOrWhiteSpace(AddressTextBox.Text)
              || string.IsNullOrWhiteSpace(TelephoneTextBox.Text) || string.IsNullOrWhiteSpace(SalaryTextBox.Text) || TypeComboBox.SelectedIndex == -1)
            {
                if (AppUtil.currentLanguage == "English")
                {
                    var w = new WarningWindow("Incomplete data!");
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
                    Employee update = new Employee()
                    {
                        Jmb = employee.Jmb,
                        Name = NameTextBox.Text,
                        Salary = salary,
                        Prezime = SurnameTextBox.Text,
                        Address = AddressTextBox.Text,
                        Telephone = TelephoneTextBox.Text,
                        IsManager = type,
                        Password = employee.Password,
                        Username = employee.Username,
                        Theme = employee.Theme,
                        Language = employee.Language,
                    };
                    Employee temp = _employeeDAOImpl.Update(update);
                    if (temp == null)
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
                    else
                    {
                        /* int index = employees.IndexOf(employee);

                         employees[index] = temp;*/
                        var all = _employeeDAOImpl.GetAll();                                               
                        if (employeesPage.TypeComboBox.SelectedIndex == 0)
                            employeesPage.EmployeesGrid.ItemsSource = all;
                        else if (employeesPage.TypeComboBox.SelectedIndex == 1)
                            employeesPage.EmployeesGrid.ItemsSource = all.Where(e => e.IsManager == 1);
                        else
                            employeesPage.EmployeesGrid.ItemsSource = all.Where(e => e.IsManager == 0);
                        this.Close();
                        new SuccessNotificationWindow().ShowDialog();
                    }


                }
                catch (Exception ex)
                {
                    if (AppUtil.currentLanguage == "English")
                    {
                        var w = new WarningWindow("Invalid salary!");
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
       
    }
}
