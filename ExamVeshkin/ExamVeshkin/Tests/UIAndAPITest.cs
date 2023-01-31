using Aquality.Selenium.Browsers;
using ExamVeshkin.Constants;
using ExamVeshkin.Models;
using ExamVeshkin.Forms;
using ExamVeshkin.Utilities;
using SeleniumExtras.WaitHelpers;

namespace ExamVeshkin.Tests
{
    public class UIAndAPITest: BaseTest
    {
        private readonly string _startUrl = $"{ConfigManager.Scheme}" +
                                   $"{ConfigManager.UserName}:{ConfigManager.Password}@" +
                                   $"{ConfigManager.HostAndPort}{ConfigManager.WebPath}";
        private ProjectPage _projectPage = new(ConfigManager.ProjectName);

        [Test(Description = "ChekingBaseFunctionalityOfAPIandUI")]
        public void ChekingBaseFunctionalityOfAPIandUI()
        {
            string? token = Api.GetToken();
            Assert.That(token, Is.Not.Null, "����� �� ��� ������������");

            AqualityServices.Browser.GoTo(_startUrl);
            Assert.That(HomePage.State.IsDisplayed, Is.True, "�������� �������� �� ���������.");

            AqualityServices.Browser
                .Driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie(CookieName.TOKEN, token));
            AqualityServices.Browser.Refresh();
            Assert.That(HomePage.footer.GetVariantNumber(), Is.EqualTo(ConfigManager.Variant),
                "� ������ ������ �� ������ ����� ��������");

            HomePage.WaitAndClickProjectButton(ConfigManager.ProjectName);
            List<TestRecord>? testRecordsApi = Api.TestGetJson();

            ProjectPage projectPage= new(ConfigManager.ProjectName);
            List<TestRecord> testRecordsUI = projectPage.table.GetTestRecords();
            List<TestRecord> testRecordsUIExpected = testRecordsUI.OrderByDescending(x => DateTime.Parse(x.StartTime)).ToList();
            Assert.That(testRecordsUI.SequenceEqual(testRecordsUIExpected), Is.True, "�����, ����������� �� ������ �������� �� ������������� �� �������� ����.");
            Assert.That(testRecordsUI.All(record => testRecordsApi.Contains(record)), Is.True, "�����, ����������� �� ������ �������� �� ������������� ���, ������� ������ ������ � ���");
        }
    }
}