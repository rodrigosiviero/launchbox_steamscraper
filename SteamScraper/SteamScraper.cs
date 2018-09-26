using System;
using System.Linq;
using System.Net;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace SteamScraper
{
    class SteamScraper : IGameMenuItemPlugin 
    {
        public static IGame game { get; set; }

        public bool SupportsMultipleGames
        {
            get { return false; }
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
            game = selectedGame;
            Form1 frm = new Form1();
            frm.Show();
        }

        public void OnSelected(IGame[] selectedGames)
        {
            throw new NotImplementedException();
        }
    }
}