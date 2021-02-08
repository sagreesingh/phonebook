using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhoneBook.Entity;
using Common.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookClient.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class BookController : SiteController
  {
    public BookController(ILogger<BookController> logger, IConfiguration config) : base(logger, config)
    {
      _api._controller = "Book";
    }

    public async Task<IEnumerable<Book>> GetList(Book book)
    {
      IEnumerable<Book> list = null;
      string responsedata = await PostAsync2(book, (int)enAPIRequest.getlist);
      if (!string.IsNullOrEmpty(responsedata))
      {
        list = JsonConvert.DeserializeObject<IEnumerable<Book>>(responsedata);
      }
      return list;
    }
  }
}
