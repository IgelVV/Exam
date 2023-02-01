using ExamVeshkin.Models;
using ExamVeshkin.Utilities;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace ExamVeshkin.API
{
    public class API
    {
        private static string _baseUrl = ConfigManager.Scheme 
                                        + ConfigManager.HostAndPort 
                                        + ConfigManager.APIPath;
        private readonly RestClient _client = new(_baseUrl)
        {
            Authenticator = new HttpBasicAuthenticator(ConfigManager.UserName, ConfigManager.Password)
        };

        public string? GetToken()
        {
            var request = new RestRequest(APIEndpoints.TOKEN_GET);
            request.AddParameter("variant", ConfigManager.Variant);

            RestResponse responce = _client.Post(request);
            string? token = responce.Content;
            return token;
        }

        public List<TestRecord>? TestGetJson()
        {
            var request = new RestRequest(APIEndpoints.TEST_GET_JSON);
            request.AddParameter("projectId", ConfigManager.ProjectId);

            List<TestRecord>? response = _client.Post<List<TestRecord>>(request);
            return response;
        }

        public string? CreateTestRecord(string sid, string projectName, string testName, string methodName)
        {
            string hostName = Dns.GetHostName();
            var request = new RestRequest(APIEndpoints.TEST_PUT);
            request.AddParameter("SID", sid);
            request.AddParameter("projectName", projectName);
            request.AddParameter("testName", testName);
            request.AddParameter("methodName", methodName);
            request.AddParameter("env", hostName);

            RestResponse response = _client.Post(request);
            string? id = response.Content;
            return id;
        }

        public void SendTestLog(string testId)
        {
            var request = new RestRequest(APIEndpoints.TEST_PUT_LOG);
            request.AddParameter("testId", testId);
            request.AddParameter("content", File.ReadAllText(ConfigManager.PathToLog));
            _client.Post(request);
        }

        public void AttachPictureToTest(string testId, string picture)
        {
            var request = new RestRequest(APIEndpoints.TEST_PUT_ATTACHMENT);
            request.AddParameter("testId", testId);
            request.AddParameter("contentType", "image/png");
            request.AddParameter("content", picture);
            _client.Post(request);
        }
    }
}