using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Unbroken.LaunchBox.Plugins.Data;

namespace SteamScraperTestHarness
{
    /// <summary>
    /// Builds a Moq-backed <see cref="IGame"/> whose additional applications and
    /// custom fields are stored in real in-memory lists. That way the scraper's
    /// add/remove calls behave realistically and can be inspected afterwards.
    /// </summary>
    internal static class FakeGame
    {
        public static Mock<IGame> Create(string platform, string applicationPath)
        {
            var additionalApps = new List<IAdditionalApplication>();
            var customFields = new List<ICustomField>();

            var game = new Mock<IGame>();
            game.SetupAllProperties();

            // Additional applications ---------------------------------------
            game.Setup(g => g.GetAllAdditionalApplications())
                .Returns(() => additionalApps.ToArray());

            game.Setup(g => g.AddNewAdditionalApplication())
                .Returns(() =>
                {
                    var app = new Mock<IAdditionalApplication>();
                    app.SetupAllProperties();
                    additionalApps.Add(app.Object);
                    return app.Object;
                });

            game.Setup(g => g.TryRemoveAdditionalApplication(It.IsAny<IAdditionalApplication>()))
                .Returns((IAdditionalApplication a) => additionalApps.Remove(a));

            // Custom fields --------------------------------------------------
            game.Setup(g => g.GetAllCustomFields())
                .Returns(() => customFields.ToArray());

            game.Setup(g => g.AddNewCustomField())
                .Returns(() =>
                {
                    var field = new Mock<ICustomField>();
                    field.SetupAllProperties();
                    customFields.Add(field.Object);
                    return field.Object;
                });

            game.Setup(g => g.TryRemoveCustomField(It.IsAny<ICustomField>()))
                .Returns((ICustomField f) => customFields.Remove(f));

            // Seed the properties the scraper reads before it writes anything.
            var obj = game.Object;
            obj.Platform = platform;
            obj.ApplicationPath = applicationPath;
            obj.Title = string.Empty;
            obj.Notes = string.Empty;
            obj.Developer = string.Empty;
            obj.Publisher = string.Empty;
            obj.GenresString = string.Empty;

            return game;
        }
    }
}
