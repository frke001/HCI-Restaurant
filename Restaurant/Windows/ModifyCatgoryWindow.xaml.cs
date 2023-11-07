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
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.Windows
{
    /// <summary>
    /// Interaction logic for ModifyCatgoryWindow.xaml
    /// </summary>
    public partial class ModifyCatgoryWindow : Window
    {
        private ArticleType articleType;
        private readonly ArticleTypeDAOImpl _articleTypeDAO;
        public ObservableCollection<ArticleType> articleTypes;
        public ModifyCatgoryWindow(ArticleType articleType, ObservableCollection<ArticleType> articleTypes)
        {
            InitializeComponent();
            this.articleType = articleType;
            this._articleTypeDAO = new ArticleTypeDAOImpl();
            this.articleTypes = articleTypes;
            NameTextBox.Text = articleType.Name;    
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                if (AppUtil.currentLanguage == "English")
                {
                    var w = new WarningWindow("Incomplete data!");
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
               
                if (articleType.Name == NameTextBox.Text)
                {
                    this.Close();
                    new SuccessNotificationWindow().ShowDialog();
                    return;
                }
                ArticleType updated = new ArticleType()
                {
                    Name = NameTextBox.Text,
                    Id = articleType.Id
                };
                ArticleType temp = _articleTypeDAO.Update(updated);
                if (temp == null)
                {
                    if (AppUtil.currentLanguage == "English")
                    {
                        var w = new WarningWindow("Data already exists!");
                        w.Title = "Warning";
                        w.ShowDialog();
                    }
                    else
                    {
                        var w = new WarningWindow("Podatak vec postoji!");
                        w.Title = "Upozorenje";
                        w.ShowDialog();
                    }
                }
                else
                {
                    int index =  articleTypes.IndexOf(articleType);
                    articleTypes[index] = temp;
                    this.Close();
                    new SuccessNotificationWindow().ShowDialog();
                }
            }
        }
    }
}
