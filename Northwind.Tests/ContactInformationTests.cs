using Northwind.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Northwind.Tests
{
    public class ContactInformationTests
    {
        [Fact]
        public void ContactInformationInitializationSucceeds()
        {
            // Arrange:
            string privatePhone = "3334 4900";
            string workPhone = "3334 4901";
            string privateMail = "home@gmail.com";
            string workMail = "mara@aspit.dk";
            ContactInformation contactInformation;

            // Act:
            contactInformation = new ContactInformation(workPhone, workMail, privatePhone, privateMail);

            // No need to assert, because the test method will fail when an unexpected exception is unhandled here.
        }

        [Fact]
        public void ContactInformationInitializationFails()
        {
            // Arrange:
            string privatePhone = "3334 4900";
            string workPhone = "3334 4901";
            string privateMail = "home@gmail.com";
            string workMail = "maraaspit.dk";   // missing @
            ContactInformation contactInformation;

            // Act:
            contactInformation = new ContactInformation(workPhone, workMail, privatePhone, privateMail);

            // No need to assert inside the test method, because of the ExpectedException attribute will function as the assertion.
        }

        [Fact]
        public void ValidateMailTest()
        {
            // Arrange:
            string correctMail = "mara@aspit.dk";
            string mailMissingSnabelA = "maraaspit.dk";
            string mailMissingTld = "mara@aspitdk";
            string emptyStringMail = String.Empty;
            string nullMail = null;

            // Act:
            var correctMailValidationResult = ContactInformation.ValidateMail(correctMail);
            var mailMissingSnabelAValidationResult = ContactInformation.ValidateMail(mailMissingSnabelA);
            var mailMissingTldValidationResult = ContactInformation.ValidateMail(mailMissingTld);
            var emptyStringMailValidationResult = ContactInformation.ValidateMail(emptyStringMail);
            var nullMailValidationResult = ContactInformation.ValidateMail(nullMail);

            // Assert:
            Assert.True(correctMailValidationResult.isValid);
            Assert.False(mailMissingSnabelAValidationResult.isValid);
            Assert.False(mailMissingTldValidationResult.isValid);
            Assert.True(emptyStringMailValidationResult.isValid); // Empty strings are OK.
            Assert.False(nullMailValidationResult.isValid);
        }

        [Fact]
        public void ValidatePhoneTest()
        {
            var correctPhoneWithPlus = "+45 3334 4901";
            var correctPhoneWithoutPlus = "0045 3334 4901";
            var correctPhoneWithOutPlusAndSpaces = "004533344901";


            // Act:
            var correctPhoneWithPlusValidationResult = ContactInformation.ValidatePhone(correctPhoneWithPlus);
            var correctPhoneWithoutPlusValidationResult = ContactInformation.ValidatePhone(correctPhoneWithoutPlus);
            var correctPhoneWithoutPlusAndSpacesValidationResult = ContactInformation.ValidatePhone(correctPhoneWithOutPlusAndSpaces);

            // Assert
            Assert.True(correctPhoneWithPlusValidationResult.isValid);
            Assert.True(correctPhoneWithoutPlusValidationResult.isValid);
            Assert.True(correctPhoneWithoutPlusAndSpacesValidationResult.isValid);

        }
    }
}