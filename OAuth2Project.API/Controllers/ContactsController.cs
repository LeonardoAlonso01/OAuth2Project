using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth2Project.API.Entities;
using OAuth2Project.API.Services.Interfaces;

namespace OAuth2Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _service;

        public ContactsController(IContactsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if(String.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Token de acesso não encontrado");
            }

            var contacts = _service.GetContacts(accessToken);

            return Ok(contacts);
        }
    }
}
