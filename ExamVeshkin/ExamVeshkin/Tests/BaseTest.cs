using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using ExamVeshkin.Extensions;

namespace ExamVeshkin.Tests
{
    public abstract class BaseTest
    {
        protected static string ScenarioName
            => TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString()
            ?? TestContext.CurrentContext.Test.Name.Replace("_", string.Empty);

        private static Logger Logger => Logger.Instance;
        private string? _sid;

        protected string Sid => _sid ??= DateTime.Now.ToUnixTimeMilliSeconds();

        [SetUp]
        public void Setup()
        {
            Logger.Info($"Start scenario [{ScenarioName}]");
        }

        [TearDown]
        public void AfterEach()
        {
            Logger.Info($"Finished scenario [{ScenarioName}]");

            if (AqualityServices.IsBrowserStarted)
            {
                AqualityServices.Browser.Quit();
            }
        }
    }
}