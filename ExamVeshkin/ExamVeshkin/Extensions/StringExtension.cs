using System.Text.RegularExpressions;

namespace ExamVeshkin.Extensions
{
    public static class StringExtension
    {
        public static string FindNumber(this string text)
        {
            var pattern = new Regex(@"\d+");
            var number = pattern.Match(text).Value;
            return number;
        }
    }
}
