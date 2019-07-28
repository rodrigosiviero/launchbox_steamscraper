using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Unbroken.LaunchBox.Plugins;

namespace SteamScraper
{
    class SteamApp
    {
        private string _appName;
        private string _apptype;
        private string _publishers;
        private string _developer;
        private string _genreListFinal;
        private string _releaseDate;
        private string _headerImage;
        private string _backgroundImage;
        private string _shortDescription;
        private string _websiteUrl;
        
        public string AppId { get; set; }
        public string AppName
        {
            get { return _appName; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _appName = value;
                else if (!string.IsNullOrEmpty(value) && _appName == "NOT_SET_PLACEHOLDER")
                {
                    _appName = RemoveSymbols(value);
                }
            }
        }
        public string ShortDescription
        {
            get { return _shortDescription; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _shortDescription = value;
                else if (!string.IsNullOrEmpty(value) && _shortDescription == "NOT_SET_PLACEHOLDER")
                {
                    _shortDescription = RemoveHtmlTags(value);
                    _shortDescription = RemoveSymbols(ShortDescription);
                }
            }
        }
        public string Source { get; set; }
        public string ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _releaseDate = value;
                else if (!string.IsNullOrEmpty(value) && _releaseDate == "NOT_SET_PLACEHOLDER")
                    _releaseDate = value;
            }
        }
        public string Apptype
        {
            get { return _apptype; }
            set
            {
                if (value == "Unknown")
                    _apptype = value;
                else if (!string.IsNullOrEmpty(value) && _apptype == "Unknown")
                    _apptype = FirstLetterToUpper(value);
            }
        }
        public string Publishers
        {
            get { return _publishers; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _publishers = value;
                else if (!string.IsNullOrEmpty(value) && _publishers == "NOT_SET_PLACEHOLDER")
                    _publishers = value;
            }
        }
        public string Developer
        {
            get { return _developer; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _developer = value;
                else if (!string.IsNullOrEmpty(value) && _developer == "NOT_SET_PLACEHOLDER")
                    _developer = value;
            }
        }
        public string GenreListFinal
        {
            get { return _genreListFinal; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _genreListFinal = value;
                else if (!string.IsNullOrEmpty(value) && value != ";" && _genreListFinal == "NOT_SET_PLACEHOLDER")
                    _genreListFinal = value;
            }
        }
        public string HeaderImage
        {
            get { return _headerImage; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _headerImage = value;
                else if (CheckUri(value) == "image/jpeg" && _headerImage == "NOT_SET_PLACEHOLDER")
                    _headerImage = value;
            }
        }
        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _backgroundImage = value;
                else if (CheckUri(value) == "image/jpeg" && _backgroundImage == "NOT_SET_PLACEHOLDER")
                    _backgroundImage = value;
            }
        }
        public string WebsiteUrl
        {
            get { return _websiteUrl; }
            set
            {
                if (value == "NOT_SET_PLACEHOLDER")
                    _websiteUrl = value;
                else if (!string.IsNullOrEmpty(value) && _websiteUrl == "NOT_SET_PLACEHOLDER")
                    _websiteUrl = value;
            }
        }
        public string Movie { get; set; }
        public string MovieQuality { get; set; }
        public string MetacriticScore { get; set; }
        public double TotalVotes { get; set; }
        public double PositiveVotes { get; set; }
        public double ReviewScore { get; set; }
        public double FinalScore { get; set; }
        public string SteamCategory { get; set; }
        public string Platform { get; set; }
        public bool MultiPlayerMode { get; set; }
        public bool CoopMode { get; set; }

        public DateTime? DateTime { get; set; }
        public JArray Screenshots { get; set; }

        private static List<string> PlayModeList { get; set; }



        public SteamApp (string pId)
        {
            //Set Default Values
            AppId = pId;
            AppName = "NOT_SET_PLACEHOLDER";
            ShortDescription = "NOT_SET_PLACEHOLDER";
            Source = "Steam";
            ReleaseDate = "NOT_SET_PLACEHOLDER";
            Apptype = "Unknown";
            Publishers = "NOT_SET_PLACEHOLDER";
            Developer = "NOT_SET_PLACEHOLDER";
            GenreListFinal = "NOT_SET_PLACEHOLDER";
            TotalVotes = 0;
            PositiveVotes = 0;
            HeaderImage = "NOT_SET_PLACEHOLDER";
            BackgroundImage = "NOT_SET_PLACEHOLDER";
            WebsiteUrl = "NOT_SET_PLACEHOLDER";
            Movie = null;
            MovieQuality = "max";
            Screenshots = null;
            MultiPlayerMode = false;
            CoopMode = false;
            PlayModeList = new List<string>();
            Platform = SteamScraper.Game.Platform;
        }


        public void SaveAppData()
        {
            try
            {
                //Set Game Title
                if (!string.IsNullOrEmpty(AppName) && AppName != "NOT_SET_PLACEHOLDER")
                    SteamScraper.Game.Title = AppName;
                else
                {
                    SteamScraper.Game.Source = "Steam (No Info Found)";
                    SteamScraper.Game.Status = "Unknown";
                    PluginHelper.DataManager.Save();
                    return;
                }

                //Set Notes
                if (!string.IsNullOrEmpty(ShortDescription) && ShortDescription != "NOT_SET_PLACEHOLDER")
                    SteamScraper.Game.Notes = ShortDescription;
                else
                    SteamScraper.Game.Notes = "";
                //Set Source
                if (!string.IsNullOrEmpty(Source))
                    SteamScraper.Game.Source = Source;
                //Set ReleaseDate
                const string format = @"d MMM, yyyy";
                try
                {
                    if (ReleaseDate.Length < 11)
                    {
                        DateTime = null;
                        SteamScraper.Game.ReleaseDate = DateTime;
                    }
                    else
                    {
                        DateTime = System.DateTime.ParseExact(ReleaseDate, format,
                            CultureInfo.InvariantCulture);
                        SteamScraper.Game.ReleaseDate = DateTime.Value.Date;
                    }
                }
                catch (Exception)
                {
                    DateTime = null;
                    SteamScraper.Game.ReleaseDate = DateTime;
                }

                //Set AppType
                if (!string.IsNullOrEmpty(Apptype))
                    SteamScraper.Game.Status = Apptype;
                //Set Publisher
                if (!string.IsNullOrEmpty(Publishers) && Publishers != "NOT_SET_PLACEHOLDER")
                    SteamScraper.Game.Publisher = Publishers;
                //Set Developer
                if (!string.IsNullOrEmpty(Developer) && Developer != "NOT_SET_PLACEHOLDER")
                    SteamScraper.Game.Developer = Developer;
                //Set Genres
                if (!string.IsNullOrEmpty(GenreListFinal) && GenreListFinal != "NOT_SET_PLACEHOLDER")
                    SteamScraper.Game.GenresString = GenreListFinal;
                //Set PlayMode
                CleanPlayMode();
                if (PlayModeList != null)
                    SteamScraper.Game.PlayMode = string.Join("; ", PlayModeList);
                SetRating();



                //Download Media
                var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
                var destMovies = Path.Combine(path ?? throw new InvalidOperationException(), "Videos", Platform);
                var destImages = Path.Combine(path, "Images", Platform);
                
                //Download Banner
                if (!string.IsNullOrEmpty(HeaderImage) && HeaderImage != "NOT_SET_PLACEHOLDER" &&
                    Properties.Settings.Default.DownloadSteamBanner)
                {
                    var bannerPath = destImages + @"\" + @"\Steam Banner\" + CleanFileName(AppName) + ".jpg";
                    DownloadFile(HeaderImage, bannerPath);
                }

                //Download Background
                if (!string.IsNullOrEmpty(BackgroundImage) && BackgroundImage != "NOT_SET_PLACEHOLDER" &&
                    Properties.Settings.Default.DownloadBackgroundImage)
                {
                    var backgroundPath = destImages + @"\" + @"\Fanart - Background\" + CleanFileName(AppName) + ".jpg";
                    DownloadFile(BackgroundImage, backgroundPath);
                }

                //Download Trailer
                if (Properties.Settings.Default.VideoQuality != "None" && (!string.IsNullOrEmpty(Movie)))
                {
                    if (Properties.Settings.Default.VideoOnlyGames)
                    {
                        if (Apptype == "Game")
                            DownloadFile(Movie, destMovies + @"\" + CleanFileName(AppName) + ".webm");
                    }
                    else
                        DownloadFile(Movie, destMovies + @"\" + CleanFileName(AppName) + ".webm");
                }

                //Download Screenshots
                if (Screenshots != null && Properties.Settings.Default.DownloadScreenshots)
                {
                    var count = 1;
                    foreach (var oneSs in Screenshots)
                    {
                        if (CheckUri(oneSs["path_full"].ToString()) == "image/jpeg")
                            DownloadFile(oneSs["path_full"].ToString(), destImages + @"\" + @"\Screenshot - Gameplay\" + CleanFileName(AppName) + "-" + count.ToString("D2") + ".jpg");
                        count++;
                    }
                }

                //Workaround for the new Steam Images
                const string boxCover = "library_600x900_2x.jpg";
                const string marquee = "library_hero.jpg";
                const string clearLogo = "logo.png";
                if (CheckUri("http://steamcdn-a.akamaihd.net/steam/apps/" + AppId + "/" + clearLogo) == "image/png" && Properties.Settings.Default.DownloadClearLogo)
                {
                    var url = "http://steamcdn-a.akamaihd.net/steam/apps/" + AppId + "/" + clearLogo;
                    DownloadFile(url, destImages + @"\" + @"\Clear Logo\" + CleanFileName(AppName) + ".png");
                }

                if (CheckUri("http://steamcdn-a.akamaihd.net/steam/apps/" + AppId + "/" + boxCover) == "image/jpeg" && Properties.Settings.Default.DownloadBoxImage)
                {
                    var url = "http://steamcdn-a.akamaihd.net/steam/apps/" + AppId + "/" + boxCover;
                    DownloadFile(url, destImages + @"\" + @"\Box - Front\" + CleanFileName(AppName) + ".jpg");
                }

                if (CheckUri("http://steamcdn-a.akamaihd.net/steam/apps/" + AppId + "/" + marquee) == "image/jpeg" && Properties.Settings.Default.DownloadMarquee)
                {
                    var url = "http://steamcdn-a.akamaihd.net/steam/apps/" + AppId + "/" + marquee;
                    DownloadFile(url, destImages + @"\" + @"\Arcade - Marquee\" + CleanFileName(AppName) + ".jpg");
                }

                
                // Add Links As Additional Apps
                AddLinkSteamDb();
                AddLinkSteamSpy();
                AddLinkPcGamingWiki();
                AddLinkSteamStore();
                AddLinkOfficialWebsite();

                // Save Data
                PluginHelper.DataManager.Save();
            }
            catch (Exception exSave)
            {
                MessageBox.Show("Game Save: " + AppId + " - " + AppName + Environment.NewLine + exSave.Message);
            }
        }


        private void AddLinkSteamDb()
        {
            //Additional Applications
            foreach (var addApp in SteamScraper.Game.GetAllAdditionalApplications())
            {
                if (addApp.Name == "View on Steam Database")
                {
                    SteamScraper.Game.TryRemoveAdditionalApplication(addApp);
                }
            }

            if (Properties.Settings.Default.Link_SteamDB)
            {
                var additionalApplication = SteamScraper.Game.AddNewAdditionalApplication();
                additionalApplication.Name = "View on Steam Database";
                additionalApplication.ApplicationPath = "https://steamdb.info/app/" + AppId + "/graphs/";
            }
        }
        

        private void AddLinkSteamSpy()
        {
            //Additional Applications
            foreach (var addApp in SteamScraper.Game.GetAllAdditionalApplications())
            {
                if (addApp.Name == "View on Steam Spy")
                {
                    SteamScraper.Game.TryRemoveAdditionalApplication(addApp);
                }
            }

            if (Properties.Settings.Default.Link_SteamSpy)
            {
                var additionalApplication = SteamScraper.Game.AddNewAdditionalApplication();
                additionalApplication.Name = "View on Steam Spy";
                additionalApplication.ApplicationPath = "https://steamspy.com/app/" + AppId;
            }
        }


        private void AddLinkPcGamingWiki()
        {
            //Additional Applications
            foreach (var addApp in SteamScraper.Game.GetAllAdditionalApplications())
            {
                if (addApp.Name == "View PCGamingWiki Article")
                {
                    SteamScraper.Game.TryRemoveAdditionalApplication(addApp);
                }
            }

            if (Properties.Settings.Default.Link_Wiki)
            {
                var additionalApplication = SteamScraper.Game.AddNewAdditionalApplication();
                additionalApplication.Name = "View PCGamingWiki Article";
                additionalApplication.ApplicationPath = "http://pcgamingwiki.com/api/appid.php?appid=" + AppId;
            }
        }


        private void AddLinkSteamStore()
        {
            //Additional Applications
            foreach (var addApp in SteamScraper.Game.GetAllAdditionalApplications())
            {
                if (addApp.Name == "Visit Steam Store page")
                {
                    SteamScraper.Game.TryRemoveAdditionalApplication(addApp);
                }
            }

            if (Properties.Settings.Default.Link_SteamStore)
            {
                var additionalApplication = SteamScraper.Game.AddNewAdditionalApplication();
                additionalApplication.Name = "Visit Steam Store page";
                additionalApplication.ApplicationPath = "https://store.steampowered.com/app/" + AppId;
            }
        }

        
        private void AddLinkOfficialWebsite()
        {
            if (!string.IsNullOrEmpty(WebsiteUrl) && WebsiteUrl != "NOT_SET_PLACEHOLDER")
            {
                //Additional Applications
                foreach (var addApp in SteamScraper.Game.GetAllAdditionalApplications())
                {
                    if (addApp.Name == "Visit Official Website")
                    {
                        SteamScraper.Game.TryRemoveAdditionalApplication(addApp);
                    }
                }

                if (Properties.Settings.Default.Link_Official)
                {
                    var additionalApplication = SteamScraper.Game.AddNewAdditionalApplication();
                    additionalApplication.Name = "Visit Official Website";
                    additionalApplication.ApplicationPath = WebsiteUrl;
                }
            }
        }


        private static string RemoveSymbols(string pStr)
        {
            // Remove Trademark and Copyright symbols
            var charsToRemove = new[] { "™", "®" };
            foreach (var c in charsToRemove)
            {
                pStr = pStr.Replace(c, string.Empty);
            }
            return pStr;
        }


        private static string RemoveHtmlTags(string description)
        {
            if (description != null)
            {
                const string htmlTags = @"<[^>].+?>";
                const string quote = @"&quot;";
                const string amp = @"&amp;";
                var temp = Regex.Replace(description, htmlTags, String.Empty);
                temp = Regex.Replace(temp, quote, "\"");
                temp = Regex.Replace(temp, amp, "&");
                return temp;
            }
            else
                return description;
        }


        private static string FirstLetterToUpper(string str)
        {
            // The AppType that the Steam API delivers is all lower case which doesn't look good inside LaunchBox 
            if (str == "dlc")
                str = "DLC";
            else if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }


        private static string CleanFileName(string fileName)
        {
            if (fileName != null)
            {
                var cleanTemp = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), "_"));
                cleanTemp = cleanTemp.Replace("'", "_");
                cleanTemp = cleanTemp.Replace("___", "_");
                return cleanTemp.Replace("__", "_");
            }
            else
                return fileName;
        }


        private void SetRating()
        {
            // Remove User Score Custom Field in case it exists
            var oldtags = SteamScraper.Game.GetAllCustomFields();
            foreach (var field in oldtags)
            {
                if (field.Name == "User Score")
                    SteamScraper.Game.TryRemoveCustomField(field);
            }
            
            // Add User Score or Metacritic Score as Star Rating
            if (Properties.Settings.Default.StarRating == "UserRating")
            {
                if (TotalVotes >= 50 && PositiveVotes != 0)
                {
                    ReviewScore = PositiveVotes / TotalVotes;
                    FinalScore = ReviewScore - (ReviewScore - 0.5) * Math.Pow(2, -Math.Log10(TotalVotes + 1));
                    SteamScraper.Game.StarRatingFloat = Convert.ToSingle(FinalScore * 5);
                }
                else if (TotalVotes >= 50 && FinalScore != 0)
                    SteamScraper.Game.StarRatingFloat = Convert.ToSingle(FinalScore);
            }
            else if (Properties.Settings.Default.StarRating == "Metacritic" && !string.IsNullOrEmpty(MetacriticScore))
                SteamScraper.Game.StarRatingFloat = Convert.ToSingle(MetacriticScore) / 20;

            // Add User Score as Custom Field
            else if (Properties.Settings.Default.CF_UserRating)
            {
                if (TotalVotes >= 50 && PositiveVotes != 0)
                {
                    ReviewScore = PositiveVotes / TotalVotes;
                    FinalScore = ReviewScore - (ReviewScore - 0.5) * Math.Pow(2, -Math.Log10(TotalVotes + 1));
                    var tags = SteamScraper.Game.AddNewCustomField();
                    tags.Name = "User Score";
                    tags.Value = Math.Round((FinalScore * 100), 2).ToString(CultureInfo.InvariantCulture);
                }
                else if (TotalVotes >= 50 && FinalScore != 0)
                {
                    var tags = SteamScraper.Game.AddNewCustomField();
                    tags.Name = "User Score";
                    tags.Value = Math.Round((FinalScore * 20), 2).ToString(CultureInfo.InvariantCulture);
                }
            }
        }


        public void SetMetacritic()
        {
            // Add Metacritic Score as Custom Field
            var oldtags = SteamScraper.Game.GetAllCustomFields();
            foreach (var field in oldtags)
            {
                if (field.Name == "Metacritic")
                {
                    SteamScraper.Game.TryRemoveCustomField(field);
                }
            }
            if (Properties.Settings.Default.CF_Metacritic)
            {
                var tags = SteamScraper.Game.AddNewCustomField();
                tags.Name = "Metacritic";
                tags.Value = MetacriticScore;
            }
        }


        public void SetSteamGenres(JArray genres)
        {
            // Merge all genres into a single string
            var genreList = new List<string>();
            foreach (var oneGenre in genres)
            {
                if (oneGenre != null)
                    genreList.Add(oneGenre["description"].ToString());
            }
            GenreListFinal = string.Join("; ", genreList);
        }


        public void AddTags (JToken pToken, string pSource)
        {
            try
            {
                var oldfields = SteamScraper.Game.GetAllCustomFields();
                var fieldname = "Tags";

                foreach (var tag in pToken)
                {
                    var tagValue = "";
                    switch (pSource)
                    {
                        //Determine needed Jtype based on source
                        case "SteamSpy":
                        {
                            var jConvert = tag.ToObject<JProperty>();
                            tagValue = jConvert.Name;
                            break;
                        }

                        case "Depressurizer":
                        {
                            var jConvert = tag.ToObject<JValue>();
                            tagValue = jConvert.ToString(CultureInfo.InvariantCulture);
                            break;
                        }
                    }

                    foreach (var field in oldfields)
                    {
                        if (field.Name == "Tags")
                            if (field.Value == tagValue)
                                SteamScraper.Game.TryRemoveCustomField(field);
                    }
                    // Separate Play Mode tags into their own data field
                    switch (tagValue)
                    {
                        case "Singleplayer":
                            AddPlayMode("Single-Player");
                            break;
                        case "Multiplayer":
                            AddPlayMode("Multi-Player");
                            break;
                        case "Massively Multiplayer":
                            MultiPlayerMode = true;
                            AddPlayMode("MMO");
                            break;
                        case "Local Co-Op":
                            CoopMode = true;
                            AddPlayMode("Co-op (Local)");
                            break;
                        case "Local Multiplayer":
                            MultiPlayerMode = true;
                            AddPlayMode("Multi-Player (Local)");
                            break;
                        case "Co-op":
                            AddPlayMode("Co-op");
                            break;
                        case "Online Co-Op":
                            CoopMode = true;
                            AddPlayMode("Co-op (Online)");
                            break;
                        case "Asynchronous Multiplayer":
                            MultiPlayerMode = true;
                            AddPlayMode("Multi-Player (Asynchronous)");
                            break;
                        case "Split Screen":
                            MultiPlayerMode = true;
                            AddPlayMode("Shared/Split Screen");
                            break;
                        case "4 Player Local":
                            MultiPlayerMode = true;
                            AddPlayMode("4 Player Local");
                            break;

                        default:
                            if (Properties.Settings.Default.CF_Tags)
                            {
                                var tags = SteamScraper.Game.AddNewCustomField();
                                tags.Name = fieldname;
                                tags.Value = tagValue;
                            }
                            break;
                    }
                }
            }
            catch (Exception exTags)
            {
                MessageBox.Show("AddTags: " + AppId + " - " + AppName + Environment.NewLine + exTags.Message);
            }
        }


        public void SetPlayModes(JArray categories)
        {
            try
            {
                var oldtags = SteamScraper.Game.GetAllCustomFields();
                foreach (var category in categories)
                {
                    SteamCategory = category["description"].ToString();
                    foreach (var field in oldtags)
                    {
                        if (field.Name != "Flags") continue;
                        if (field.Value == SteamCategory)
                            SteamScraper.Game.TryRemoveCustomField(field);
                    }

                    switch (SteamCategory)
                    {
                        // Separate Play Mode categories into their own data field
                        case "Single-player":
                            AddPlayMode("Single-Player");
                            break;
                        case "Multi-player":
                            AddPlayMode("Multi-Player");
                            break;
                        case "Online Multi-Player":
                            AddPlayMode("Multi-Player (Online)");
                            MultiPlayerMode = true;
                            break;
                        case "Local Multi-Player":
                            AddPlayMode("Multi-Player (Local)");
                            MultiPlayerMode = true;
                            break;
                        case "Cross-Platform Multiplayer":
                            AddPlayMode("Multi-Player (Cross-Platform)");
                            MultiPlayerMode = true;
                            break;
                        case "MMO":
                            AddPlayMode(SteamCategory);
                            MultiPlayerMode = true;
                            break;
                        case "Co-op":
                            AddPlayMode("Co-op");
                            break;
                        case "Online Co-op":
                            AddPlayMode("Co-op (Online)");
                            CoopMode = true;
                            break;
                        case "Local Co-op":
                            AddPlayMode("Co-op (Local)");
                            CoopMode = true;
                            break;
                        case "Shared/Split Screen":
                            AddPlayMode(SteamCategory);
                            MultiPlayerMode = true;
                            break;
                        default:
                        {
                            if (!string.IsNullOrEmpty(SteamCategory) && Properties.Settings.Default.CF_Flags)
                            {
                                var tags = SteamScraper.Game.AddNewCustomField();
                                tags.Name = "Flags";
                                tags.Value = SteamCategory;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception exPlayModeSteam)
            {
                MessageBox.Show("SetPlayModesSteam: " + AppId + " - " + AppName + Environment.NewLine + exPlayModeSteam.Message);
            }
        }


        public void SetPlayModes(JToken categories)
        {
            try
            {
                var oldtags = SteamScraper.Game.GetAllCustomFields();
                foreach (var category in categories)
                {
                    var jValue = category.ToObject<JValue>();
                    SteamCategory = jValue.ToString(CultureInfo.InvariantCulture);
                    foreach (var field in oldtags)
                    {
                        if (field.Name != "Flags") continue;
                        if (field.Value == SteamCategory)
                            SteamScraper.Game.TryRemoveCustomField(field);
                    }

                    switch (SteamCategory)
                    {
                        // Separate Play Mode categories into their own data field
                        case "Single-player":
                            AddPlayMode("Single-Player");
                            break;
                        case "Multi-player":
                            AddPlayMode("Multi-Player");
                            break;
                        case "Online Multi-Player":
                            AddPlayMode("Multi-Player (Online)");
                            MultiPlayerMode = true;
                            break;
                        case "Local Multi-Player":
                            AddPlayMode("Multi-Player (Local)");
                            MultiPlayerMode = true;
                            break;
                        case "Cross-Platform Multiplayer":
                            AddPlayMode("Multi-Player (Cross-Platform)");
                            MultiPlayerMode = true;
                            break;
                        case "MMO":
                            AddPlayMode(SteamCategory);
                            MultiPlayerMode = true;
                            break;
                        case "Co-op":
                            AddPlayMode("Co-op");
                            break;
                        case "Online Co-op":
                            AddPlayMode("Co-op (Online)");
                            CoopMode = true;
                            break;
                        case "Local Co-op":
                            AddPlayMode("Co-op (Local)");
                            CoopMode = true;
                            break;
                        case "Shared/Split Screen":
                            AddPlayMode(SteamCategory);
                            MultiPlayerMode = true;
                            break;
                        default:
                        {
                            if (!string.IsNullOrEmpty(SteamCategory) && Properties.Settings.Default.CF_Flags)
                            {
                                var tags = SteamScraper.Game.AddNewCustomField();
                                tags.Name = "Flags";
                                tags.Value = SteamCategory;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception exPlayModeDepressurizer)
            {
                MessageBox.Show("SetPlayModesDepressurizer: " + AppId + " - " + AppName + Environment.NewLine + exPlayModeDepressurizer.Message);
            }
        }


        public void AddPlayMode(string pMode)
        {
            if (!PlayModeList.Contains(pMode) && !string.IsNullOrEmpty(pMode))
                PlayModeList.Add(pMode);
        }


        private void CleanPlayMode()
        {
            try
            {
                // Search if redundant Play Mode values exist and remove them
                for (var i = 0; i < PlayModeList.Count; i++)
                {
                    if (PlayModeList[i].Equals("Multi-Player") && MultiPlayerMode)
                    {
                        PlayModeList.RemoveAt(i);
                        i--;
                    }
                    else if (PlayModeList[i].Equals("Co-op") && CoopMode)
                    {
                        PlayModeList.RemoveAt(i);
                        i--;
                    }
                }
                PlayModeList.Sort();
                // Shift Single-Player to the front of the Play Mode data field
                if (PlayModeList.Contains("Single-Player"))
                {
                    PlayModeList.Remove("Single-Player");
                    PlayModeList.Insert(0, "Single-Player");
                }
            }
            catch (Exception exCleanPlayMode)
            {
                MessageBox.Show("CleanPlayModes: " + AppId + " - " + AppName + Environment.NewLine + exCleanPlayMode.Message);
            }
        }


        public string MergeValuesIntoString(JToken pToken)
        {
            // Merge all genres into a single string
            try
            {
                if (pToken != null)
                {
                    var tempList = new List<string>();
                    foreach (var entry in pToken)
                    {
                        var tempJValue = entry.ToObject<JValue>();
                        tempList.Add(tempJValue.Value.ToString());
                    }
                    var mergedstring = string.Join("; ", tempList);
                    return mergedstring;
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        

        public void AddHltbTimes(int hltbTime, string hltbCategory)
        {
            try
            {
                if (Properties.Settings.Default.CF_Hltb)
                {
                    // Convert Minutes to Hours to be more in line with the Hltb-Scraper plugin that already exists for LaunchBox
                    var hours = " Hours";
                    if (hltbTime >= 60)
                        hltbTime /= 60;
                    else if (hltbTime > 0 && hltbTime < 60)
                        hltbTime = 1;

                    if (hltbTime == 1)
                        hours = " Hour";

                    // Add Hltb time as Custom Field
                    if (hltbTime == 0) return;
                    var oldhltb = SteamScraper.Game.GetAllCustomFields();
                    foreach (var field in oldhltb)
                    {
                        if (field.Name == hltbCategory)
                            SteamScraper.Game.TryRemoveCustomField(field);
                    }

                    var hltb = SteamScraper.Game.AddNewCustomField();
                    hltb.Name = hltbCategory;
                    hltb.Value = hltbTime.ToString("0000") + hours;
                }
            }
            catch (Exception exHltb)
            {
                MessageBox.Show("Add Hltb Times: " + AppId + " - " + AppName + Environment.NewLine + exHltb.Message);
            }
        }


        private static string CheckUri(string pUrl)
        {
            if (!string.IsNullOrEmpty(pUrl))
            {
                // Delay requests to prevent timeouts from server
                System.Threading.Thread.Sleep(100);

                var urlCheck = new Uri(pUrl);
                var request = (HttpWebRequest)WebRequest.Create(urlCheck);
                // Check online status of requested file
                try
                {
                    if (request.GetResponse() is HttpWebResponse response)
                    {
                        var contentType = response.ContentType;
                        response.Close();
                        return contentType;
                    }
                    return "False";
                }
                catch (Exception)
                {
                    return "False";
                }
            }
            else
                return "False";
        }


        private void DownloadFile(string url, string dest)
        {
            try
            {
                // Delay requests to prevent timeouts from server
                System.Threading.Thread.Sleep(100);

                using (var wc = new WebClient())
                {
                    wc.DownloadFileAsync(
                        // Param1 = Link of file
                        new Uri(url),
                        // Param2 = Path to save
                        dest
                    );
                }
            }
            catch (Exception exDownload)
            {
                MessageBox.Show("Download File: " + AppId + " - " + AppName + Environment.NewLine + exDownload.Message);
            }
        }
    }
}
