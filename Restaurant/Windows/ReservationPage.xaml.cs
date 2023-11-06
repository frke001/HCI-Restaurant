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
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : Page
    {
        private readonly ReservationDAOImpl _reservationDAO;
        public ObservableCollection<Reservation> Reservations { get; set; }
        public ReservationPage()
        {
            this._reservationDAO = new ReservationDAOImpl();
            Reservations = new ObservableCollection<Reservation>(_reservationDAO.GetAll());
            InitializeComponent();
            //ReservationsGrid.ItemsSource = Reservations;
            DateTime? selectedDate = ReservationDatePicker.SelectedDate;
            ReservationsGrid.ItemsSource = Reservations.Where(o => o.DateAndTime.Date == selectedDate.Value.Date);
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = ReservationDatePicker.SelectedDate;
            ReservationsGrid.ItemsSource = Reservations.Where(o => o.DateAndTime.Date == selectedDate.Value.Date);
        }
        private void AddReservationButton_Click(object sender, RoutedEventArgs e)
        {
            new AddReservationWindow(Reservations,this).Show();
        }

        private void ModifyReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(ReservationsGrid.SelectedItem != null)
            {
                Reservation reservation = (Reservation)ReservationsGrid.SelectedItem;
                new ModifyReservationWindow(Reservations, reservation).Show();
            }
            else
            {
                new WarningWindow("Izaberite podatak!").ShowDialog();
            }
            
        }

        private void DeleteReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(ReservationsGrid.SelectedItem != null)
            {
                Reservation selected = (Reservation)ReservationsGrid.SelectedItem;
                _reservationDAO.Delete(selected);
                Reservations.Remove(selected);
                ReservationsGrid.ItemsSource = _reservationDAO.GetAll().Where(el => el.DateAndTime.Date == ReservationDatePicker.SelectedDate.Value.Date);
                new SuccessNotificationWindow().ShowDialog();
            }else
            {
                new WarningWindow("Izaberite podatak!").ShowDialog();
            }
        }
    }
}
