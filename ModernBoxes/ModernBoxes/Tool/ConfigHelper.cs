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


        private Configuration configuration = null;
        public Configuration MyConfiguration
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


        public void setConfig(String key, Object value)
        {
            if (MyConfiguration.AppSettings.Settings[key]!=null)
            {
                MyConfiguration.AppSettings.Settings[key].Value = value.ToString();
            }
            else
            {
                MyConfiguration.AppSettings.Settings.Add(key, value.ToString());
            }
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public String getConfig(String key)
        {
            if (MyConfiguration.AppSettings.Settings[key].Value!=null)
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
