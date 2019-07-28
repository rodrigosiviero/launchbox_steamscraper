using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins.Data;

namespace SteamScraper
{
    public partial class Form1 : Form
    {
        private SteamApp _newApp;
        private SteamAppList _allapps;
        private SteamDepressurizerDb _localDb;
        public IGame SingleGame { get; set; }
        public IGame[] MultipleGames { get; set; }
        public string SteamAppId { get; set; }


        public Form1(IGame pGame, IGame[] pGames)
        {
            SingleGame = pGame;
            MultipleGames = pGames;
            InitializeComponent();
            if (SingleGame == null)
            {
                TbManualGame.Enabled = false;
                TbManualGame.Text = "Only available when you scrape a single game";
            }
            else
                TbManualGame.Text = SingleGame.ApplicationPath+@"/";

            LoadSettings();
        }


        public void LoadSettings()
        {
            cbSteamBanner.Checked= Properties.Settings.Default.DownloadSteamBanner;
            cbScreenshots.Checked = Properties.Settings.Default.DownloadScreenshots;
            cbBackground.Checked = Properties.Settings.Default.DownloadBackgroundImage;
            cbBox.Checked = Properties.Settings.Default.DownloadBoxImage;
            cbClearLogo.Checked = Properties.Settings.Default.DownloadClearLogo;
            cbMarquee.Checked = Properties.Settings.Default.DownloadMarquee;
            cbVideoOnlyForGames.Checked = Properties.Settings.Default.VideoOnlyGames;
            if (Properties.Settings.Default.VideoQuality == "None")
                cbVideoOnlyForGames.Enabled = false;
            else
                cbVideoOnlyForGames.Enabled = true;

            if (Properties.Settings.Default.VideoQuality == "max")
                rbMax.Checked = true;
            else if (Properties.Settings.Default.VideoQuality == "480")
                rb480p.Checked = true;
            else
                rbNoVideo.Checked = true;

            if (Properties.Settings.Default.StarRating == "UserRating")
            {
                rbSteamRating.Checked = true;
                cbUserRatingCustomField.Enabled = false;
                cbUserRatingCustomField.Checked = false;
            }
            else if (Properties.Settings.Default.StarRating == "Metacritic")
            {
                rbMetacriticScore.Checked = true;
                cbMetacriticCustomField.Enabled = false;
                cbMetacriticCustomField.Checked = false;
            }
            else
            {
                rbNoStarRating.Checked = true;
                cbMetacriticCustomField.Enabled = true;
                cbUserRatingCustomField.Enabled = true;
            }

            cbFlags.Checked = Properties.Settings.Default.CF_Flags;
            cbSteamTags.Checked = Properties.Settings.Default.CF_Tags;
            cbHltb.Checked = Properties.Settings.Default.CF_Hltb;

            if (cbMetacriticCustomField.Enabled)
                cbMetacriticCustomField.Checked = Properties.Settings.Default.CF_Metacritic;
            else
                cbMetacriticCustomField.Checked = false;

            if (cbUserRatingCustomField.Enabled)
                cbUserRatingCustomField.Checked = Properties.Settings.Default.CF_UserRating;
            else
                cbUserRatingCustomField.Checked = false;

            cbLinkSteamDB.Checked = Properties.Settings.Default.Link_SteamDB;
            cbLinkSteamSpy.Checked = Properties.Settings.Default.Link_SteamSpy;
            cbLinkSteamStore.Checked = Properties.Settings.Default.Link_SteamStore;
            cbLinkPCwiki.Checked = Properties.Settings.Default.Link_Wiki;
            cbLinkOfficialWebsite.Checked = Properties.Settings.Default.Link_Official;
        }


