using ExamVeshkin.Models;
using ExamVeshkin.Utilities;
using RestSharp;
using RestSharp.Authenticators;

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
    }
}