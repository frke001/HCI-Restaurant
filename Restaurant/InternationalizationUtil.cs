using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;

namespace Restaurant
{
    class InternationalizationUtil
    {

        private static ResourceManager resourceManager;

        static InternationalizationUtil()
        {
            resourceManager = new ResourceManager("Restaurant.Languages.Language",Assembly.GetExecutingAssembly());
        }

        public static string? GetString(string key)
        {
            return resourceManager.GetString(key);
        }

        public static void ChangeLanguage(string language)
        {
            var cultureInfo = new CultureInfo(language);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
    }
}
