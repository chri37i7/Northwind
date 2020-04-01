using Northwind.Utilities;
using Xunit;

namespace Northwind.Tests
{
    public class ValidationsTests
    {
        #region ValidateIsFloatNegative Tests
        [Fact]
        public void IsFloatNegativeReturnsFalse()
        {
            // Arrange:
            int negativeNumber = -1;

            // Act:
            (bool isValid, string errorMessage) = Validations.ValidateIsFloatNegative(negativeNumber);

            // Assert:
            Assert.False(isValid);
        }

        [Fact]
        public void IsFloatNegativeReturnsTrue()
        {
            // Arrange:
            int positiveNumber = 1;

            // Act:
            (bool isValid, string errorMessage) = Validations.ValidateIsFloatNegative(positiveNumber);

            // Assert:
            Assert.True(isValid);
        }
        #endregion

        #region ValidateIsStringNull Tests
        [Fact]
        public void IsStringNullReturnsFalse()
        {
            // Arrange:
            string emptyString = string.Empty;

            // Act:
            (bool isValid, string errorMessage) = Validations.ValidateIsStringNull(emptyString);

            // Assert:
            Assert.False(isValid);
        }

        [Fact]
        public void IsStringNullReturnsTrue()
        {
            // Arrange:
            string testString = "This is text";

            // Act:
            (bool isValid, string errorMessage) = Validations.ValidateIsStringNull(testString);

            // Assert:
            Assert.True(isValid);
        }
        #endregion
    }
}