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
        public static string sDesc;
        public static string banner;
        public static string developer;
        public static string publisher;
        public static string genreListFinal;
        public static string movie;
        public static string releaseDate;
        private static string plataforma;
        private static string gameTitle;
        private static JArray screenShots;
        private static JArray genres;

        public static void SteamSearch(string appId)
        {
            //SSL
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var webClient = new System.Net.WebClient())
            {
                string gameUrl = "https://store.steampowered.com/api/appdetails?appids=" + appId;
                var json = webClient.DownloadString(gameUrl);

                JObject jsonContent = JObject.Parse(json);
                //Serialization
                name = (string)jsonContent[appId]["data"]["name"];
                sDesc = (string)jsonContent[appId]["data"]["short_description"];
                banner = (string)jsonContent[appId]["data"]["header_image"];
                developer = (string)jsonContent[appId]["data"]["developers"][0];
                publisher = (string)jsonContent[appId]["data"]["publishers"][0];
                genres = (JArray)jsonContent[appId]["data"]["genres"];
                screenShots = (JArray)jsonContent[appId]["data"]["screenshots"];
                movie = (string)jsonContent[appId]["data"]["movies"][0]["webm"]["480"];
                //releaseDate = (string)jsonContent[appId]["data"]["release_date"]["date"];
            }

            //string format = @"dd MMM, yyyy";

            //DateTime releaseDateFinal = DateTime.ParseExact(releaseDate, format,
            //    CultureInfo.InvariantCulture);

            //Set Data
            SteamScraper.game.Title = name;
            SteamScraper.game.Notes = sDesc;
            SteamScraper.game.Developer = developer;
            SteamScraper.game.Publisher = publisher;
            SteamScraper.game.GenresString = genreListFinal;
            //SteamScraper.game.ReleaseDate = releaseDateFinal.Date;

            //GenreList
            List<string> genreList = new List<string>();
            foreach (var oneGenre in genres)
            {
                genreList.Add(oneGenre["description"].ToString());
            }
            genreListFinal = string.Join(";", genreList);

            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            plataforma = SteamScraper.game.Platform;
            gameTitle = SteamScraper.game.Title;
            string destMovies = Path.Combine(path, "Videos", plataforma);
            string destImages = Path.Combine(path, "Images", plataforma);
            //Download Trailer
            downloadFile(movie, destMovies + @"\" + CleanFileName(gameTitle) + ".mp4");
            //Download Banner
            string banner_path = destImages + @"\" + "\\Steam Banner\\" + CleanFileName(gameTitle) + ".jpg";
            downloadFile(banner, banner_path);

            var count = 1;
            //List of Screenshots
            foreach (var oneSS in screenShots)
            {
                downloadFile(oneSS["path_thumbnail"].ToString(), destImages + @"\" + "\\Screenshot - Gameplay\\" + CleanFileName(gameTitle) + "-" + count.ToString("D2") + ".jpg");
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
