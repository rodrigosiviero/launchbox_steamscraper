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
            SteamApi.SteamSearch(SteamAppId);
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
