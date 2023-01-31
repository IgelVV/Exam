using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class PanelHeading : Form
    {
        private IButton Add => FormElement.FindChildElement<IButton>(By.ClassName("pull-right"), nameof(Add));

        public PanelHeading() : base(By.ClassName("panel-heading"), nameof(PanelHeading))
        {
        }

        public void ClickAdd() => Add.Click();
    }
}