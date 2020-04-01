using Northwind.DataAccess;
using System.Data;
using Xunit;

namespace Northwind.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void RepositoryInitializationSucceeds()
        {
            // Arrange:
            Repository repository;

            // Act:
            repository = new Repository();

            // Assert
        }

        [Fact]
        public void CanExecuteSql()
        {
            // Arrange:
            string query = "SELECT * FROM Orders";
            Repository repository = new Repository();
            DataSet result;
            int rowCount;

            // Act:
            result = repository.Execute(query);

            // Assert:
            rowCount = result.Tables[0].Rows.Count;
            Assert.True(rowCount > 0);
        }
    }
}