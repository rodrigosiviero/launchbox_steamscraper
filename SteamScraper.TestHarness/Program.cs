using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Moq;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace SteamScraperTestHarness
{
    /// <summary>
    /// Console harness that drives the SteamScraper plugin's runtime
    /// (<c>SteamApi.SteamSearchAsync</c>) without needing LaunchBox running.
    ///
    /// It injects a Moq-backed <see cref="IGame"/> and <see cref="IDataManager"/>,
    /// runs the real scraping/download logic against the live Steam API, then
    /// prints everything the scraper wrote back onto the game.
    ///
    /// Usage:
    ///   SteamScraper.TestHarness [appId] [--tags] [--platform "PC (Windows)"]
    ///   Defaults: appId = 440 (Team Fortress 2), tags off, platform "PC (Windows)".
    /// </summary>
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            string appId = "440";
            string platform = "PC (Windows)";
            bool enableTags = false;

            for (int i = 0; i < args.Length; i++)
            {
                string a = args[i];
                if (a == "--tags")
                {
                    enableTags = true;
                }
                else if (a == "--platform" && i + 1 < args.Length)
                {
                    platform = args[++i];
                }
                else if (!a.StartsWith("--"))
                {
                    // First positional argument is the Steam appId.
                    string digits = new string(a.Where(char.IsDigit).ToArray());
                    if (!string.IsNullOrEmpty(digits))
                    {
                        appId = digits;
                    }
                }
            }

            Console.WriteLine("=== SteamScraper Test Harness ===");
            Console.WriteLine($"appId    : {appId}");
            Console.WriteLine($"platform : {platform}");
            Console.WriteLine($"tags     : {enableTags}");
            Console.WriteLine();

            // The scraper reads properties.json from next to the SteamScraper
            // assembly. Make sure one exists and reflects the --tags flag.
            WritePropertiesJson(enableTags);

            // Provide a fake IDataManager so PluginHelper.DataManager.Save() works.
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(d => d.Save(It.IsAny<bool>()))
                       .Callback(() => Console.WriteLine("[IDataManager.Save called]"));
            PluginHelper.DataManager = dataManager.Object;

            // Provide a fake IGame and hand it to the plugin's static slot.
            var game = FakeGame.Create(platform, $"steam://rungameid/{appId}");
            global::SteamScraper.SteamScraper.game = game.Object;

            try
            {
                Console.WriteLine("Running SteamApi.SteamSearchAsync ...\n");
                await global::SteamScraper.SteamApi.SteamSearchAsync(appId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n!!! Scraper threw an exception:");
                Console.WriteLine(ex);
                return 1;
            }

            PrintResult(game.Object);
            return 0;
        }

        private static void WritePropertiesJson(bool enableTags)
        {
            string dllDir = Path.GetDirectoryName(
                typeof(global::SteamScraper.SteamApi).Assembly.Location);
            string path = Path.Combine(dllDir, "properties.json");
            string value = enableTags ? "true" : "false";
            File.WriteAllText(path, "{\r\n  \"customFields\": \"" + value + "\"\r\n}");
            Console.WriteLine($"Wrote {path} (customFields={value})");
        }

        private static void PrintResult(IGame game)
        {
            Console.WriteLine();
            Console.WriteLine("=== Metadata written to the game ===");
            Console.WriteLine($"Title       : {game.Title}");
            Console.WriteLine($"ReleaseDate : {game.ReleaseDate}");
            Console.WriteLine($"Developer   : {game.Developer}");
            Console.WriteLine($"Publisher   : {game.Publisher}");
            Console.WriteLine($"Genres      : {game.GenresString}");
            Console.WriteLine($"Notes       : {Truncate(game.Notes, 200)}");

            Console.WriteLine();
            Console.WriteLine("Additional applications:");
            foreach (var app in game.GetAllAdditionalApplications())
            {
                Console.WriteLine($"  - {app.Name} => {app.ApplicationPath}");
            }

            Console.WriteLine();
            Console.WriteLine("Custom fields:");
            foreach (var field in game.GetAllCustomFields())
            {
                Console.WriteLine($"  - {field.Name} = {Truncate(field.Value, 200)}");
            }

            string outputDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Console.WriteLine();
            Console.WriteLine("Downloaded media (if any) was written under:");
            Console.WriteLine($"  {Path.Combine(outputDir, "Images")}");
            Console.WriteLine($"  {Path.Combine(outputDir, "Videos")}");
        }

        private static string Truncate(string value, int max)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= max ? value : value.Substring(0, max) + "...";
        }
    }
}