        public void GetSettings()
        {
            Properties.Settings.Default.DownloadSteamBanner = cbSteamBanner.Checked;
            Properties.Settings.Default.DownloadScreenshots = cbScreenshots.Checked;
            Properties.Settings.Default.DownloadBackgroundImage = cbBackground.Checked;
            Properties.Settings.Default.DownloadBoxImage = cbBox.Checked;
            Properties.Settings.Default.DownloadClearLogo = cbClearLogo.Checked;
            Properties.Settings.Default.DownloadMarquee = cbMarquee.Checked;

            if (cbVideoOnlyForGames.Enabled)
                Properties.Settings.Default.VideoOnlyGames = cbVideoOnlyForGames.Checked;
            else
                Properties.Settings.Default.VideoOnlyGames = false;

            if (rbMax.Checked)
                Properties.Settings.Default.VideoQuality = "max";
            else if (rb480p.Checked)
                Properties.Settings.Default.VideoQuality = "480";
            else
                Properties.Settings.Default.VideoQuality = "None";
            
            if (rbSteamRating.Checked)
                Properties.Settings.Default.StarRating = "UserRating";
            else if (rbMetacriticScore.Checked)
                Properties.Settings.Default.StarRating = "Metacritic";
            else
                Properties.Settings.Default.StarRating = "None";

            Properties.Settings.Default.CF_Flags = cbFlags.Checked;
            Properties.Settings.Default.CF_Tags = cbSteamTags.Checked;
            Properties.Settings.Default.CF_Hltb = cbHltb.Checked;

            if (cbMetacriticCustomField.Enabled)
                Properties.Settings.Default.CF_Metacritic = cbMetacriticCustomField.Checked;
            else
                Properties.Settings.Default.CF_Metacritic = false;

            if (cbUserRatingCustomField.Enabled)
                Properties.Settings.Default.CF_UserRating = cbUserRatingCustomField.Checked;
            else
                Properties.Settings.Default.CF_UserRating = false;
            
            Properties.Settings.Default.Link_SteamDB = cbLinkSteamDB.Checked;
            Properties.Settings.Default.Link_SteamSpy = cbLinkSteamSpy.Checked;
            Properties.Settings.Default.Link_SteamStore = cbLinkSteamStore.Checked;
            Properties.Settings.Default.Link_Wiki = cbLinkPCwiki.Checked;
            Properties.Settings.Default.Link_Official = cbLinkOfficialWebsite.Checked;

            //Save currently selected settings
            Properties.Settings.Default.Save();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                GetSettings();

                PlDownload.Visible = true;
                LbDownload.Visible = true;

                // Instance the big databases as own objects so that they don't have to be reloaded for every SingleGame.
                _allapps = new SteamAppList();
                _localDb = new SteamDepressurizerDb();


                if (SingleGame == null)
                {
                    foreach (var steamGame in MultipleGames)
                    {
                        if (steamGame != null)
                        {
                            var input = steamGame.ApplicationPath;
                            const string steamPattern = @"steam://rungameid/";
                            SteamScraper.Game = steamGame;
                            if (Regex.IsMatch(input, steamPattern))
                            {
                                var rx = new Regex(@"(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                var matches = rx.Matches(input);
                                _newApp = new SteamApp(matches[0].Value);
                                SteamApi.SteamSearch(_newApp);
                                SteamSpyApi.SteamSpySearch(_newApp);
                                _localDb.SteamDepressurizerSearch(_newApp);

                                // Searching in the Steam AppList is very time consuming therefore it will be only used as last resort of all other sources fail
                                if (string.IsNullOrEmpty(_newApp.AppName) || _newApp.AppName == "NOT_SET_PLACEHOLDER")
                                    _allapps.ConvertIDtoName(_newApp);
                                _newApp.SaveAppData();
                            }
                        }
                    }

                    MessageBox.Show("Download process is finished!");
                    Close();
                }
                else
                {
                    SteamScraper.Game = SingleGame;
                    //Steam URL
                    var url = TbManualGame.Text;
                    //Regex to get the appId
                    var pattern = @"^.*?\/[^\d]*(\d+)[^\d]*\/.*$";
                    var input = url;
                    RegexOptions options = RegexOptions.Singleline | RegexOptions.Multiline;
                    Match m = Regex.Match(input, pattern, options);

                    if (!m.Success)
                    {
                        MessageBox.Show("Please paste the entire Steam URL, couldn't find the appId!");
                    }
                    else
                    {
                        SteamAppId = m.Groups[1].Value;
                        if (SteamScraper.Game.GetVideoPath() != null || SteamScraper.Game.Publisher != "" ||
                            SteamScraper.Game.Developer != "" || SteamScraper.Game.Notes != "" ||
                            SteamScraper.Game.GenresString != "")
                        {
                            var dialogResult =
                                MessageBox.Show("This game has some metadata already, do you want to replace it?",
                                    "Steam Downloader", MessageBoxButtons.YesNo);
                            switch (dialogResult)
                            {
                                case DialogResult.Yes:
                                    SteamScraper.Game = SingleGame;
                                    _newApp = new SteamApp(SteamAppId);
                                    SteamApi.SteamSearch(_newApp);
                                    SteamSpyApi.SteamSpySearch(_newApp);
                                    _localDb.SteamDepressurizerSearch(_newApp);
                                    // Searching in the Steam AppList is very time consuming therefore it will be only used as last resort of all other sources fail
                                    if (string.IsNullOrEmpty(_newApp.AppName) || _newApp.AppName == "NOT_SET_PLACEHOLDER")
                                        _allapps.ConvertIDtoName(_newApp);
                                    _newApp.SaveAppData();
                                    MessageBox.Show("Download process is finished!");
                                    Close();
                                    break;
                                case DialogResult.No:
                                    Close();
                                    break;
                                case DialogResult.None:
                                    break;
                                case DialogResult.OK:
                                    break;
                                case DialogResult.Cancel:
                                    break;
                                case DialogResult.Abort:
                                    break;
                                case DialogResult.Retry:
                                    break;
                                case DialogResult.Ignore:
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else
                        {
                            SteamScraper.Game = SingleGame;
                            _newApp = new SteamApp(SteamAppId);
                            SteamApi.SteamSearch(_newApp);
                            SteamSpyApi.SteamSpySearch(_newApp);
                            _localDb.SteamDepressurizerSearch(_newApp);
                            // Searching in the Steam AppList is very time consuming therefore it will be only used as last resort of all other sources fail
                            if (string.IsNullOrEmpty(_newApp.AppName) || _newApp.AppName == "NOT_SET_PLACEHOLDER")
                                _allapps.ConvertIDtoName(_newApp);
                            _newApp.SaveAppData();
                            MessageBox.Show("Download process is finished!");
                            Close();
                        }

                        Close();
                    }
                }
            }
            catch (Exception eDownload)
            {
                MessageBox.Show("Scraping Failed: " + SteamScraper.Game.Title + " - " + _newApp.AppName + Environment.NewLine + eDownload.Message);
            }
        }

        private void BtDefault_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DownloadSteamBanner = true;
            Properties.Settings.Default.DownloadScreenshots = true;
            Properties.Settings.Default.DownloadBackgroundImage = true;
            Properties.Settings.Default.DownloadBoxImage = true;
            Properties.Settings.Default.DownloadClearLogo = true;
            Properties.Settings.Default.DownloadMarquee = true;
            Properties.Settings.Default.VideoQuality = "max";
            Properties.Settings.Default.VideoOnlyGames = true;
            Properties.Settings.Default.StarRating = "UserRating";
            Properties.Settings.Default.CF_Flags = true;
            Properties.Settings.Default.CF_Tags = true;
            Properties.Settings.Default.CF_Hltb = true;
            Properties.Settings.Default.CF_Metacritic = true;
            Properties.Settings.Default.CF_UserRating = false;
            Properties.Settings.Default.Link_SteamDB = true;
            Properties.Settings.Default.Link_SteamSpy = false;
            Properties.Settings.Default.Link_SteamStore = false;
            Properties.Settings.Default.Link_Wiki = false;
            Properties.Settings.Default.Link_Official = false;

            Properties.Settings.Default.Save();
            LoadSettings();
        }


        private void RbNoVideo_Click(object sender, EventArgs e)
        {
            cbVideoOnlyForGames.Enabled = false;
        }

        private void Rb480p_Click(object sender, EventArgs e)
        {
            cbVideoOnlyForGames.Enabled = true;
        }

        private void RbMax_Click(object sender, EventArgs e)
        {
            cbVideoOnlyForGames.Enabled = true;
        }

        private void RbNoStarRating_Click(object sender, EventArgs e)
        {
            cbMetacriticCustomField.Enabled = true;
            cbUserRatingCustomField.Enabled = true;
        }

        private void RbMetacriticScore_Click(object sender, EventArgs e)
        {
            cbMetacriticCustomField.Checked = false;
            cbMetacriticCustomField.Enabled = false;
            cbUserRatingCustomField.Enabled = true;
        }

        private void RbSteamRating_Click(object sender, EventArgs e)
        {
            cbMetacriticCustomField.Enabled = true;
            cbUserRatingCustomField.Enabled = false;
            cbUserRatingCustomField.Checked = false;
        }

        private void Button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            GetSettings();
            Close();
        }
    }
}
