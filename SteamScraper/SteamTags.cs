using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SteamScraper
{
    class SteamTags
    {
        public static JToken SteamTag(string appId)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons
            var jsonUrl = "https://steamspy.com/api.php?request=appdetails&appid=" + appId;
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(jsonUrl);
                JObject jsonContent = JObject.Parse(json);
                //Serialization
                return jsonContent["tags"];
            }
        }
    }
}
