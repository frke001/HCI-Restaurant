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
    /// Interaction logic for ModifyReservationWindowWindow.xaml
    /// </summary>
    public partial class ModifyReservationWindow : Window
    {
        private readonly ReservationDAOImpl _reservationDAO;
        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation reservation;
        public ModifyReservationWindow(ObservableCollection<Reservation> reservations, Reservation reservation)
        {
            InitializeComponent();
            this._reservationDAO = new ReservationDAOImpl();
            Reservations = reservations;
            this.reservation = reservation;
            PersonNameTextBox.Text = reservation.PersonName;
            ReservationDatePicker.SelectedDate = reservation.DateAndTime;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PersonNameTextBox.Text))
            {
                new WarningWindow("Nepotpuni podaci!").ShowDialog();
            }
            else
            {
                Reservation temp = new Reservation();
                temp.PersonName = PersonNameTextBox.Text;
                temp.DateAndTime = ReservationDatePicker.SelectedDate.Value;
                temp.IdReservation = reservation.IdReservation;
                Reservation r = _reservationDAO.Update(temp);
                if (r != null)
                {
                    int index = Reservations.IndexOf(reservation);
                    Reservations[index] = temp;
                    this.Close();
                    new SuccessNotificationWindow().ShowDialog();
                }
                else
                {
                    new WarningWindow("Rerzervacija postoji!").ShowDialog();
                }
            }
        }
    }
}
