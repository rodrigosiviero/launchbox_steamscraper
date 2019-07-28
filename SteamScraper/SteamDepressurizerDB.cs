using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SteamScraper
{
    class SteamDepressurizerDb
    {
        private string _tempApptype;
        private readonly JObject _localDb;

        public SteamDepressurizerDb()
        {
            try
            {
                var dllPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var databasepath = Path.Combine(dllPath ?? throw new InvalidOperationException(), "database.json");
                using (var streamReader = new StreamReader(databasepath))
                {
                    var json = streamReader.ReadToEnd();
                    _localDb = JObject.Parse(json);
                }

                //Create a list with all existing AppIDs (for importing and testing them in LaunchBox)
                //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //JToken IDs = _localDB["Games"];
                //foreach (var id in IDs)
                //{
                //    JProperty jProp = id.ToObject<JProperty>();
                //    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "AllSteamIDs.txt"), true))
                //    {
                //        outputFile.WriteLine(jProp.Name.ToString());
                //    }              
                //}
            }
            catch (Exception exDepressurizer)
            {
                MessageBox.Show(exDepressurizer.Message);
            }
        }

        public void SteamDepressurizerSearch(SteamApp sa)
        {
            try
            {
                if (_localDb["Games"][sa.AppId]["Name"] != null)
                    sa.AppName = (string)_localDb["Games"][sa.AppId]["Name"];

                if (!string.IsNullOrEmpty((string)_localDb["Games"][sa.AppId]["AppType"]))
                {
                    if (sa.Apptype != (string)_localDb["Games"][sa.AppId]["AppType"])
                    {
                        sa.Apptype = "Unknown";
                        _tempApptype = (string)_localDb["Games"][sa.AppId]["AppType"];
                        switch (_tempApptype)
                        {
                            case "1":
                                sa.Apptype = "Game";
                                break;
                            case "2":
                                sa.Apptype = "DLC";
                                break;
                            case "3":
                                sa.Apptype = "Demo";
                                break;
                            case "4":
                                sa.Apptype = "Application";
                                break;
                            case "5":
                                sa.Apptype = "Tool";
                                break;
                            case "6":
                                sa.Apptype = "Media";
                                break;
                            case "7":
                                sa.Apptype = "Config";
                                break;
                            case "8":
                                sa.Apptype = "Series";
                                break;
                            case "9":
                                sa.Apptype = "Video";
                                break;
                            case "10":
                                sa.Apptype = "Hardware";
                                break;
                        }
                    }
                }

                sa.GenreListFinal = sa.MergeValuesIntoString(_localDb["Games"][sa.AppId]["Genres"]);
                sa.Developer = sa.MergeValuesIntoString(_localDb["Games"][sa.AppId]["Developers"]);
                sa.Publishers = sa.MergeValuesIntoString(_localDb["Games"][sa.AppId]["Publishers"]);
                
                if (_localDb["Games"][sa.AppId]["Flags"] != null)
                    sa.SetPlayModes(_localDb["Games"][sa.AppId]["Flags"]);

                if (_localDb["Games"][sa.AppId]["HltbMain"] != null)
                    sa.AddHltbTimes((int)_localDb["Games"][sa.AppId]["HltbMain"], "HLTB (Main)");

                if (_localDb["Games"][sa.AppId]["HltbExtras"] != null)
                    sa.AddHltbTimes((int)_localDb["Games"][sa.AppId]["HltbExtras"], "HLTB (Main+Extras)");

                if (_localDb["Games"][sa.AppId]["HltbCompletionist"] != null)
                    sa.AddHltbTimes((int)_localDb["Games"][sa.AppId]["HltbCompletionist"], "HLTB (Completionist)");

                if (_localDb["Games"][sa.AppId]["ReviewTotal"] != null && _localDb["Games"][sa.AppId]["ReviewPositivePercentage"] != null && sa.TotalVotes == 0 && sa.PositiveVotes == 0)
                {
                    sa.TotalVotes = (double)_localDb["Games"][sa.AppId]["ReviewTotal"];
                    sa.FinalScore = (double)_localDb["Games"][sa.AppId]["ReviewPositivePercentage"] / 20;
                }

                if (_localDb["Games"][sa.AppId]["SteamReleaseDate"] != null)
                    sa.ReleaseDate = (string)_localDb["Games"][sa.AppId]["SteamReleaseDate"];

                if (_localDb["Games"][sa.AppId]["Tags"] != null)
                    sa.AddTags(_localDb["Games"][sa.AppId]["Tags"], "Depressurizer");
                
            }
            catch (Exception exSearchDepressurizer)
            {
                MessageBox.Show("Depressurizer: " + sa.AppId + " - " + sa.AppName + Environment.NewLine + exSearchDepressurizer.Message);
            }
        }


     
    }
}
