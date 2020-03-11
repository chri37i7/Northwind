using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Entities
{
    public class Employee
    {
        protected int employeeID;
        protected string lastname;
        protected string firstname;
        protected string title;
        protected string titleOfCourtesy;
        protected DateTime birthDate;
        protected DateTime hireDate;
        protected string address;
        protected string city;
        protected string region;
        protected string postalCode;
        protected string country;
        protected string homePhone;
        protected string extension;

        public Employee(int employeeID, string lastname, string firstname, string title, string titleOfCourtesy, DateTime birthDate, DateTime hireDate, string address, string city, string region, string postalCode, string country, string homePhone, string extension)
        {
            EmployeeID = employeeID;
            Lastname = lastname;
            Firstname = firstname;
            Title = title;
            TitleOfCourtesy = titleOfCourtesy;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            HomePhone = homePhone;
            Extension = extension;
        }

        public virtual int EmployeeID
        {
            get
            {
                return employeeID;
            }
            set
            {
                employeeID = value;
            }
        }

        public virtual string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        public virtual string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public virtual string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public virtual string TitleOfCourtesy
        {
            get
            {
                return titleOfCourtesy;
            }
            set
            {
                titleOfCourtesy = value;
            }
        }

        public virtual DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
            }
        }

        public virtual DateTime HireDate
        {
            get
            {
                return hireDate;
            }
            set
            {
                hireDate = value;
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
                address = value;
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
                city = value;
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
                region = value;
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
                postalCode = value;
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
                country = value;
            }
        }

        public virtual string HomePhone
        {
            get
            {
                return homePhone;
            }
            set
            {
                homePhone = value;
            }
        }

        public virtual string Extension
        {
            get
            {
                return extension;
            }
            set
            {
                extension = value;
            }
        }

        public override string ToString()
        {
            return $"{firstname} {Lastname}";
        }
    }
}