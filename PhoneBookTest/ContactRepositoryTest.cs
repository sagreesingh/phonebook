using Common.Repo;
using NUnit.Framework;
using PhoneBook.Entity;
using PhoneBook.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookTest
{
  class ContactRepositoryTest
  {
    protected int _bookid { get; set; }
    protected ContactRepository _repo { get; set; }

    [SetUp]
    public void Setup()
    {
      _bookid = 1; // Only a single book is catered
      string src = string.Format("Data Source=PhoneBook{0}.sqlite", DateTime.Now.Ticks);
      DatabaseConfig config = new DatabaseConfig() { Name = src };
      DatabaseBootstrap db = new DatabaseBootstrap(config);
      db.Setup();

      _repo = new ContactRepository(config);
    }

    // -----------------------------------------------------------

    [Test]
    public async Task Add(
      [Values("aaa", "bbb", "ccc")] string name,
      [Values("111", "222", "333")] string number)
    {
      Assert.NotNull(_repo);

      Contact contact = new Contact() { BookId = _bookid, Name = name, Number = number };
      await _repo.Insert(contact);
    }

    [Test]
    public async Task GetList([Values(null, "", "a", "1", "d")] string name)
    {
      Assert.NotNull(_repo);

      Contact contact = new Contact() { BookId = _bookid, Name = name };
      IEnumerable<Contact> list = await _repo.GetList(contact);
      Assert.IsNotNull(list);
    }
  }
}
