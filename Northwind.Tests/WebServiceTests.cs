using Northwind.Entities;
using Northwind.Services;
using System.Collections.Generic;
using Xunit;

namespace Northwind.Tests
{
    public class WebServiceTests
    {
        [Fact]
        public async void WebServiceReturnsExchangeRates()
        {
            // Arrange
            WebService service;
            List<ExchangeRate> rates;

            // Act
            service = new WebService();
            rates = await service.GetRates();

            // Assert
            Assert.True(rates.Count > 0);
        }
    }
}