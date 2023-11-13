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
                    if (AppUtil.currentLanguage == "English")
                    {
                        var w = new WarningWindow("Bill already canceled");
                        w.Title = "Warning";
                        w.ShowDialog();
                    }
                    else
                    {
                        var w = new WarningWindow("Račun je već storniran!");
                        w.Title = "Upozorenje";
                        w.ShowDialog();
                    }
                    return;
                }
                selectedOrder.IsCanceled = 1;
                Order temp = _billDaoImpl.Delete(selectedOrder);
               
                BillGrid.ItemsSource = Orders.Where(el => el.IssueDateAndTime.Date == BillDatePicker.SelectedDate.Value.Date);
                new SuccessNotificationWindow().Show();
            }
            else
            {
                if (AppUtil.currentLanguage == "English")
                {
                    var w = new WarningWindow("Data not selected");
                    w.Title = "Warning";
                    w.ShowDialog();
                }
                else
                {
                    var w = new WarningWindow("Nije selektovan podatak!");
                    w.Title = "Upozorenje";
                    w.ShowDialog();
                }
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

            DataGridRow row = sender as DataGridRow;

            if (row != null )
            {
                e.Handled = true;
            }
        }
    }
}
