using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class ProjectPage : Form
    {
        public readonly Footer footer = new();
        public readonly TableForm table = new();
        public readonly PanelHeading panelHeading = new();

        public ProjectPage() : base(By.ClassName("nav-tabs"), nameof(ProjectPage))
        {
        }
    }
}