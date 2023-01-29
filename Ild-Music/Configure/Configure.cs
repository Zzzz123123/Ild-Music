using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ild_Music.Configure
{
    public class Configure : IConfigure
    {
        public string ComponentsFile {get; init;}

        public IEnumerable<string> Players {get; set;}
        public IEnumerable<string> Synches {get; set;}

        public Configure()
        {}

        public Configure(string componentsFile)
        {
            ComponentsFile = componentsFile;
            Parse();
        }

        public void Parse()
        {
            if (File.Exists(ComponentsFile))
            {
                using (StreamReader file = File.OpenText(ComponentsFile))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);
                    Players = (IEnumerable<string>)json["components"]["player"];
                    Synches = (IEnumerable<string>)json["components"]["synch"];
                }
            }
            else
            {
                throw new Exception("Could not find your configuration file!");
            }
        }
    }
}
