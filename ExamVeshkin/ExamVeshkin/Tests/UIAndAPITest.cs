using Aquality.Selenium.Browsers;
using ExamVeshkin.Constants;
using ExamVeshkin.Models;
using ExamVeshkin.Forms;
using ExamVeshkin.Utilities;
using OpenQA.Selenium;

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
            string methodName = nameof(ChekingBaseFunctionalityOfAPIandUI); //уточнить

            string? token = Api.GetToken();
            Assert.That(token, Is.Not.Null, 
                "Токен НЕ был сгенерирован");

            AqualityServices.Browser.GoTo(_startUrl);
            AqualityServices.Browser.Maximize();
            Assert.That(HomePage.State.IsDisplayed, Is.True, 
                "Страница проектов не открылась.");
            AqualityServices.Browser
                .Driver.Manage().Cookies.AddCookie(new Cookie(CookieName.TOKEN, token));
            AqualityServices.Browser.Refresh();
            Assert.That(HomePage.footer.GetVariantNumber(), Is.EqualTo(ConfigManager.Variant),
                "В футере указан НЕ верный номер варианта");

            //HomePage.WaitAndClickProjectButton(ConfigManager.ProjectName);
            //List<TestRecord>? testRecordsApi = Api.TestGetJson();
            //List<TestRecord> testRecordsUI = ProjectPage.table.GetTestRecords();
            //List<TestRecord> testRecordsUIExpected = testRecordsUI
            //    .OrderByDescending(x => DateTime.Parse(x.StartTime)).ToList();
            //Assert.That(testRecordsUI.SequenceEqual(testRecordsUIExpected), Is.True, 
            //    "Тесты, находящиеся на первой странице НЕ отсортированы по убыванию даты.");
            //Assert.That(testRecordsUI.All(record => testRecordsApi.Contains(record)), Is.True, 
            //    "Тесты, находящиеся на первой странице НЕ соответствуют тем, которые вернул запрос к апи");

            //AqualityServices.Browser.GoBack();
            HomePage.panelHeading.ClickAdd();
            string mainTab = AqualityServices.Browser.Tabs().CurrentTabHandle;
            string nameOfNewProject = RandomUtils.GenerateString();
            AqualityServices.Browser.Tabs().SwitchToLastTab();
            AddProjectPage.ClearAndTypeProjectName(nameOfNewProject);
            AddProjectPage.WaitAndClickSubmit();
            Assert.That(AddProjectPage.IsProjectSaved(), Is.True, 
                "После сохранения проекта НЕ появилось сообщение об успешном сохранении.");
            AqualityServices.Browser.Tabs().CloseTab();
            AqualityServices.Browser.Tabs().SwitchToTab(mainTab);
            AqualityServices.Browser.Refresh();
            Assert.That(HomePage.IsProjectExist(nameOfNewProject), Is.True, 
                "После обновления страницы проект НЕ появился в списке");

            HomePage.WaitAndClickProjectButton(nameOfNewProject);
            string newTestRecord = Api.AddNewTest(Sid,nameOfNewProject, _testName, methodName);
        }
    }
}