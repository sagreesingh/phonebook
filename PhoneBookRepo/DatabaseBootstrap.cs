using Common.Repo;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace PhoneBook.Repo
{
  public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig databaseConfig;

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Contact';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "Contact")
                return;

            connection.Execute(@"
              CREATE TABLE Book (
                BookId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name VARCHAR(100) NOT NULL);

              CREATE TABLE Contact (
                BookId INTEGER NOT NULL,
                Name VARCHAR(100) NOT NULL,
                Number VARCHAR(1000) NULL,
                FOREIGN KEY(BookId) REFERENCES Book(BookId));

              INSERT INTO Book (Name) VALUES ('defaultBook');
            "); // Only a single book is catered
        }
    }
}
