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
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        private readonly ArticleDAOImpl _articleDAO;
        private readonly OrderItemDaoImpl _orderItemDao;
        private readonly OrderDaoImpl _orderDao;
        public ObservableCollection<Article> Articles { get;  set; }
        public ObservableCollection<OrderItem> OrderItems { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public Employee Employee { get; set; }
        public CreateOrderWindow(Employee employee, ObservableCollection<Order> orders)
        {
            InitializeComponent();
            this._articleDAO = new ArticleDAOImpl();
            this._orderItemDao = new OrderItemDaoImpl();
            this._orderDao = new OrderDaoImpl();
            Articles = new ObservableCollection<Article>(_articleDAO.GetAll().Where(a => a.Active == 1));
            OrderItems = new ObservableCollection<OrderItem>();
            ArticlesGrid.ItemsSource = Articles;
            OrderItemsGrid.ItemsSource = OrderItems;
            QuantityTextBox.Text = "1";
            this.Employee = employee;
            this.Orders = orders;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if(OrderItems.Count() == 0)
            {
                new WarningWindow("Niste dodali stavke!").ShowDialog();
            }
            if(PaymentComboBox.SelectedIndex == -1)
            {
                new WarningWindow("Odaberite način plaćanja!").ShowDialog();
            }
            else
            {
                Order order = new Order();
                order.IssueDateAndTime = DateTime.Now;
                order.IsCanceled = 0;
                order.Jmbemployee = Employee.Jmb;
                if (PaymentComboBox.SelectedIndex == 0)
                    order.WithCash = 1;
                else
                    order.WithCash = 0;
                Order temp = _orderDao.Add(order);
                foreach (OrderItem item in OrderItems)
                {
                    item.OrderNumber = temp.OrderNumber;
                    item.IdArticleNavigation = null;
                    _orderItemDao.Add(item);    
                }
                Orders.Add(temp);
                this.Close();
                new SuccessNotificationWindow().ShowDialog();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(ArticlesGrid.SelectedItem != null) {
                Article selectedArticle = (Article) ArticlesGrid.SelectedItem;
                OrderItem item = new OrderItem();
                try
                {
                    item.Quantity = int.Parse(QuantityTextBox.Text);
                    item.Price = selectedArticle.Price;
                    item.IdArticle = selectedArticle.Id;
                    item.IdArticleNavigation = selectedArticle;
                    OrderItems.Add(item);
                }
                catch (Exception ex)
                {
                    new WarningWindow("Nevalidna količina!").ShowDialog();
                }
                
            }
            else
            {
                new WarningWindow("Izaberite artikal!").ShowDialog();
            }
            
        }

        private void CancelItemButton_Click(object sender, RoutedEventArgs e)
        {
            if(OrderItemsGrid.SelectedItem != null)
            {
                OrderItem item = (OrderItem)OrderItemsGrid.SelectedItem;
                OrderItems.Remove(item);
            }
            else
            {
                new WarningWindow("Izaberite stavku!").ShowDialog();
            }
        }
    }
}
