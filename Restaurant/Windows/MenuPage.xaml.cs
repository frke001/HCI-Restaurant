﻿using Restaurant.DAO;
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
using System.Windows.Controls.Primitives;
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
        private readonly ArticleDAOImpl _articleDAO;

        public ObservableCollection<Article> Articles { get; set; }
        public MenuPage()
        {
            
            InitializeComponent();
            _articleDAO = new ArticleDAOImpl();

            Articles = new ObservableCollection<Article>(_articleDAO.GetAll());
            ArticlesGrid.ItemsSource = Articles;
            TypeComboBox.SelectedIndex = 0;
        }

        private void AddArticleButton_Click(object sender, RoutedEventArgs e)
        {

            new AddArticleWindow(Articles,this).Show();
        }

        private void ModifyArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ArticlesGrid.SelectedItem != null)
            {
                Article article = (Article)ArticlesGrid.SelectedItem;
                new ModifyArticleWindow(article, Articles,this).ShowDialog();
            }
            else
            {
                new WarningWindow("Nije selektovan podatak").ShowDialog();
            }
        }

        private void DeleteArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ArticlesGrid.SelectedItem != null)
            {
                Article selectedArticle = (Article)ArticlesGrid.SelectedItem;
               
                /*if (TypeComboBox.SelectedIndex == 1)
                    Articles.Remove(selectedArticle);*/
                _articleDAO.Delete(selectedArticle);   
                Articles = new ObservableCollection<Article>(_articleDAO.GetAll());
                if (TypeComboBox.SelectedIndex == 1)
                    ArticlesGrid.ItemsSource = Articles.Where(el => el.Active == 1);
                else
                {
                    ArticlesGrid.ItemsSource = Articles;
                }
                new SuccessNotificationWindow().ShowDialog();   
            }
            else
            {
                new WarningWindow("Nije selektovan podatak").ShowDialog();
            }
        }

        private void ManageArticleButton_Click(object sender, RoutedEventArgs e)
        {
            new ManageArticleTypeWindow().Show();   
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = _articleDAO.GetAll();
            if(TypeComboBox.SelectedIndex == 0)
            {
                ArticlesGrid.ItemsSource = temp;
            }
            else
            {
                ArticlesGrid.ItemsSource = temp.Where(article => article.Active==1);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                if (TypeComboBox.SelectedIndex == 0)
                    ArticlesGrid.ItemsSource = Articles;
                else
                    ArticlesGrid.ItemsSource = Articles.Where(article => article.Active == 1);
               
            }
            else
            {
                string searchText = SearchTextBox.Text.ToLower();
                if(TypeComboBox.SelectedIndex == 0)
                    ArticlesGrid.ItemsSource = Articles.Where(article => article.Name.ToLower().Contains(searchText));
                else
                    ArticlesGrid.ItemsSource = Articles.Where(article => article.Name.ToLower().Contains(searchText) && article.Active == 1);
            }          
        }
    }
}
