using System.Text;
using System.Text.Json;

namespace BookTeamsMeeting.Services
{
    public class HttpService
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://graph.microsoft.com "),
        };

        static async Task PostAsync(object request, string endPoint, HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(endPoint, request);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
        }
    }
}
