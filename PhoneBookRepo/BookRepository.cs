using Common.Repo;
using Dapper;
using Microsoft.Data.Sqlite;
using PhoneBook.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Repo
{
  public class BookRepository : IBookRepository
  {
    private readonly DatabaseConfig databaseConfig;

    public BookRepository(DatabaseConfig databaseConfig)
    {
      this.databaseConfig = databaseConfig;
    }

    public async Task<IEnumerable<Book>> GetList(Book book)
    {
      using var connection = new SqliteConnection(databaseConfig.Name);

      return await connection.QueryAsync<Book>(string.Format(@"
        SELECT BookId, Name FROM Book
         WHERE Name like '%{0}%' ", book.Name));
    }

    public async Task Insert(Book book)
    {
      using var connection = new SqliteConnection(databaseConfig.Name);

      await connection.ExecuteAsync(@"INSERT INTO Book (Name) VALUES (@Name);", book);
    }
  }
}
