using System;
using System.Collections.Generic;
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
        public ReservationPage()
        {
            InitializeComponent();
        }

        private void AddReservationButton_Click(object sender, RoutedEventArgs e)
        {
            new AddReservationWindow().Show();
        }

        private void ModifyReservationButton_Click(object sender, RoutedEventArgs e)
        {
            new ModifyReservationWindow().Show();
        }
    }
}
