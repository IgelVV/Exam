using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using Aquality.Selenium.Browsers;
using ExamVeshkin.Models;
using OpenQA.Selenium;

namespace ExamVeshkin.Forms
{
    public class TableForm : Form
    {
        private const int FIRST_VALUABLE_ROW_INDEX = 1;
        private const int NAME_INDEX = 0;
        private const int METHOD_INDEX = 1;
        private const int STATUS_INDEX = 2;
        private const int START_TIME_INDEX = 3;
        private const int END_TIME_INDEX = 4;
        private const int DURATION_INDEX = 5;


        private IList<ILabel> Rows => FormElement.FindChildElements<ILabel>(By.XPath("/*/*"));
        private IList<ILabel> GetCells(IElement row) => row.FindChildElements<ILabel>(By.XPath("/*"));

        public TableForm() : base(By.Id("allTests"), nameof(TableForm))
        {
        }

        public List<TestRecord> GetTestRecords()
        {
            State.WaitForEnabled();
            List<TestRecord> testRecords= new();
            foreach (IElement row in Rows.Skip(FIRST_VALUABLE_ROW_INDEX))
            {
                row.State.WaitForDisplayed();
                testRecords.Add(RowToRecord(row));
            }
            return testRecords;
        }

        private TestRecord RowToRecord(IElement row)
        {
            IList<ILabel> cells = GetCells(row);
            TestRecord testRecord = new TestRecord
            {
                Name= cells[NAME_INDEX].Text,
                Method = cells[METHOD_INDEX].Text,
                Status = cells[STATUS_INDEX].Text,
                StartTime = cells[START_TIME_INDEX].Text,
                EndTime = cells[END_TIME_INDEX].Text,
                Duration= cells[DURATION_INDEX].Text,
            };
            return testRecord;
        }
    }
}