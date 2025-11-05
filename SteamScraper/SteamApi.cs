using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Unbroken.LaunchBox.Plugins;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Versioning;

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

        public static async Task SteamSearchAsync(string appId)
        {
            using (var httpClient = new HttpClient())
            {
                string gameUrl = "https://store.steampowered.com/api/appdetails?cc=UK&appids=" + appId;
                var json = await httpClient.GetStringAsync(gameUrl);

                JObject jsonContent = JObject.Parse(json);
                //Serialization
                var data = jsonContent[appId]["data"];
                JToken success = jsonContent[appId]["success"];

                if (success.ToString() == "True")
                {
                    Console.WriteLine("Found");
                }
                else
                {
                    SteamDBLink(appId);
                    PluginHelper.DataManager.Save();
                    return;
                }
                name = (string)jsonContent[appId]["data"]["name"];
                short_description = (string)jsonContent[appId]["data"]["short_description"];
                header_image = (string)jsonContent[appId]["data"]["header_image"];
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
                if (jsonContent[appId]["data"]["screenshots"] != null)
                {
                    screenshots = (JArray)jsonContent[appId]["data"]["screenshots"];
                }
                else
                {
                    screenshots = null;
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

            String sDescFinal = removeHtmlTags(short_description);

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
                string moviePath = Path.Combine(destMovies, CleanFileName(gameTitle) + ".webm");
                downloadFile(movie, moviePath);
            }
            
            //Download Banner
            string bannerPath = Path.Combine(destImages, "Steam Banner", CleanFileName(gameTitle) + ".jpg");
            downloadFile(header_image, bannerPath);

            //List of Screenshots
            if (screenshots != null)
            {
                var count = 1;
                foreach (var oneSS in screenshots)
                {
                    string screenshotPath = Path.Combine(destImages, "Steam Screenshot", 
                        CleanFileName(gameTitle) + "-" + count.ToString("D2") + ".jpg");
                    downloadFile(oneSS["path_full"].ToString(), screenshotPath);
                    count++;
                }
            }

            //Workaround for the new Steam Images
            string boxCover = "library_600x900_2x.jpg";
            string marquee = "library_hero.jpg";
            string clearLogo = "logo.png";

            var clearLogoType = await CheckURI(clearLogo, appId);
            if (clearLogoType == "image/png")
            {
                string url = "https://cdn.cloudflare.steamstatic.com/steam/apps/" + appId + "/" + clearLogo;
                string logoPath = Path.Combine(destImages, "Clear Logo", CleanFileName(gameTitle) + ".png");
                downloadFile(url, logoPath);
            }

            var boxCoverType = await CheckURI(boxCover, appId);
            if (boxCoverType == "image/jpeg")
            {
                string url = "https://cdn.cloudflare.steamstatic.com/steam/apps/" + appId + "/" + boxCover;
                string posterPath = Path.Combine(destImages, "Steam Poster", CleanFileName(gameTitle) + ".jpg");
                downloadFile(url, posterPath);
            }

            var marqueeType = await CheckURI(marquee, appId);
            if (marqueeType == "image/jpeg")
            {
                string url = "https://cdn.cloudflare.steamstatic.com/steam/apps/" + appId + "/" + marquee;
                string marqueePath = Path.Combine(destImages, "Steam Banner", CleanFileName(gameTitle) + ".jpg");
                downloadFile(url, marqueePath);
            }

            var dllPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dllPathFinal = Path.Combine(dllPath, "properties.json");
            using (var streamReader = new StreamReader(dllPathFinal))
            {
                string jsonFile = streamReader.ReadToEnd();
                Properties jsonConfig = JsonConvert.DeserializeObject<Properties>(jsonFile);
                if (jsonConfig.customFields == "true")
                {
                    JToken steamSpyTags = await SteamTags.SteamTag(appId);

                    var oldfields = SteamScraper.game.GetAllCustomFields();

                    foreach (var Tag in steamSpyTags)
                    {
                        JProperty jProperty = Tag.ToObject<JProperty>();
                        string propertyName = jProperty.Name;
                        foreach (var field in oldfields)
                        {
                            if (field.Name == "Tags")
                            {
                                if (field.Value == propertyName)
                                {
                                    SteamScraper.game.TryRemoveCustomField(field);
                                }
                            }
                        }
                        var tags = SteamScraper.game.AddNewCustomField();
                        tags.Name = "Tags";
                        tags.Value = propertyName;
                    }
                }
            }
            //Saves data
            SteamDBLink(appId);
            PluginHelper.DataManager.Save();
        }

        public static void downloadFile(string url, string dest)
        {
            try
            {
                // Create directory if it doesn't exist
                string directory = Path.GetDirectoryName(dest);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    var response = client.GetAsync(url).Result;
                    
                    // Only proceed if successful
                    if (response.IsSuccessStatusCode)
                    {
                        var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                        File.WriteAllBytes(dest, fileBytes);
                        Console.WriteLine($"Successfully downloaded: {Path.GetFileName(dest)}");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to download (HTTP {response.StatusCode}): {url}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error downloading {url}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading {url}: {ex.Message}");
            }
        }

        public static async Task<string> CheckURI(string type, string appId)
        {
            string url = "https://cdn.cloudflare.steamstatic.com/steam/apps/" + appId + "/" + type;
            using (HttpClient client = new())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return response.Content.Headers.ContentType.MediaType;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }

        public static string removeHtmlTags(string description)
        {
            string htmlTags = @"<[^>].+?>";
            string quote = @"&quot;";
            string amp = @"&amp;";
            var temp = Regex.Replace(description, htmlTags, String.Empty);
            temp = Regex.Replace(temp, quote,"\"");
            temp = Regex.Replace(temp, amp, "&");
            return temp;
        }

        public static string CleanFileName(string fileName)
        {
            var cleanTemp = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), "_"));
            return cleanTemp.Replace("'", "_");
        }

        [SupportedOSPlatform("windows7.0")]
        public static void SteamDBLink(string appId)
        {
            //Additional Applications
            foreach (var addApp in SteamScraper.game.GetAllAdditionalApplications())
            {
                if (addApp.Name == "Visit Steam Database page")
                {
                    _ = SteamScraper.game.TryRemoveAdditionalApplication(addApp);
                }
            }
            var additionalApplication = SteamScraper.game.AddNewAdditionalApplication();
            additionalApplication.Name = "Visit Steam Database page";
            additionalApplication.ApplicationPath = "https://steamdb.info/app/" + appId + "/";
        }
    }
}
