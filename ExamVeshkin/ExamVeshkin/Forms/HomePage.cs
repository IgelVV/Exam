using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class HomePage : Form
    {
        public readonly Footer footer = new();
        public readonly PanelHeading panelHeading = new();

        private IButton ProjectButton(string projectName) => 
            ElementFactory.GetButton(By.XPath($"//*[contains(@class, 'list-group-item')][text()[contains(., '{projectName}')]]"), $"{nameof(ProjectButton)} {projectName}");
 
        public HomePage() : base(By.XPath("//div[contains(@class, 'list-group')]"), nameof(HomePage))
        {
        }

        public void WaitAndClickProjectButton(string projectName)
        {
            IButton project = ProjectButton(projectName);
            project.State.WaitForExist();
            project.JsActions.ScrollIntoView();
            project.WaitAndClick();
        }

        public bool IsProjectExist(string projectName) => ProjectButton(projectName).State.IsExist;
    }
}