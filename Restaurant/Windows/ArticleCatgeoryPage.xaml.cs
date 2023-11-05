﻿using Restaurant.DAO;
using Restaurant.DAO.MySQL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
    /// Interaction logic for ArticleCatgeoryPage.xaml
    /// </summary>
    public partial class ArticleCatgeoryPage : Page
    {
        private readonly ArticleTypeDAOImpl _articleTypeDAO;

        public ObservableCollection<ArticleType> ArticleTypes { get; set; }
        public ArticleCatgeoryPage()
        {
            InitializeComponent();
            _articleTypeDAO = new ArticleTypeDAOImpl();

            ArticleTypes = new ObservableCollection<ArticleType>(_articleTypeDAO.GetAll());
            articleTypeGrid.ItemsSource = ArticleTypes; 

            //Loaded += async (sender, e) => await LoadData();
        }

       // private async Task LoadData()
       // {
       //     List<ArticleType> articleTypes = await _articleTypeDAO.GetAll();
       //
       //     foreach (var articleType in articleTypes)
       //     {
       //         ArticleTypes.Add(articleType);
       //     }
       // }

      

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            new AddCategoryWindow(ArticleTypes).Show(); 
        }

        private void ModifyCategoryButton_CLick(object sender, RoutedEventArgs e)
        {
            if (articleTypeGrid.SelectedItem != null)
            {
                ArticleType selectedArticleType = (ArticleType)articleTypeGrid.SelectedItem;
                new ModifyCatgoryWindow(selectedArticleType,ArticleTypes).Show();
            }
            else
            {
                new WarningWindow("Nije selektovan podatak").ShowDialog();
            }
           
        }

        private void DeleteCategoryButton_CLick(object sender, RoutedEventArgs e)
        {
            if(articleTypeGrid.SelectedItem != null)
            {
                ArticleType selectedArticleType = (ArticleType)articleTypeGrid.SelectedItem;
                ArticleTypes.Remove(selectedArticleType);
                _articleTypeDAO.Delete(selectedArticleType);
            }
            else
            {
                new WarningWindow("Nije selektovan podatak").ShowDialog();
            }
        
        }
    }
}
