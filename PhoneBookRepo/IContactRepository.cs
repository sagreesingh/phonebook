using PhoneBook.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Repo
{
  public interface IContactRepository
  {
    Task<IEnumerable<Contact>> GetList(Contact contact);
    Task Insert(Contact contact);
  }
}