using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace SteamScraper
{
    class SteamScraper : IGameMenuItemPlugin 
    {
        public static IGame game { get; set; }

        public bool SupportsMultipleGames
        {
            get { return true; }
        }

        public string Caption
        {
            get { return "Steam Metadata Downloader"; }
        }

        public System.Drawing.Image IconImage
        {
            get { return Resource1.steam; }
        }

        public bool ShowInLaunchBox
        {
            get { return true; }
        }

        public bool ShowInBigBox
        {
            get { return false; }
        }

        public static object Configs { get; private set; }


        public bool GetIsValidForGame(IGame selectedGame)
        {
            return !string.IsNullOrEmpty(selectedGame.Platform);
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return selectedGames.Any(g => !string.IsNullOrEmpty(g.Platform));
        }

        public void OnSelected(IGame selectedGame)
        {
            string input = selectedGame.ApplicationPath;
            string steamPattern = @"steam://rungameid/";
            game = selectedGame;
            if (Regex.IsMatch(input, steamPattern))
            {
                Regex rx = new Regex(@"(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(input);
                _ = SteamApi.SteamSearchAsync(matches[0].Value.ToString());
            }
            else
            {
                Form1 frm = new Form1();
                frm.Show();
            }
        }

        public void OnSelected(IGame[] selectedGames)
        {
            foreach (var steamGame in selectedGames)
            {
                string input = steamGame.ApplicationPath;
                string steamPattern = @"steam://rungameid/";
                game = steamGame;
                if (Regex.IsMatch(input, steamPattern))
                {
                    Regex rx = new Regex(@"(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection matches = rx.Matches(input);
                    _ = SteamApi.SteamSearchAsync(matches[0].Value.ToString());
                }
            }
        }
    }
}