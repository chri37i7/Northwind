using Northwind.Utilities;
using System;
using Xunit;

namespace Northwind.Tests
{
    public class ValidationsTests
    {
        public void TextOnlyTest()
        {
            // Arrange:
            string text = "Mads";
            string nullText = null;
            string empty = String.Empty;
            string numbers = "1234";
            string otherChars = "!#¤%";
            string textWithNumber = "Mad5";
            string textWithOtherChar = "M@ds";

            // Act:
            bool validationResultText = Validations.TextOnly(text);
            bool validationResultNull = Validations.TextOnly(nullText);
            bool validationResultEmpty = Validations.TextOnly(empty);
            bool validationResultNumbers = Validations.TextOnly(numbers);
            bool validationResultOtherChars = Validations.TextOnly(otherChars);
            bool validationResultTextWithNumber = Validations.TextOnly(textWithNumber);
            bool validationResultTextWithOtherChar = Validations.TextOnly(textWithOtherChar);

            // Assert:
            Assert.True(validationResultText);
            Assert.False(validationResultNull);
            Assert.False(validationResultEmpty);
            Assert.False(validationResultNumbers);
            Assert.False(validationResultOtherChars);
            Assert.False(validationResultTextWithNumber);
            Assert.False(validationResultTextWithOtherChar);
        }
    }
}