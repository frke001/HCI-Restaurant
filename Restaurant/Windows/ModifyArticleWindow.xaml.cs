﻿using Restaurant.DAO;
using Restaurant.DAO.MySQL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
    /// Interaction logic for ModifyArticleWindow.xaml
    /// </summary>
    public partial class ModifyArticleWindow : Window
    {
        private Article article;
        private readonly ArticleDAOImpl _articleDAO;
        public ObservableCollection<Article> articles;
        public MenuPage menuPage;
        public ModifyArticleWindow(Article article, ObservableCollection<Article> articles, MenuPage menuPage)
        {
            InitializeComponent();
            this.article = article;
            this._articleDAO = new ArticleDAOImpl();
            this.articles = articles;
            this.menuPage = menuPage;
            NameTextBox.Text = article.Name;
            PriceTextBox.Text = article.Price.ToString();
            DescriptionTextBox.Text = article.Description;
            var articleTypes = new ArticleTypeDAOImpl().GetAll();
            
            ArticleTypeComboBox.ItemsSource = articleTypes;
            ArticleTypeComboBox.DisplayMemberPath = "Name";
            ArticleTypeComboBox.SelectedValuePath = "Id";

            ArticleTypeComboBox.SelectedValue = article.IdType;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(PriceTextBox.Text)
               || string.IsNullOrWhiteSpace(DescriptionTextBox.Text) || ArticleTypeComboBox.SelectedIndex == -1)
            {
                new WarningWindow("Nepotpuni podaci!").ShowDialog();
            }
            else
            {
                ArticleTypeDAOImpl articleTypeDAO = new ArticleTypeDAOImpl();
                var articleTypes = articleTypeDAO.GetAll();
                var t = (ArticleType) ArticleTypeComboBox.SelectedItem;
                ArticleType articleType = articleTypes.Where(articleType => articleType.Name == t.Name).FirstOrDefault();
                int articleTypeId = articleType.Id;
                ArticleDAOImpl articleDAO = new ArticleDAOImpl();
                try
                {
                    decimal price = Decimal.Parse(PriceTextBox.Text);
                    Article updated = new Article()
                    {
                        Id = article.Id,
                        Name = NameTextBox.Text,
                        Price = price,
                        Description = DescriptionTextBox.Text,
                        Active = 1,
                        IdType = articleTypeId,
                    };
                    Article temp = articleDAO.Update(updated);
                    if (temp == null)
                    {
                        new WarningWindow("Postojeći artikal!").ShowDialog();
                        
                     
                    }
                    else
                    {
                        /*temp.IdTypeNavigation = articleType;
                        int index = articles.IndexOf(article);
                        articles[index] = temp;*/
                        menuPage.ArticlesGrid.ItemsSource = _articleDAO.GetAll();
                        this.Close();
                        new SuccessNotificationWindow().ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    new WarningWindow("Nevalidna cijena!").ShowDialog();
                }


            }
        }
    }
}
