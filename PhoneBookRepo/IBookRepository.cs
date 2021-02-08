using PhoneBook.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Repo
{
  public interface IBookRepository
  {
    Task<IEnumerable<Book>> GetList(Book contact);
    Task Insert(Book contact);
  }
}