using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamScraper
{
    class SteamTags
    {
        public static async Task<JToken> SteamTag(string appId)
        {
            var jsonUrl = "https://steamspy.com/api.php?request=appdetails&appid=" + appId;
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync(jsonUrl);
                JObject jsonContent = JObject.Parse(json);
                //Serialization
                return jsonContent["tags"];
            }
        }
    }
}
