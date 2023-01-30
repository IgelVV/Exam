using Aquality.Selenium.Browsers;
using SeleniumExtras.WaitHelpers;

namespace ExamVeshkin.Tests
{
    public class UIAndAPITest: BaseTest
    {
        [Test]
        public void Test1()
        {
            string? token = Api.GetToken();
            // спросить на что проверять
            Assert.That(token, Is.Not.Null, "Token hasn't been generated");

            AqualityServices.Browser.GoTo("http://login:password@localhost:8080/web/");
            AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("token", token));
        }
    }
}