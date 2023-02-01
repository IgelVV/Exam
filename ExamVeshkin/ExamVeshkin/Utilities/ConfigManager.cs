using Aquality.Selenium.Core.Utilities;

namespace ExamVeshkin.Utilities
{
    public static class ConfigManager
    {
        private static readonly JsonSettingsFile s_configData = new(@"config_data.json");
        private static readonly JsonSettingsFile s_testData = new(@"test_data.json");

        public static string Scheme => s_configData.GetValue<string>("scheme");
        public static string HostAndPort => s_configData.GetValue<string>("hostAndPort");
        public static string WebPath => s_configData.GetValue<string>("webPath");
        public static string APIPath => s_configData.GetValue<string>("apiPath");
        public static string UserName => s_configData.GetValue<string>("userName");
        public static string Password => s_configData.GetValue<string>("password");

        public static string Variant => s_testData.GetValue<string>("variant");
        public static string ProjectName => s_testData.GetValue<string>("projectName");
        public static string ProjectId => s_testData.GetValue<string>("projectId");
        public static string PathToLog => s_testData.GetValue<string>("pathToLog");
    }
}