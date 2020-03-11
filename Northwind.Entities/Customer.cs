namespace Northwind.Entities
{
    public class Customer
    {
        protected string customerID;
        protected string companyName;
        protected string contactName;
        protected string contactTitle;
        protected string address;
        protected string city;
        protected string region;
        protected string postalCode;
        protected string country;
        protected string phone;
        protected string fax;

        public Customer(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax)
        {
            CustomerID = customerID;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            Fax = fax;
        }

        public virtual string CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                if(customerID != value)
                {
                    customerID = value;
                }
            }
        }

        public virtual string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                if(companyName != value)
                {
                    companyName = value;
                }
            }
        }

        public virtual string ContactName
        {
            get
            {
                return contactName;
            }
            set
            {
                if(companyName != value)
                {
                    contactName = value;
                }
            }
        }

        public virtual string ContactTitle
        {
            get
            {
                return contactTitle;
            }
            set
            {
                if(contactTitle != value)
                {
                    contactTitle = value;
                }
            }
        }

        public virtual string Address
        {
            get
            {
                return address;
            }
            set
            {
                if(address != value)
                {
                    address = value;
                }
            }
        }

        public virtual string City
        {
            get
            {
                return city;
            }
            set
            {
                if(city != value)
                {
                    city = value;
                }
            }
        }

        public virtual string Region
        {
            get
            {
                return region;
            }
            set
            {
                if(region != value)
                {
                    region = value;
                }
            }
        }

        public virtual string PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                if(postalCode != value)
                {
                    postalCode = value;
                }
            }
        }

        public virtual string Country
        {
            get
            {
                return country;
            }
            set
            {
                if(country != value)
                {
                    country = value;
                }
            }
        }

        public virtual string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if(phone != value)
                {
                    phone = value;
                }
            }
        }

        public virtual string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                if(fax != value)
                {
                    fax = value;
                }
            }
        }

        public override string ToString()
        {
            return $"{CompanyName}";
        }
    }
}