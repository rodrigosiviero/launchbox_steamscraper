using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Unbroken.LaunchBox.Plugins.Data;
using Unbroken.LaunchBox.Plugins;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;

namespace SteamScraper
{
    class SteamApi
    {
        public static string name;
        public static string short_description;
        public static string header_image;
        public static string developer;
        public static string publishers;
        public static string genreListFinal;
        public static string movie;
        public static string release_date;
        private static string plataforma;
        private static string gameTitle;
        private static JArray screenshots;
        private static JArray genres;
        private static DateTime? dateTime;

        public static void SteamSearch(string appId)
        {
            //SSL
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var webClient = new System.Net.WebClient())
            {
                string gameUrl = "https://store.steampowered.com/api/appdetails?cc=UK&appids=" + appId;
                var json = webClient.DownloadString(gameUrl);

                JObject jsonContent = JObject.Parse(json);
                //Serialization
                name = (string)jsonContent[appId]["data"]["name"];
                short_description = (string)jsonContent[appId]["data"]["short_description"];
                header_image = (string)jsonContent[appId]["data"]["header_image"];
                screenshots = (JArray)jsonContent[appId]["data"]["screenshots"];
                release_date = (string)jsonContent[appId]["data"]["release_date"]["date"];
                if (jsonContent[appId]["data"]["publishers"] != null)
                {
                    publishers = (string)jsonContent[appId]["data"]["publishers"][0];
                }
                else
                {
                    publishers = null;
                }
                if (jsonContent[appId]["data"]["developers"] != null)
                {
                    developer = (string)jsonContent[appId]["data"]["developers"][0];
                }
                else
                {
                    developer = null;
                }
                if (jsonContent[appId]["data"]["movies"] != null)
                {
                    movie = (string)jsonContent[appId]["data"]["movies"][0]["webm"]["480"];
                }
                else
                {
                    movie = null;
                }
                if (jsonContent[appId]["data"]["genres"] != null)
                {
                    genres = (JArray)jsonContent[appId]["data"]["genres"];
                    //GenreList
                    List<string> genreList = new List<string>();
                    foreach (var oneGenre in genres)
                    {
                        genreList.Add(oneGenre["description"].ToString());
                    }
                    genreListFinal = string.Join(";", genreList);
                }
                else
                {
                    genres = null;
                }
            }
            string format = @"d MMM, yyyy";


            if (release_date.Length < 11)
            {
                dateTime = null;
                SteamScraper.game.ReleaseDate = dateTime;
            }
            else
            {
                dateTime = DateTime.ParseExact(release_date, format,
                                        CultureInfo.InvariantCulture);
                SteamScraper.game.ReleaseDate = dateTime.Value.Date;
            }

            string pattern = @"<[^>].+?>";
            String sDescFinal = Regex.Replace(short_description, pattern, String.Empty);

            //Set Data
            SteamScraper.game.Title = name;
            SteamScraper.game.Notes = sDescFinal;
            SteamScraper.game.Developer = developer;
            SteamScraper.game.Publisher = publishers;
            SteamScraper.game.GenresString = genreListFinal;


            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            plataforma = SteamScraper.game.Platform;
            gameTitle = SteamScraper.game.Title;
            string destMovies = Path.Combine(path, "Videos", plataforma);
            string destImages = Path.Combine(path, "Images", plataforma);
            //Download Trailer
            if (movie != null)
            {
                downloadFile(movie, destMovies + @"\" + CleanFileName(gameTitle) + ".mp4");
            }            
            //Download Banner
            string banner_path = destImages + @"\" + "\\Steam Banner\\" + CleanFileName(gameTitle) + ".jpg";
            downloadFile(header_image, banner_path);

            var count = 1;
            //List of Screenshots
            foreach (var oneSS in screenshots)
            {
                downloadFile(oneSS["path_full"].ToString(), destImages + @"\" + "\\Screenshot - Gameplay\\" + CleanFileName(gameTitle) + "-" + count.ToString("D2") + ".jpg");
                count++;
            }
            //Saves data
            PluginHelper.DataManager.Save();
        }

        public static void downloadFile(string url, string dest)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri(url),
                    // Param2 = Path to save
                    dest
                );
            }
        }

        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), "_"));
        }

    }
}
