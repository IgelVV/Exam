using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class ProjectPage : Form
    {
        public readonly Footer footer = new();
        public readonly TableForm table = new();

        public ProjectPage(string projectName) 
            : base(By.XPath($"//*[contains(@class, 'breadcrumb')]/*[text()[contains(., '{projectName}')]]"), 
                  $"{nameof(ProjectPage)} {projectName}")
        {
        }
    }
}