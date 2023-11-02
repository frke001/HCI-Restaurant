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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String selectedTheme = ThemeComboBox.SelectedValue.ToString();
            if(selectedTheme == "Svijetla")
            {
                AppUtil.ChangeTheme(new Uri("/Dictionaries/LightTheme.xaml", UriKind.Relative));
            }
            if (selectedTheme == "Tamna")
            {
                AppUtil.ChangeTheme(new Uri("/Dictionaries/DarkTheme.xaml", UriKind.Relative));
            }
            if(selectedTheme == "Hibridna")
            {
                AppUtil.ChangeTheme(new Uri("/Dictionaries/HibridTheme.xaml", UriKind.Relative));
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(LanguageComboBox.SelectedIndex == 0)
            {
                AppUtil.ChangeLanguage(new Uri("/Languages/EnglishLanguage.xaml", UriKind.Relative));
            }
            else
            {
                AppUtil.ChangeLanguage(new Uri("/Languages/SerbianLanguage.xaml", UriKind.Relative));
            }
        }
    }
}
