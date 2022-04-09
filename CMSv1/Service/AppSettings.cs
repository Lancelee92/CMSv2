using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Service
{
    public class AppSettings
    {
        private static AppSettings _appSettings;

        public string ConnectionString { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtSecret { get; set; }
        public string EncryptKey { get; set; }
        public string Key { get; set; }

        public AppSettings(IConfiguration config)
        {
            this.ConnectionString = config.GetValue<String>("Database:ConnectionString");
            this.JwtSecret = config.GetValue<String>("JWT:Secret");
            this.JwtAudience = config.GetValue<String>("JWT:ValidIssuer");
            this.JwtIssuer = config.GetValue<String>("JWT:ValidAudience");
            this.EncryptKey = config.GetValue<String>("JWT:EncryptKey");
            this.Key = config.GetValue<String>("JWT:Key");

            // Now set Current
            _appSettings = this;
        }

        public static AppSettings Current
        {
            get
            {
                if (_appSettings == null)
                {
                    _appSettings = GetCurrentSettings();
                }

                return _appSettings;
            }
        }

        public static AppSettings GetCurrentSettings()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new AppSettings(configuration);

            return settings;
        }
    }
}
