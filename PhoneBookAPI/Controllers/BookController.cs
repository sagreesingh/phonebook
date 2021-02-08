using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Repo;
using PhoneBook.Entity;

namespace PhoneBook.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BookController : ControllerBase
  {
    private readonly IBookRepository bookRepository;

    public BookController(IBookRepository bookRepository)
    {
      this.bookRepository = bookRepository;
    }

    // POST: api/<BookController>
    [HttpPost()]
    public async Task<IEnumerable<Book>> GetList([FromBody] Book book)
    {
      return await bookRepository.GetList(book);
    }


    // POST: api/<BookController>/2
    [HttpPost("{id:int}")]
    public async Task Post([FromBody] Book book)
    {
      await bookRepository.Insert(book);
    }
  }
}