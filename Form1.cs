using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SteamScraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string SteamAppId;

        private void button1_Click(object sender, EventArgs e)
        {
            //Steam URL
            string url = textBox1.Text;
            //Regex to get the appId
            string pattern = @"^.*?\/[^\d]*(\d+)[^\d]*\/.*$";
            string input = @url;
            RegexOptions options = RegexOptions.Singleline | RegexOptions.Multiline;
            Match m = Regex.Match(input, pattern, options);
            SteamAppId = m.Groups[1].Value;
            if (SteamScraper.game.Title != null || SteamScraper.game.Publisher !=null || SteamScraper.game.Developer != null || SteamScraper.game.Notes != null || SteamScraper.game.Genres != null)
            {
                DialogResult dialogResult = MessageBox.Show("This game has some Metadata already, Do you want to replace it?", "Steam Downloader", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SteamApi.SteamSearch(SteamAppId);
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
