using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        private readonly ArticleTypeDAOImpl _articleTypeDAO;
        public ObservableCollection<ArticleType> articleTypes;
        public AddCategoryWindow(ObservableCollection<ArticleType> articleTypes)
        {
            InitializeComponent();
            _articleTypeDAO = new ArticleTypeDAOImpl();
            this.articleTypes = articleTypes;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                new WarningWindow("Nepotpuno polje!").ShowDialog();
            }
            else
            {
                ArticleType temp = _articleTypeDAO.Add(new ArticleType(NameTextBox.Text));
                if (temp == null)
                {
                    new WarningWindow("Podatak vec postoji!").ShowDialog();
                }
                else
                {
                    articleTypes.Add(temp);
                    this.Close();
                    new SuccessNotificationWindow().ShowDialog();
                }
            }

        }
    }
}
