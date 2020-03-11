using System;
using System.Linq;

namespace Northwind.Utilities
{
    public static class Validations
    {
        public static (bool, string) ValidateIsStringNull(string input)
        {
            if(string.IsNullOrEmpty(input))
            {
                return (false, "The value cannot be null, or empty");
            }
            else
            {
                return (true, string.Empty);
            }
        }

        #region Number Validation Methods
        public static (bool, string) ValidateIsIntNegative(int number)
        {
            if(number < 0)
            {
                return (false, "The number cannot be lower than 0");
            }
            else
            {
                return (true, string.Empty);
            }
        }

        public static (bool, string) ValidateIsFloatNegative(float number)
        {
            if(number < 0)
            {
                return (false, "The number cannot be lower than 0");
            }
            else
            {
                return (true, string.Empty);
            }
        }

        public static (bool, string) ValidateIsDecimalNegative(decimal number)
        {
            if(number < 0)
            {
                return (false, "The number cannot be lower than 0");
            }
            else
            {
                return (true, string.Empty);
            }
        }
        #endregion
    }
}