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
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private readonly BillDaoImpl _billDaoImpl;
        public ObservableCollection<Order> Orders { get; set; }
        public Employee Employee { get; set; }
        public OrdersPage(Employee employee)
        {
            this._billDaoImpl = new BillDaoImpl();
            this.Orders = new ObservableCollection<Order>(_billDaoImpl.GetAll());
            InitializeComponent();

            BillGrid.ItemsSource = this.Orders.Where(o => o.IssueDateAndTime.Date == BillDatePicker.SelectedDate.Value.Date);
            Employee = employee;
        }

        private void CreateOrdeButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateOrderWindow(Employee,Orders).Show(); 
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = BillDatePicker.SelectedDate;
            BillGrid.ItemsSource = Orders.Where(o => o.IssueDateAndTime.Date == selectedDate.Value.Date);

        }
        private void BillGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Order selectedItem = (Order)BillGrid.SelectedItem;


            if (selectedItem != null)
            {
                new BillItemsWindow(selectedItem).ShowDialog();
            }
        }
    }
}
