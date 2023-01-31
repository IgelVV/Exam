using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using ExamVeshkin.Extensions;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class Footer : Form
    {
        private ILabel FooterLabel =>
            ElementFactory.GetLabel(By.ClassName("footer-text"), nameof(FooterLabel));

        public Footer() : base(By.ClassName("footer"), nameof(Footer))
        {
        }

        public string GetFooterText() => FooterLabel.Text;

        public string GetVariantNumber()
        {
            return GetFooterText().FindNumber();
        }
    }
}