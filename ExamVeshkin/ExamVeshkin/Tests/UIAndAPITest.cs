using Aquality.Selenium.Browsers;
using ExamVeshkin.Constants;
using ExamVeshkin.Models;
using ExamVeshkin.Forms;
using ExamVeshkin.Utilities;
using OpenQA.Selenium;
using NUnit.Framework.Internal;

namespace ExamVeshkin.Tests
{
    public class UIAndAPITest: BaseTest
    {
        private readonly string _startUrl = $"{ConfigManager.Scheme}" +
                                   $"{ConfigManager.UserName}:{ConfigManager.Password}@" +
                                   $"{ConfigManager.HostAndPort}{ConfigManager.WebPath}";
        private readonly string _testName = nameof(UIAndAPITest);

        [Test(Description = "ChekingBaseFunctionalityOfAPIandUI")]
        public void ChekingBaseFunctionalityOfAPIandUI()
        {
            string methodName = nameof(ChekingBaseFunctionalityOfAPIandUI);

            string? token = Api.GetToken();
            Assert.That(token, Is.Not.Null,
                "The token has NOT been generated");

            AqualityServices.Browser.GoTo(_startUrl);
            AqualityServices.Browser.Maximize();
            Assert.That(HomePage.State.IsDisplayed, Is.True,
                "Projects page has not opened.");
            AqualityServices.Browser
                .Driver.Manage().Cookies.AddCookie(new Cookie(CookieName.TOKEN, token));
            AqualityServices.Browser.Refresh();
            Assert.That(HomePage.footer.GetVariantNumber(), Is.EqualTo(ConfigManager.Variant),
                "Invalid variant number in the footer.");

            HomePage.WaitAndClickProjectButton(ConfigManager.ProjectName);
            List<TestRecord>? testRecordsApi = Api.TestGetJson();
            Assert.That(testRecordsApi, Is.Not.Null, 
                "Unexpected data format.");
            List<TestRecord> testRecordsUI = ProjectPage.table.GetTestRecords();
            List<TestRecord> testRecordsUIExpected = testRecordsUI
                .OrderByDescending(x => DateTime.Parse(x.StartTime)).ToList();
            Assert.That(testRecordsUI.SequenceEqual(testRecordsUIExpected), Is.True,
                "Tests on the first page are NOT sorted by date descending.");
            CollectionAssert.IsSubsetOf(testRecordsUI, testRecordsApi,
                "The tests on the first page do NOT match those returned by the API request.");

            AqualityServices.Browser.GoBack();
            HomePage.panelHeading.ClickAdd();
            string mainTab = AqualityServices.Browser.Tabs().CurrentTabHandle;
            AqualityServices.Browser.Tabs().SwitchToLastTab();
            string nameOfNewProject = RandomUtils.GenerateString();
            AddProjectPage.ClearAndTypeProjectName(nameOfNewProject);
            AddProjectPage.WaitAndClickSubmit();
            Assert.That(AddProjectPage.IsProjectSaved(), Is.True,
                "After saving the project, the message about successful saving has NOT appeared.");
            AqualityServices.Browser.Tabs().CloseTab();
            AqualityServices.Browser.Tabs().SwitchToTab(mainTab);
            AqualityServices.Browser.Refresh();
            Assert.That(HomePage.IsProjectExist(nameOfNewProject), Is.True,
                "After refreshing the page, the project did NOT appear in the list");

            HomePage.WaitAndClickProjectButton(nameOfNewProject);
            string? newTestRecordId = Api.CreateTestRecord(Sid,nameOfNewProject, _testName, methodName);
            Api.SendTestLog(newTestRecordId);
            string screenshot = AqualityServices.Browser.Driver.GetScreenshot().AsBase64EncodedString;
            Api.AttachPictureToTest(newTestRecordId, screenshot);
            Assert.That(AqualityServices.ConditionalWait.WaitFor(ProjectPage.table.IsTableNotEmpty), Is.True,
                "Test has not rendered without refreshing the page");
        }
    }
}