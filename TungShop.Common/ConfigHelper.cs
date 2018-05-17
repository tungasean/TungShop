using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TungShop.Common
{
    public class ConfigHelper
    {
        public static string GetByKey(string key)
        {
            if(ConfigurationManager.AppSettings[key] != null)
            return ConfigurationManager.AppSettings[key].ToString();
            return "";
        }
    }
}
