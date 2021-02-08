using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Repo;
using PhoneBook.Entity;

namespace PhoneBook.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ContactController : ControllerBase
  {
    private readonly IContactRepository contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
      this.contactRepository = contactRepository;
    }

    // POST: api/<ContactController>
    [HttpPost()]
    public async Task<IEnumerable<Contact>> GetList([FromBody] Contact contact)
    {
      return await contactRepository.GetList(contact);
    }


    // POST: api/<ContactController>/2
    [HttpPost("{id:int}")]
    public async Task Post([FromBody] Contact contact)
    {
      await contactRepository.Insert(contact);
    }
  }
}