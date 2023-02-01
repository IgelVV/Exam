using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class AddProjectPage : Form
    {
        private ITextBox ProjectNameBox =>
            ElementFactory.GetTextBox(By.Id("projectName"), nameof(ProjectNameBox));
        private IButton Submit =>
            ElementFactory.GetButton(By.XPath("//*[contains(@type,'submit')]"), nameof(Submit));
        private ILabel SuccessMessage =>
            ElementFactory.GetLabel(By.ClassName("alert-success"), nameof(SuccessMessage));

        public AddProjectPage() : base(By.Id("projectName"), nameof(AddProjectPage))
        {
        }

        public void ClearAndTypeProjectName(string projectName)
        {
            ProjectNameBox.State.WaitForDisplayed();
            ProjectNameBox.ClearAndType(projectName);
        }

        public void WaitAndClickSubmit() => Submit.WaitAndClick();

        public bool IsProjectSaved() => SuccessMessage.State.IsDisplayed;
    }
}