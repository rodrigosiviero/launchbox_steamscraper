using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows.Forms;


namespace SteamScraper
{
    class SteamApi
    {
        public static void SteamSearch(SteamApp sa)
        {
            try {
                //SSL
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var webClient = new WebClient())
                {

                    var gameUrl = "https://store.steampowered.com/api/appdetails?cc=UK&appids=" + sa.AppId;
                    var json = webClient.DownloadString(gameUrl);

                    var jsonContent = JObject.Parse(json);
                    //Serialization
                    var success = jsonContent[sa.AppId]["success"];

                    if (success.ToString() != "True")
                    {
                        sa.Source = "Steam (Not Available In Store)";
                        sa.HeaderImage = "http://steamcdn-a.akamaihd.net/steam/apps/" + sa.AppId + "/header.jpg";
                        // Additional delay for SteamSpy API request
                        System.Threading.Thread.Sleep(150);
                        return;
                    }

                    //Get Metadata
                    if (jsonContent[sa.AppId]["data"]["name"] != null)
                        sa.AppName = (string)jsonContent[sa.AppId]["data"]["name"];
                    
                    if (jsonContent[sa.AppId]["data"]["short_description"] != null)
                        sa.ShortDescription = (string)jsonContent[sa.AppId]["data"]["short_description"];

                    if (jsonContent[sa.AppId]["data"]["is_free"] != null)
                        if ((bool)jsonContent[sa.AppId]["data"]["is_free"])
                            sa.Source = "Steam (Free)";

                    if (jsonContent[sa.AppId]["data"]["release_date"]["date"] != null)
                    {
                        if ((bool)jsonContent[sa.AppId]["data"]["release_date"]["coming_soon"] == false)
                            sa.ReleaseDate = (string)jsonContent[sa.AppId]["data"]["release_date"]["date"];
                        else
                            sa.Source = "Steam (Not Released Yet)";
                    }

                    if (jsonContent[sa.AppId]["data"]["type"] != null)
                        sa.Apptype = (string)jsonContent[sa.AppId]["data"]["type"];

                    if (jsonContent[sa.AppId]["data"]["website"] != null)
                        sa.WebsiteUrl = ((string)jsonContent[sa.AppId]["data"]["website"]);
                
                    if (jsonContent[sa.AppId]["data"]["publishers"] != null)
                    {
                        var tempList = new List<string>();
                        var publishersArray = (JArray)jsonContent[sa.AppId]["data"]["publishers"];
                        foreach (var onePublisher in publishersArray)
                        {
                            tempList.Add(onePublisher.ToString());
                        }
                        sa.Publishers = string.Join("; ", tempList);
                    }

                    if (jsonContent[sa.AppId]["data"]["developers"] != null)
                    {
                        var tempList = new List<string>();
                        var developersArray = (JArray)jsonContent[sa.AppId]["data"]["developers"];
                        foreach (var oneDeveloper in developersArray)
                        {
                            tempList.Add(oneDeveloper.ToString());
                        }
                        sa.Developer = string.Join("; ", tempList);
                    }

                    if (jsonContent[sa.AppId]["data"]["genres"] != null)
                        sa.SetSteamGenres((JArray)jsonContent[sa.AppId]["data"]["genres"]);

                    if (jsonContent[sa.AppId]["data"]["categories"] != null)
                        sa.SetPlayModes((JArray)jsonContent[sa.AppId]["data"]["categories"]);

                    if (jsonContent[sa.AppId]["data"]["metacritic"] != null)
                    {
                        sa.MetacriticScore = (string)jsonContent[sa.AppId]["data"]["metacritic"]["score"];
                        sa.SetMetacritic();
                    }


                    //Get Media
                    if (jsonContent[sa.AppId]["data"]["header_image"] != null)
                        sa.HeaderImage = (string)jsonContent[sa.AppId]["data"]["header_image"];
                    sa.HeaderImage = "http://steamcdn-a.akamaihd.net/steam/apps/" + sa.AppId + "/header.jpg"; //Fallback Image
                    if (jsonContent[sa.AppId]["data"]["background"] != null)
                        sa.BackgroundImage = (string)jsonContent[sa.AppId]["data"]["background"];
                    if (jsonContent[sa.AppId]["data"]["movies"] != null && Properties.Settings.Default.VideoQuality != "None")
                        sa.Movie = (string)jsonContent[sa.AppId]["data"]["movies"][0]["webm"][Properties.Settings.Default.VideoQuality];
                    if (jsonContent[sa.AppId]["data"]["screenshots"] != null)
                        sa.Screenshots = (JArray)jsonContent[sa.AppId]["data"]["screenshots"];
                }
            }
            catch (Exception exSteamApi)
            {
                MessageBox.Show("SteamAPI: " + sa.AppId + " - " + sa.AppName + Environment.NewLine + exSteamApi.Message);
            }
        }
    }
}
