using System.Runtime.CompilerServices;

// GenerateAssemblyInfo is disabled for this project, so the MSBuild
// <InternalsVisibleTo> item would not emit anything. Declare the attribute in
// source instead so the test harness can reach the internal SteamApi /
// SteamScraper types.
[assembly: InternalsVisibleTo("SteamScraper.TestHarness")]
