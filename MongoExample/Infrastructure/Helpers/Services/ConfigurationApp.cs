using Microsoft.Extensions.Configuration;
using MongoExample.Infrastructure.Helpers.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoExample.Infrastructure.Helpers.Services
{
    public class ConfigurationApp : IConfigurationApp
    {
        private readonly IConfiguration _configuration;

        public ConfigurationApp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetValueKey(string key)
        {
            return _configuration[key];
        }

        public T? GetValueKey<T>(string key) where T : class
        {
            var cnn = this.GetValueKey(key);
            var info = JsonConvert.DeserializeObject<T>(cnn);

            if(info is null)
            {
                Console.WriteLine($"{key} be not find!");
            }

            return info;
        }
    }
}
