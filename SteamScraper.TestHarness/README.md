# SteamScraper.TestHarness

A small console app that lets you run the SteamScraper LaunchBox plugin's
runtime **directly in Visual Studio**, without installing the plugin into
LaunchBox or launching LaunchBox at all.

## What it does

The plugin's real work happens in `SteamScraper.SteamApi.SteamSearchAsync(appId)`,
which talks to the live Steam store API, downloads media, and writes metadata
back onto a LaunchBox `IGame`. This harness:

1. Creates a **fake `IGame`** and **fake `IDataManager`** using [Moq]
   (LaunchBox's `IGame` has 86 properties + 34 methods, so mocking beats
   hand-writing a stub).
2. Injects them into the plugin's static entry points
   (`PluginHelper.DataManager` and `SteamScraper.game`).
3. Calls the real `SteamApi.SteamSearchAsync` against the live Steam API.
4. Prints every field the scraper wrote back (title, release date, developer,
   publisher, genres, notes, the "Visit Steam Database page" additional
   application, and the optional `Tags` custom field).

Access to the plugin's `internal` types is granted via
`<InternalsVisibleTo Include="SteamScraper.TestHarness" />` in
`SteamScraper/SteamScraper.csproj`.

## Running it

Set **SteamScraper.TestHarness** as the startup project and press F5, or from
a terminal in the repo root:

```powershell
dotnet run --project SteamScraper.TestHarness -- 440
```

### Arguments

| Argument              | Description                                             | Default          |
| --------------------- | ------------------------------------------------------- | ---------------- |
| `[appId]`             | Steam appId to scrape (digits are extracted from input) | `440` (Team Fortress 2) |
| `--tags`              | Enable the SteamSpy "Tags" custom-field code path       | off              |
| `--platform "<name>"` | Platform name assigned to the fake game                 | `PC (Windows)`   |

Examples:

```powershell
dotnet run --project SteamScraper.TestHarness -- 620            # Portal 2
dotnet run --project SteamScraper.TestHarness -- 292030 --tags  # The Witcher 3, with tags
```

## Where downloaded media goes

Just like inside LaunchBox, the scraper saves images/videos relative to the
running executable. When run from the harness they land under the harness
`bin` output directory in `Images/<platform>/...` and `Videos/<platform>/...`.
The harness prints those paths at the end.

## Notes

- A live internet connection is required (the harness hits the real Steam and
  SteamSpy APIs).
- The harness writes a `properties.json` next to the plugin DLL to control the
  `--tags` behaviour; this mirrors how the plugin reads its config at runtime.

[Moq]: https://github.com/devlooped/moq
