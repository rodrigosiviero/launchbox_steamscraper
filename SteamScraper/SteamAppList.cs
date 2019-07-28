using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Windows.Forms;

namespace SteamScraper
{
    class SteamAppList
    {
        public JToken AllIDs { get; set; }

        private string _searchId;
        private int _count;

        public SteamAppList()
        {
            try
            {
                //SSL
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var webClient = new WebClient())
                {
                    const string allGamesUrl = "https://api.steampowered.com/ISteamApps/GetAppList/v2/";
                    var json = webClient.DownloadString(allGamesUrl);
                    var jsonContent = JObject.Parse(json);
                    //Serialization
                    AllIDs = jsonContent["applist"]["apps"];
                    _searchId = "";
                    _count = 0;
                }
            }
            catch (Exception exList)
            {
                MessageBox.Show(exList.Message);
            }
        }

        public void ConvertIDtoName (SteamApp sa)
        {
            try
            {
                // Search for AppID in the AppList (which is horribly structured compared to the other sources) and get the app name that belongs to it
                foreach (var dummy in AllIDs)
                {
                    _searchId = (string)AllIDs[_count]["appid"];
                    if (_searchId == sa.AppId)
                    {
                        sa.AppName = (string)AllIDs[_count]["name"];
                        break;
                    }
                    _count++;
                }
                _count = 0;
            }
            catch (Exception exConvertId)
            {
                MessageBox.Show("AllIDs: " + _count + Environment.NewLine + exConvertId.Message);
            }
        }
    }
}
