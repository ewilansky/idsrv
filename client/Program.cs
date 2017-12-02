using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Contract.Ensures(Contract.Result<Task>() != null);
            // discover endpoints from metadata
            var disco =
                await DiscoveryClient.GetAsync("https://mac.local:44304");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request a token
            var tokenClient =
                new TokenClient(disco.TokenEndpoint, "webHdfsClient", "secret");

            var tokenResponse =
                await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"Token response error: {tokenResponse.Error}");
                return;
            }

            Console.WriteLine($"The token response is: {tokenResponse.Json}");

            await CallIdentityApi(tokenResponse);
        }

        private static async Task CallIdentityApi(TokenResponse tokenResponse)
        {
            // call the api
            var client = new HttpClient();
            // add access token retrieved from token endpoint - Identity Server
            client.SetBearerToken((tokenResponse.AccessToken));

            var response =
                await client.GetAsync("https://mini.local:44304/api/webhdfs");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failure in retrieving a response from the api: {response.StatusCode}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
