using Northwind.Utilities;
using System;

namespace Northwind.Entities
{
    public class ExchangeRate
    {
        // Fields
        protected string currency;
        protected double rate;

        public ExchangeRate(string currency, double rate)
        {
            Currency = currency;
            Rate = rate;
        }

        /// <summary>
        /// Represents the currency of the <see cref="ExchangeRate"/> object.
        /// </summary>
        public virtual string Currency
        {
            get
            {
                return currency;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsStringNull(value);
                if(isValid)
                {
                    if(currency != value)
                    {
                        currency = value;
                    }
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(Currency));
                }
            }
        }

        /// <summary>
        /// Represents the exchange rate of the 
        /// </summary>
        public virtual double Rate
        {
            get
            {
                return rate;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsDoubleNegative(value);
                if(isValid)
                {
                    if(rate != value)
                    {
                        rate = value;
                    }
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(Rate));
                }
            }
        }
    }
}