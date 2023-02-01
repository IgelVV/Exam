using ExamVeshkin.Models;
using ExamVeshkin.Utilities;
using ExamVeshkin.Extensions;
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
            //Authenticator = new HttpBasicAuthenticator(ConfigManager.UserName, ConfigManager.Password)
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

        public string? AddNewTest(string sid, string projectName, string testName, string methodName)
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
    }
}