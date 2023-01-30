using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using ExamVeshkin.Utilities;
using ExamVeshkin.Forms;

namespace ExamVeshkin.Tests
{
    public abstract class BaseTest
    {
        protected static string ScenarioName
            => TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString()
            ?? TestContext.CurrentContext.Test.Name.Replace("_", string.Empty);

        private static Logger Logger => Logger.Instance;
        private string _startUrl = ConfigManager.HostAndPort + ConfigManager.WebPath;

        private HomePage? _homePage;

        protected HomePage HomePage => _homePage ??= new();

        [SetUp]
        public void Setup()
        {
            Logger.Info($"Start scenario [{ScenarioName}]");
            AqualityServices.Browser.GoTo(_startUrl);
            AqualityServices.Browser.Maximize();
        }

        [TearDown]
        public virtual void AfterEach()
        {
            Logger.Info($"Finished scenario [{ScenarioName}]");

            if (AqualityServices.IsBrowserStarted)
            {
                AqualityServices.Browser.Quit();
            }
        }
    }
}