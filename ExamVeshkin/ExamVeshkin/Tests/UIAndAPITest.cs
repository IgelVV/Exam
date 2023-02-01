using Aquality.Selenium.Browsers;
using ExamVeshkin.Models;
using ExamVeshkin.Utilities;
using OpenQA.Selenium;
using NUnit.Framework.Internal;
using ExamVeshkin.API;
using ExamVeshkin.Forms;

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
        HomePage homePage = new();
        ProjectPage projectPage = new();
        AddProjectPage addProjectPage = new();

        string methodName = nameof(ChekingBaseFunctionalityOfAPIandUI);

            string? token = API.API.GetToken();
            Assert.That(token, Is.Not.Null,
                "The token has NOT been generated");

            AqualityServices.Browser.GoTo(_startUrl);
            AqualityServices.Browser.Maximize();
            Assert.That(homePage.State.IsDisplayed, Is.True,
                "Projects page has not opened.");
            AqualityServices.Browser
                .Driver.Manage().Cookies.AddCookie(new Cookie(ConfigManager.CookieNameToken, token));
            AqualityServices.Browser.Refresh();
            Assert.That(homePage.footer.GetVariantNumber(), Is.EqualTo(ConfigManager.Variant),
                "Invalid variant number in the footer.");

            homePage.WaitAndClickProjectButton(ConfigManager.ProjectName);
            List<TestRecord>? testRecordsApi = API.API.TestGetJson();
            Assert.That(testRecordsApi, Is.Not.Null, 
                "Unexpected data format.");
            List<TestRecord> testRecordsUI = projectPage.table.GetTestRecords();
            List<TestRecord> testRecordsUIExpected = testRecordsUI
                .OrderByDescending(x => DateTime.Parse(x.StartTime)).ToList();
            Assert.That(testRecordsUI.SequenceEqual(testRecordsUIExpected), Is.True,
                "Tests on the first page are NOT sorted by date descending.");
            CollectionAssert.IsSubsetOf(testRecordsUI, testRecordsApi,
                "The tests on the first page do NOT match those returned by the API request.");

            AqualityServices.Browser.GoBack();
            homePage.panelHeading.ClickAdd();
            string mainTab = AqualityServices.Browser.Tabs().CurrentTabHandle;
            AqualityServices.Browser.Tabs().SwitchToLastTab();
            string nameOfNewProject = RandomUtils.GenerateString();
            addProjectPage.ClearAndTypeProjectName(nameOfNewProject);
            addProjectPage.WaitAndClickSubmit();
            Assert.That(addProjectPage.IsProjectSaved(), Is.True,
                "After saving the project, the message about successful saving has NOT appeared.");
            AqualityServices.Browser.Tabs().CloseTab();
            AqualityServices.Browser.Tabs().SwitchToTab(mainTab);
            AqualityServices.Browser.Refresh();
            Assert.That(homePage.IsProjectExist(nameOfNewProject), Is.True,
                "After refreshing the page, the project did NOT appear in the list");

            homePage.WaitAndClickProjectButton(nameOfNewProject);
            string? newTestRecordId = API.API.CreateTestRecord(Sid,nameOfNewProject, _testName, methodName);
            API.API.SendTestLog(newTestRecordId);
            string screenshot = AqualityServices.Browser.Driver.GetScreenshot().AsBase64EncodedString;
            API.API.AttachPictureToTest(newTestRecordId, screenshot);
            Assert.That(AqualityServices.ConditionalWait.WaitFor(projectPage.table.IsTableNotEmpty), Is.True,
                "Test has not rendered without refreshing the page");
        }
    }
}