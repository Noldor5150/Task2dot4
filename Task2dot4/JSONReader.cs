using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;


namespace Task2dot4
{
    public static class JSONReader
    {
        public static Monitor GetMonitorFromJsonConfig(string jsonPath)
        {
            return JsonConvert.DeserializeObject<Monitor>(File.ReadAllText(jsonPath));
        }
        public static List<Site> GetSitesFromConfig(string jsonPath)
        {
            var json = File.ReadAllText(jsonPath);
            var rawData = JObject.Parse(json);
            var WindowsList = rawData["SitesList"];

            List<Site> list = new List<Site>();
            foreach (var token in WindowsList)
            {
                list.Add(new Site( (int)token["Interval"], (int)token["MaxResponseTime"], (string)token["Url"], (string)token["AdminEmail"]));

            }
            return list;
        }
    }
}
