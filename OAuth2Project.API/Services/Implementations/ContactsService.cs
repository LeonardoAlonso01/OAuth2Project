using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;
using OAuth2Project.API.Entities;
using OAuth2Project.API.Services.Interfaces;

namespace OAuth2Project.API.Services.Implementations
{
    public class ContactsService : IContactsService
    {
        public async Task<IEnumerable<PersonInfo>> GetContacts(string accessToken)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);

            var peopleService = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "OAuth2Project"
            });

            var request = peopleService.People.Connections.List("people/me");
            request.PersonFields = "names,phoneNumbers";

            var connections = await request.ExecuteAsync();

            var contacts = connections.Connections.Select(c => new PersonInfo
            {
                Name = c.Names?.FirstOrDefault()?.DisplayName,
                Phone = c.PhoneNumbers?.FirstOrDefault()?.Value
            });

            return contacts;
        }
    }
}
