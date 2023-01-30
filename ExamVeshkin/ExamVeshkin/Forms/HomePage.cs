using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using ExamVeshkin.Utilities;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class HomePage : Form
    {
        public HomePage() : base(By.XPath("//div[contains(@class, 'list-group')]"), nameof(HomePage))
        {
        }
    }
}