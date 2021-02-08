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
  public class ContactController : SiteController
  {
    public ContactController(ILogger<ContactController> logger, IConfiguration config) : base(logger, config)
    {
      _api._controller = "Contact";
    }

    public async Task<IEnumerable<Contact>> GetList(Contact contact)
    {
      IEnumerable<Contact> list = null;
      string responsedata = await PostAsync2(contact, (int)enAPIRequest.getlist);
      if (!string.IsNullOrEmpty(responsedata))
      {
        list = JsonConvert.DeserializeObject<IEnumerable<Contact>>(responsedata);
      }
      return list;
    }

    public async Task<Contact> Add(Contact contact)
    {
      var responseData = await PostAsync2(contact, (int)enAPIRequest.put);
      if (String.IsNullOrEmpty(responseData))
        return default(Contact);
      else
      {
        return JsonConvert.DeserializeObject<Contact>(responseData);
      }
    }
  }
}
