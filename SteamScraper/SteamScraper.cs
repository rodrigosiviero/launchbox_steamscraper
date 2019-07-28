using System.Linq;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace SteamScraper
{
    class SteamScraper : IGameMenuItemPlugin
    {
        public static IGame Game { get; set; }

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


        public bool GetIsValidForGame(IGame selectedGame)
        {
            return !string.IsNullOrEmpty(selectedGame.Platform);
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return selectedGames.Any(g => !string.IsNullOrEmpty(g.Platform));
        }

        public IGame SelectedGame { get; set; }
        public IGame[] SelectedGames { get; set; }


        public void OnSelected(IGame pGame)
        {
            SelectedGame = pGame;
            SelectedGames = null;
            var frm = new Form1(SelectedGame, SelectedGames);
            frm.Show();
        }


        public void OnSelected(IGame[] pGames)
        {
            SelectedGame = null;
            SelectedGames = pGames;
            var frm = new Form1(SelectedGame, SelectedGames);
            frm.Show();
        }
    }
}