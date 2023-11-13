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
    /// Interaction logic for AddArticleWindow.xaml
    /// </summary>
    public partial class AddArticleWindow : Window
    {
        private readonly ArticleTypeDAOImpl _articleTypeDAO;
        public ObservableCollection<Article> articles;
        public MenuPage menuPage;
        public AddArticleWindow(ObservableCollection<Article> articles, MenuPage menuPage)
        {
            InitializeComponent();
            _articleTypeDAO = new ArticleTypeDAOImpl();
            ArticleTypeComboBox.ItemsSource = _articleTypeDAO.GetAll().Select(article => article.Name).ToList();
            this.articles = articles;
            this.menuPage = menuPage;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(PriceTextBox.Text) 
                || string.IsNullOrWhiteSpace(DescriptionTextBox.Text) || ArticleTypeComboBox.SelectedIndex == -1)
            {
                if (AppUtil.currentLanguage == "English")
                {
                    var w = new WarningWindow("Uncompleted data!");
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
                ArticleTypeDAOImpl articleTypeDAO = new ArticleTypeDAOImpl();
                var articleTypes = articleTypeDAO.GetAll();
                ArticleType articleType = articleTypes.Where(articleType => articleType.Name == ArticleTypeComboBox.SelectedItem.ToString()).FirstOrDefault();
                int articleTypeId = articleType.Id;
                ArticleDAOImpl articleDAO = new ArticleDAOImpl();
                try
                {
                    decimal price = Decimal.Parse(PriceTextBox.Text);
                    Article temp = articleDAO.Add(new Article(NameTextBox.Text, price, DescriptionTextBox.Text, 1, articleTypeId));
                    /*temp.IdTypeNavigation = articleType;
                    articles.Add(temp);*/
                    if (temp == null)
                    {
                        if (AppUtil.currentLanguage == "English")
                        {
                            var w = new WarningWindow("Article already exists!");
                            w.Title = "Warning";
                            w.ShowDialog();
                        }
                        else
                        {
                            var w = new WarningWindow("Artikal postoji!");
                            w.Title = "Upozorenje";
                            w.ShowDialog();
                        }
                        
                    }
                    else
                    {
                        var all = articleDAO.GetAll();
         
                        if (menuPage.TypeComboBox.SelectedIndex == 1)
                            menuPage.ArticlesGrid.ItemsSource = all.Where(el => el.Active == 1);
                        else
                        {
                            menuPage.ArticlesGrid.ItemsSource = all;
                        }
                        this.Close();
                        new SuccessNotificationWindow().ShowDialog();
                    }
                    

                }
                catch (Exception ex)
                {
                    if (AppUtil.currentLanguage == "English")
                    {
                        var w = new WarningWindow("Invalid price!");
                        w.Title = "Warning";
                        w.ShowDialog();
                    }
                    else
                    {
                        var w = new WarningWindow("Nevalidna cijena!");
                        w.Title = "Upozorenje";
                        w.ShowDialog();
                    }
                    
                }

                            
            }
        }
    }
}
