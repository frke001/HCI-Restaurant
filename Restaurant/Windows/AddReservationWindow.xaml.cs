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
    /// Interaction logic for AddReservationWindow.xaml
    /// </summary>
    public partial class AddReservationWindow : Window
    {
        private readonly ReservationDAOImpl _reservationDAO;
        public ObservableCollection<Reservation> Reservations { get; set; }
        public ReservationPage reservationPage { get; set; }
        public AddReservationWindow(ObservableCollection<Reservation> reservations, ReservationPage reservationPage)
        {
            InitializeComponent();
            this._reservationDAO = new ReservationDAOImpl();
            Reservations = reservations;
            this.reservationPage = reservationPage;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PersonNameTextBox.Text))
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
                Reservation reservation = new Reservation();
                reservation.DateAndTime = ReservationDatePicker.SelectedDate.Value; 
                reservation.PersonName = PersonNameTextBox.Text;
                Reservation res = _reservationDAO.Add(reservation);
                if(res == null)
                {
                    if (AppUtil.currentLanguage == "English")
                    {
                        var w = new WarningWindow("Reservation already exists!");
                        w.Title = "Warning";
                        w.ShowDialog();
                    }
                    else
                    {
                        var w = new WarningWindow("Rezervacija postoji!");
                        w.Title = "Upozorenje";
                        w.ShowDialog();
                    }
                }
                else
                {
                    
                    var all = _reservationDAO.GetAll();
                    reservationPage.ReservationsGrid.ItemsSource = all.Where(el => el.DateAndTime.Date == reservationPage.ReservationDatePicker.SelectedDate.Value.Date);
                    this.Close();
                    new SuccessNotificationWindow().ShowDialog();
                }
                
            }
        }
    }
}
