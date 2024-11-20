using System.Net.Http;

namespace Lab5.Lab6GetToken
{
    public class Auth0TokenResponse
    {
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }
        public string Token_Type { get; set; }
    }

    public class GetToken
    {
        public static async Task<string> GetAccessTokenAsync(IHttpClientFactory _httpClientFactory, IConfiguration _configuration)
        {
            var client = _httpClientFactory.CreateClient();
            var tokenEndpoint = _configuration["Lab6API:TokenEndpoint"];
            var clientId = _configuration["Lab6API:ClientId"];
            var clientSecret = _configuration["Lab6API:ClientSecret"];
            var audience = _configuration["Lab6API:Audience"];

            var requestBody = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "audience", audience }
            };

            var requestContent = new FormUrlEncodedContent(requestBody);
            var response = await client.PostAsync(tokenEndpoint, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unable to retrieve access token.");
            }

            var responseContent = await response.Content.ReadFromJsonAsync<Auth0TokenResponse>();
            return responseContent.Access_Token;
        }
    }
}
