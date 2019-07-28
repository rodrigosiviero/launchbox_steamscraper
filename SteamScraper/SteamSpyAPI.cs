using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Windows.Forms;

namespace SteamScraper
{
    class SteamSpyApi
    {
        public static void SteamSpySearch(SteamApp sa)
        {
            // Delay requests to prevent timeouts from server
            System.Threading.Thread.Sleep(200);
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons
                var jsonUrl = "https://steamspy.com/api.php?request=appdetails&appid=" + sa.AppId;
                using (var webClient = new WebClient())
                {
                    var json = webClient.DownloadString(jsonUrl);
                    var jsonContent = JObject.Parse(json);

                    //Serialization
                    if (jsonContent.ContainsKey(propertyName: "name"))
                        sa.AppName = (string)jsonContent["name"];
                    if (jsonContent.ContainsKey(propertyName: "developer"))
                        sa.Developer = (string)jsonContent["developer"];
                    if (jsonContent.ContainsKey(propertyName: "publisher"))
                        sa.Publishers = (string)jsonContent["publisher"];
                    if (jsonContent.ContainsKey(propertyName: "genre"))
                    {
                        sa.GenreListFinal = (string)jsonContent["genre"];
                        sa.GenreListFinal = sa.GenreListFinal?.Replace(',', ';');
                    }
                    
                    if (jsonContent.ContainsKey(propertyName: "tags"))
                        sa.AddTags(jsonContent["tags"], "SteamSpy");

                    if (jsonContent.ContainsKey(propertyName: "positive") && jsonContent.ContainsKey(propertyName: "negative"))
                    {
                        sa.TotalVotes = (double)jsonContent["positive"] + (double)jsonContent["negative"];
                        sa.PositiveVotes = (double)jsonContent["positive"];
                    }
                }
            }
            catch (Exception exSpy)
            {
                MessageBox.Show("SteamSpy: "+ sa.AppId + " - " + sa.AppName + Environment.NewLine + exSpy.Message);
            }
        }
    }
}
