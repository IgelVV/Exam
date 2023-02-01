using Aquality.Selenium.Core.Utilities;

namespace ExamVeshkin.Utilities
{
    public static class ConfigManager
    {
        private static readonly JsonSettingsFile s_configData = new(@"configData.json");
        private static readonly JsonSettingsFile s_testData = new(@"testData.json");
        private static readonly JsonSettingsFile s_credentials = new(@"credentials.json");

        public static string Scheme => s_configData.GetValue<string>("scheme");
        public static string HostAndPort => s_configData.GetValue<string>("hostAndPort");
        public static string WebPath => s_configData.GetValue<string>("webPath");
        public static string APIPath => s_configData.GetValue<string>("apiPath");
        public static string CookieNameToken => s_configData.GetValue<string>("cookieNames.token");

        public static string EndpointTokenGet => s_configData.GetValue<string>("apiEndpoints.tokenGet");
        public static string EndpointTestGetJson => s_configData.GetValue<string>("apiEndpoints.testGetJson");
        public static string EndpointTestGetXml => s_configData.GetValue<string>("apiEndpoints.testGetXml");
        public static string EndpointTestPut => s_configData.GetValue<string>("apiEndpoints.testPut");
        public static string EndpointTestPutLog => s_configData.GetValue<string>("apiEndpoints.testPutLog");
        public static string EndpointTestPutAttachment => s_configData.GetValue<string>("apiEndpoints.testPutAttachment");

        public static string Variant => s_testData.GetValue<string>("variant");
        public static string ProjectName => s_testData.GetValue<string>("projectName");
        public static string ProjectId => s_testData.GetValue<string>("projectId");
        public static string PathToLog => s_testData.GetValue<string>("pathToLog");

        public static string UserName => s_credentials.GetValue<string>("userName");
        public static string Password => s_credentials.GetValue<string>("password");
    }
}