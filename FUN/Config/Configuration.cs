using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUN.Config
{
    class Configuration
    {
        private const string CONFIG_LOCATION = @"Config/config.json";
        
        public static Configuration Load()
        {
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(CONFIG_LOCATION));
        }

        public void Save()
        {
            File.WriteAllText(CONFIG_LOCATION, JsonConvert.SerializeObject(this));
        }

        public string UndertaleDataPath { get; set; }
    }
}
