using Azure.Identity;
using BookTeamsMeeting.Common;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace BookTeamsMeeting.Services
{
    public interface IOnlineMeetingService
    {
        Task<string> CreateOnlineMeeting(string OrganizerEmail, string OrganizerName);
    }
    public class OnlineMeetingService : IOnlineMeetingService
    {
        private readonly IConfiguration _configuration;

        public OnlineMeetingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateOnlineMeeting(string OrganizerEmail, string OrganizerName)
        {
            var graphClient = await GetGraphServiceClient();

            var userItemRequest = graphClient.Users[OrganizerEmail.ToLower()];

            var allUsers = await graphClient.Users.GetAsync();

            var requestBody = new Event
            {
                Subject = "Test Teams meeting",
                Organizer = new Recipient
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = OrganizerEmail,
                        Name = OrganizerName,
                    }
                },
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = "Test meeting Invitation",
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = "2024-01-13T14:00:00",
                    TimeZone = "Indian/Mauritius",
                },
                End = new DateTimeTimeZone
                {
                    DateTime = "2024-01-13T15:00:00",
                    TimeZone = "Indian/Mauritius",
                },
                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = OrganizerEmail,
                            Name = OrganizerName,
                        },
                        Type = AttendeeType.Resource,
                    }
                },
                AllowNewTimeProposals = false,
                IsOnlineMeeting = true,
                IsReminderOn = true,
                OnlineMeetingProvider = OnlineMeetingProviderType.TeamsForBusiness,
            };

            var userEvents = userItemRequest.Events;
            try
            {
                var result = await userEvents.PostAsync(requestBody);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private async Task<GraphServiceClient> GetGraphServiceClient()
        {
            string clientId = _configuration.GetSection(AppSettings.ClientId).Value;
            string secret = _configuration.GetSection(AppSettings.ClientSecret).Value;
            string tanent = _configuration.GetSection(AppSettings.TenantId).Value;

            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var clientSecretCredential = new ClientSecretCredential(
                tanent, clientId, secret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            return graphClient;
        }
    }
}
