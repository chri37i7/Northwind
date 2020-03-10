using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Entities
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Firstname => "Mads";
        public virtual ContactInformation ContactInformation { get; set; }
    }
}