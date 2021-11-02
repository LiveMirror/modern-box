using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Tool
{
    public class ConfigHelper
    {


        private static Configuration configuration = null;
        public static Configuration MyConfiguration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    return configuration;
                }
                return configuration;
            }

        }


        public static void setConfig(String key, Object value)
        {
            if (MyConfiguration.AppSettings.Settings[key]!=null)
            {
                MyConfiguration.AppSettings.Settings[key].Value = value.ToString();
            }
            else
            {
                MyConfiguration.AppSettings.Settings.Add(key, value.ToString());
            }
            MyConfiguration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static String getConfig(String key)
        {
            if (MyConfiguration.AppSettings.Settings[key]!=null)
            {
                return MyConfiguration.AppSettings.Settings[key].Value;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
