using Common.Repo;
using Dapper;
using Microsoft.Data.Sqlite;
using PhoneBook.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Repo
{
  public class ContactRepository : IContactRepository
  {
    private readonly DatabaseConfig databaseConfig;

    public ContactRepository(DatabaseConfig databaseConfig)
    {
      this.databaseConfig = databaseConfig;
    }

    public async Task<IEnumerable<Contact>> GetList(Contact contact)
    {
      using var connection = new SqliteConnection(databaseConfig.Name);

      string sql = string.Format(@"
        SELECT rowid AS ContactId, BookId, Name, Number FROM Contact
         WHERE (Name like '%{0}%'
            OR Number like '%{0}%')
          AND (BookId = {1}) ;", contact.Name, contact.BookId);
      IEnumerable<Contact> list =await connection.QueryAsync<Contact>(sql);
      return list;
    }

    public async Task Insert(Contact contact)
    {
      using var connection = new SqliteConnection(databaseConfig.Name);

      await connection.ExecuteAsync("INSERT INTO Contact (BookId, Name, Number)" +
          "VALUES (@BookId, @Name, @Number);", contact);
    }
  }
}
