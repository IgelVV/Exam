using ExamVeshkin.Utilities;
using RestSharp;

namespace ExamVeshkin.API
{
    public class API
    {
        private static string _baseUrl = ConfigManager.HostAndPort + ConfigManager.APIPath;
        private readonly RestClient _client = new(_baseUrl);

        public string? GetToken()
        {
            var request = new RestRequest(APIEndpoints.TOKEN_GET);
            request.AddParameter("variant", ConfigManager.Variant);

            var responce = _client.Post(request);
            string? token = responce.Content;
            return token;
        }
    }
}