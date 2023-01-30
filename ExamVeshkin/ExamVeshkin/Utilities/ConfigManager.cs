using Aquality.Selenium.Core.Utilities;

namespace ExamVeshkin.Utilities
{
    public static class ConfigManager
    {
        private static readonly JsonSettingsFile s_configData = new(@"config_data.json");

        public static string HostAndPort => s_configData.GetValue<string>("hostAndPort");
        public static string WebPath => s_configData.GetValue<string>("webPath");
        public static string APIPath => s_configData.GetValue<string>("apiPath");
        public static string Variant => s_configData.GetValue<string>("variant");
    }
}