using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Entities
{
    /// <summary>
    /// Represents an <see cref="Order"/> object.
    /// </summary>
    public class Order
    {
        // Fields
        protected int orderID;
        protected string customerID;
        protected int employeeID;
        protected DateTime orderDate;
        protected DateTime requiredDate;
        protected DateTime shippedDate;
        protected int shipVia;
        protected decimal freight;
        protected string shipName;
        protected string shipAddress;
        protected string shipCity;
        protected string shipRegion;
        protected string shipPostalCode;
        protected string shipCountry;
        List<OrderDetail> orderDetails;

        /// <summary>
        /// Creates an order object with the ID property. Used when creating objects with data from the database
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <param name="orderDate"></param>
        /// <param name="requiredDate"></param>
        /// <param name="shippedDate"></param>
        /// <param name="shipVia"></param>
        /// <param name="freight"></param>
        /// <param name="shipName"></param>
        /// <param name="shipAddress"></param>
        /// <param name="shipCity"></param>
        /// <param name="shipRegion"></param>
        /// <param name="shipPostalCode"></param>
        /// <param name="shipCountry"></param>
        /// <param name="orderDetails"></param>
        public Order(int orderID, string customerID, int employeeID, DateTime orderDate, DateTime requiredDate, DateTime shippedDate, int shipVia, decimal freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, List<OrderDetail> orderDetails)
        {
            OrderID = orderID;
            CustomerID = customerID;
            EmployeeID = employeeID;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipVia = shipVia;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            OrderDetails = orderDetails;
        }

        /// <summary>
        /// Creates an Order object without the ID property. Used when creating new data for the database
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <param name="orderDate"></param>
        /// <param name="requiredDate"></param>
        /// <param name="shippedDate"></param>
        /// <param name="shipVia"></param>
        /// <param name="freight"></param>
        /// <param name="shipName"></param>
        /// <param name="shipAddress"></param>
        /// <param name="shipCity"></param>
        /// <param name="shipRegion"></param>
        /// <param name="shipPostalCode"></param>
        /// <param name="shipCountry"></param>
        /// <param name="orderDetails"></param>
        public Order(string customerID, int employeeID, DateTime orderDate, DateTime requiredDate, DateTime shippedDate, int shipVia, decimal freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, List<OrderDetail> orderDetails)
        {
            CustomerID = customerID;
            EmployeeID = employeeID;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipVia = shipVia;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            OrderDetails = orderDetails;
        }

        public virtual int OrderID
        {
            get
            {
                return orderID;
            }
            set
            {
                if(orderID != value)
                {
                    orderID = value;
                }
            }
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

        public virtual int EmployeeID
        {
            get
            {
                return employeeID;
            }
            set
            {
                if(employeeID != value)
                {
                    employeeID = value;
                }
            }
        }

        public virtual DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                if(orderDate != value)
                {
                    orderDate = value;
                }
            }
        }

        public virtual DateTime RequiredDate
        {
            get
            {
                return requiredDate;
            }
            set
            {
                if(requiredDate != value)
                {
                    requiredDate = value;
                }
            }
        }

        public virtual DateTime ShippedDate
        {
            get
            {
                return shippedDate;
            }
            set
            {
                if(shippedDate != value)
                {
                    shippedDate = value;
                }
            }
        }

        public virtual int ShipVia
        {
            get
            {
                return shipVia;
            }
            set
            {
                if(shipVia != value)
                {
                    shipVia = value;
                }
            }
        }

        public virtual decimal Freight
        {
            get
            {
                return freight;
            }
            set
            {
                if(freight != value)
                {
                    freight = value;
                }
            }
        }

        public virtual string ShipName
        {
            get
            {
                return shipName;
            }
            set
            {
                if(shipName != value)
                {
                    shipName = value;
                }
            }
        }

        public virtual string ShipAddress
        {
            get
            {
                return shipAddress;
            }
            set
            {
                if(shipAddress != value)
                {
                    shipAddress = value;
                }
            }
        }

        public virtual string ShipCity
        {
            get
            {
                return shipCity;
            }
            set
            {
                if(shipCity != value)
                {
                    shipCity = value;
                }
            }
        }

        public virtual string ShipRegion
        {
            get
            {
                return shipRegion;
            }
            set
            {
                if(shipRegion != value)
                {
                    shipRegion = value;
                }
            }
        }

        public virtual string ShipPostalCode
        {
            get
            {
                return shipPostalCode;
            }
            set
            {
                if(shipPostalCode != value)
                {
                    shipPostalCode = value;
                }
            }
        }

        public virtual string ShipCountry
        {
            get
            {
                return shipCountry;
            }
            set
            {
                if(shipCountry != value)
                {
                    shipCountry = value;
                }
            }
        }

        public virtual List<OrderDetail> OrderDetails
        {
            get
            {
                return orderDetails;
            }
            set
            {
                if(orderDetails != value)
                {
                    orderDetails = value;
                }
            }
        }
    }
}