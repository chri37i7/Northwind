using Northwind.Entities;
using System;
using Xunit;

namespace Northwind.Tests
{
    public class EntityTests
    {
        [Fact]
        public void OrderDetailInitializationAndMutationSucceeds()
        {
            // Arrange
            OrderDetail orderDetail = new OrderDetail(1, 27, 13, 43, 0);

            // Act
            orderDetail.ProductID = 26;

            // Assert
            Assert.True(orderDetail.ProductID == 26);
        }

        [Fact]
        public void OrderDetailInvalidInitializationThrows()
        {
            // Arrange
            OrderDetail orderDetail;

            // Actsert
            Assert.Throws<ArgumentException>(
                () => orderDetail = new OrderDetail(-1, 27, 13, 43, 0));
        }
    }
}