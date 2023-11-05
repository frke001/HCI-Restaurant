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
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            
        }

        private void AddArticleButton_Click(object sender, RoutedEventArgs e)
        {
            new AddArticleWindow().Show();
        }

        private void ModifyArticleButton_Click(object sender, RoutedEventArgs e)
        {
            new ModifyArticleWindow().Show();
        }

        private void DeleteArticleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ManageArticleButton_Click(object sender, RoutedEventArgs e)
        {
            new ManageArticleTypeWindow().Show();   
        }
    }
}
