using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System;

namespace ParsingUtils.JSON
{
    public static class Parsing
    {
        public static JObject JSONParse(string fileName)
        {
            JObject o2;
            // read SON directly from a file
            StreamReader file = File.OpenText(String.Format(fileName));
            
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                o2 = (JObject)JToken.ReadFrom(reader);
            }

            return o2;
        }
    }
    
}