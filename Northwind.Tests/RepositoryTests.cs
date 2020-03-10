using Northwind.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
        }

        [Fact]
        public void CanExecuteSql()
        {
            // Arrange:
            string sql = "SELECT * FROM Employees";
            Repository repository = new Repository();
            DataSet result;
            int rowCount;

            // Act:
            result = repository.Execute(sql);

            // Assert:
            rowCount = result.Tables[0].Rows.Count;
            Assert.True(rowCount > 0);
        }
    }
}