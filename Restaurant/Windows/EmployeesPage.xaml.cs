using Restaurant.DAO;
using Restaurant.DAO.MySQL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Restaurant.Windows
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        private readonly EmployeeDAOImpl _employeeDAO;

        public ObservableCollection<Employee> Employees { get; set; }
        public EmployeesPage()
        {
           
            InitializeComponent();
            _employeeDAO = new EmployeeDAOImpl();

            Employees = new ObservableCollection<Employee>(_employeeDAO.GetAll());
            EmployeesGrid.ItemsSource = Employees;
            TypeComboBox.SelectedIndex = 0;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            new AddEmployeeWindow(Employees, this).ShowDialog();
        }

        private void ModifyEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem != null)
            {
                Employee employee = (Employee)EmployeesGrid.SelectedItem;
                new ModifyEmployeeWindow(employee, Employees,this).ShowDialog();
            }
            else
            {
                new WarningWindow("Nije selektovan podatak").ShowDialog();
            }
            
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = _employeeDAO.GetAll();
            if (TypeComboBox.SelectedIndex == 0)
            {
                EmployeesGrid.ItemsSource = temp;
            }
            else if(TypeComboBox.SelectedIndex == 1)
            {
                EmployeesGrid.ItemsSource = temp.Where(employee => employee.IsManager == 1);
            }
            else
            {
                EmployeesGrid.ItemsSource = temp.Where(employee => employee.IsManager == 0);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                if (TypeComboBox.SelectedIndex == 0)
                    EmployeesGrid.ItemsSource = Employees;
                else if (TypeComboBox.SelectedIndex == 1)
                    EmployeesGrid.ItemsSource = Employees.Where(employee => employee.IsManager == 1);
                else
                    EmployeesGrid.ItemsSource = Employees.Where(employee => employee.IsManager == 0);
            }
            else
            {
                string searchText = SearchTextBox.Text.ToLower();
                if(TypeComboBox.SelectedIndex == 0)
                    EmployeesGrid.ItemsSource = Employees.Where(employee => employee.Name.ToLower().Contains(searchText) || employee.Prezime.ToLower().Contains(searchText));
                else if(TypeComboBox.SelectedIndex == 1)
                    EmployeesGrid.ItemsSource = Employees.Where(employee => employee.IsManager ==1 && (employee.Name.ToLower().Contains(searchText) || employee.Prezime.ToLower().Contains(searchText)));
                else
                    EmployeesGrid.ItemsSource = Employees.Where(employee => employee.IsManager == 0 && (employee.Name.ToLower().Contains(searchText) || employee.Prezime.ToLower().Contains(searchText)));
            }
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem != null)
            {
                Employee selectedEmployee = (Employee)EmployeesGrid.SelectedItem;
                
                _employeeDAO.Delete(selectedEmployee);
                Employees = new ObservableCollection<Employee>(_employeeDAO.GetAll());
                if(TypeComboBox.SelectedIndex == 0)
                    EmployeesGrid.ItemsSource = Employees;
                else if(TypeComboBox.SelectedIndex == 1)
                    EmployeesGrid.ItemsSource = Employees.Where(e => e.IsManager == 1);
                else
                    EmployeesGrid.ItemsSource = Employees.Where(e => e.IsManager == 0);
            }
            else
            {
                new WarningWindow("Nije selektovan podatak").ShowDialog();
            }
        }
    }
}
