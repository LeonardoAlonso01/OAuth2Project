using OAuth2Project.API.Entities;

namespace OAuth2Project.API.Services.Interfaces
{
    public interface IContactsService
    {
        Task<IEnumerable<PersonInfo>> GetContacts(string accessToken);
    }
}
