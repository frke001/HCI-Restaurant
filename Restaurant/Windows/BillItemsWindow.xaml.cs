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
using System.Windows.Shapes;

namespace Restaurant.Windows
{
    /// <summary>
    /// Interaction logic for BillItemsWindow.xaml
    /// </summary>
    public partial class BillItemsWindow : Window
    {
        private readonly OrderItemDaoImpl _orderItemDao;
        private Order order;
        public ObservableCollection<OrderItem> OrderItems { get; set; }

        public BillItemsWindow(Order order)
        {
            InitializeComponent();
            this._orderItemDao = new OrderItemDaoImpl();
            this.order = order;
            OrderItems = new ObservableCollection<OrderItem>(_orderItemDao.GetAllByOrderNumber(order.OrderNumber));
            BillItemsGrid.ItemsSource = OrderItems;
            EmployeeDAOImpl employeeDAOImpl = new EmployeeDAOImpl();
            EmployeeTextBox.Text = employeeDAOImpl.GetNameAndSurname(order.Jmbemployee);
            decimal totalPrice = 0;
            foreach(var el in OrderItems)
            {
                totalPrice += el.Price;
            }
            TotalPriceTextBox.Text = totalPrice.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}
