using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant
{
    class AppUtil
    {
        public static void ChangeTheme(Uri themeUri)
        {
            ResourceDictionary Theme = new ResourceDictionary();
            Theme.Source = themeUri;

            var themeDictionaries = App.Current.Resources.MergedDictionaries
            .Where(d => d.Source != null && d.Source.ToString().Contains("Theme"))
            .ToList();

            foreach (var themeDictionary in themeDictionaries)
            {
                App.Current.Resources.MergedDictionaries.Remove(themeDictionary);
            }
            
            

            App.Current.Resources.MergedDictionaries.Add(Theme);

        }

        public static void ChangeLanguage(Uri languageUri)
        {
            ResourceDictionary Lang = new ResourceDictionary();
            Lang.Source = languageUri;

            var langDictionaries = App.Current.Resources.MergedDictionaries
            .Where(d => d.Source != null && d.Source.ToString().Contains("Language"))
            .ToList();

            foreach (var langDictionary in langDictionaries)
            {
                App.Current.Resources.MergedDictionaries.Remove(langDictionary);
            }
            


            App.Current.Resources.MergedDictionaries.Add(Lang);

        }
    }
}
