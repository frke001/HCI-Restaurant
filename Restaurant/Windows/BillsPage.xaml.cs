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
    /// Interaction logic for BillsPage.xaml
    /// </summary>
    public partial class BillsPage : Page
    {
        private readonly BillDaoImpl _billDaoImpl;
        public ObservableCollection<Order> Orders { get; set; }
        public BillsPage()
        {
            this._billDaoImpl = new BillDaoImpl();
            this.Orders = new ObservableCollection<Order>(_billDaoImpl.GetAll());
            InitializeComponent();

            BillGrid.ItemsSource = this.Orders.Where(o => o.IssueDateAndTime.Date == BillDatePicker.SelectedDate.Value.Date);
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = BillDatePicker.SelectedDate;
            BillGrid.ItemsSource = Orders.Where(o => o.IssueDateAndTime.Date == selectedDate.Value.Date);
        }

        private void CancelBillButton_Click(object sender, RoutedEventArgs e)
        {
            if (BillGrid.SelectedItem != null)
            {
                Order selectedOrder = (Order)BillGrid.SelectedItem;
                if(selectedOrder.IsCanceled == 1)
                {
                    new WarningWindow("Racun je vec storniran").ShowDialog();
                    return;
                }
                selectedOrder.IsCanceled = 1;
                Order temp = _billDaoImpl.Delete(selectedOrder);
                BillGrid.ItemsSource = _billDaoImpl.GetAll();
                new SuccessNotificationWindow().Show();
            }
            else
            {
                new WarningWindow("Nije selektovan podatak").ShowDialog();
            }
        }
        private void BillItemsGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem);

            Order selectedItem = (Order)BillGrid.SelectedItem;

           
            if (selectedItem != null)
            {
                new BillItemsWindow(selectedItem).ShowDialog();
            }
        }
        private void BillGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Dobijte red koji je izazvao događaj
            DataGridRow row = sender as DataGridRow;

            // Ako želite onemogućiti dvoklik na određenim redovima, postavite uslov prema vašim potrebama
            if (row != null )
            {
                // Ako red ispunjava vaš kriterijum, sprečite dalje širenje događaja
                e.Handled = true;
            }
        }
    }
}
