using Newtonsoft.Json;
using NUnit.Framework;
using PhoneBook.Entity;
using Common.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneBookTest
{
  public class ContactTest
  {
    protected int _bookid { get; set; }
    protected static HttpHelper _api { get; set; }

    [SetUp]
    public void Setup()
    {
      _bookid = 1; // Only a single book is catered
      _api = new HttpHelper("http://localhost:5013");
      _api._controller = "Contact";
    }

    // -----------------------------------------------------------

    [Test]
    public async Task API_Add(
      [Values("aaa", "bbb", "ccc")] string name,
      [Values("111", "222", "333")] string number)
    {
      Contact contact =  new Contact() { BookId = _bookid, Name = name, Number = number };
      var responsedata = await PostAsync2(contact, (int)enAPIRequest.put);
      Assert.True(String.IsNullOrEmpty(responsedata));
    }

    [Test]
    public async Task API_GetList([Values(null, "", "a", "1", "d")] string name)
    {
      string responsedata = await PostAsync2(new Contact() { BookId = _bookid, Name = name }, (int)enAPIRequest.getlist);
      Assert.False(String.IsNullOrEmpty(responsedata));

      IEnumerable<Contact> list = JsonConvert.DeserializeObject<IEnumerable<Contact>>(responsedata);
      Assert.IsNotNull(list);
    }

    // -----------------------------------------------------------

    protected async Task<string> PostAsync2(object obj, int methodid, string controller = "", string action = "")
    {
      Assert.NotNull(_api);

      HttpResponseMessage responseMessage = await _api.PostAsync(obj, methodid, controller, action);
      Assert.False((responseMessage == null) || (responseMessage.Content == null));

      string responsedata = responseMessage.Content.ReadAsStringAsync().Result;
      Assert.True(responseMessage.IsSuccessStatusCode);
      if (responseMessage.IsSuccessStatusCode)
      {
        return responsedata;
      }

      SiteError error = JsonConvert.DeserializeObject<SiteError>(responsedata);
      Assert.IsNotNull(error);
      return null;
    }
  }
}