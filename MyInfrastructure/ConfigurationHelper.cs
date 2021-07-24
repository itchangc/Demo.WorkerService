using Microsoft.Extensions.Configuration;
using System;

namespace MyInfrastructure
{

    public class ConfigurationHelper
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// key AppSetting:DbType
        /// </summary>
        /// <param name="key">AppSetting:DbType</param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var section = Configuration.GetSection(key) as ConfigurationSection;
                if (section != null && !string.IsNullOrEmpty(section.Value))
                {
                    return section.Value;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IConfigurationSection GetSection(string key)
        {
            return Configuration.GetSection(key);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Bind(string key, object instance)
        {
            Configuration.Bind(key, instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return Configuration.GetConnectionString(key);
            }
            return string.Empty;
        }

        public static string CurrentPath { get; set; }
    }

}
