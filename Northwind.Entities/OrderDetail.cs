﻿using Northwind.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Entities
{
    public class OrderDetail
    {
        protected int orderID;
        protected string productID;
        protected decimal unitPrice;
        protected int quantity;
        protected float discount;

        public OrderDetail(int orderID, string productID, decimal unitPrice, int quantity, float discount)
        {
            OrderID = orderID;
            ProductID = productID;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = discount;
        }

        public virtual int OrderID
        {
            get
            {
                return orderID;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsIntNegative(value);
                if(isValid)
                {
                    if(orderID != value)
                    {
                        orderID = value;
                    }
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(OrderID));
                }
            }
        }

        public virtual string ProductID
        {
            get
            {
                return productID;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsStringNull(value);
                if(isValid)
                {
                    if(productID != value)
                    {
                        productID = value;
                    } 
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(ProductID));
                }
            }
        }

        public virtual decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsDecimalNegative(value);
                if(isValid)
                {
                    if(unitPrice != value)
                    {
                        unitPrice = value;
                    } 
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(UnitPrice));
                }
            }
        }

        public virtual int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsIntNegative(value);
                if(isValid)
                {
                    if(quantity != value)
                    {
                        quantity = value;
                    } 
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(Quantity));
                }
            }
        }

        public virtual float Discount
        {
            get
            {
                return discount;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsFloatNegative(value);
                if(isValid)
                {
                    if(discount != value)
                    {
                        discount = value;
                    } 
                }
                else
                {
                    throw new ArgumentException(errorMessage, nameof(Discount));
                }
            }
        }
    }
}