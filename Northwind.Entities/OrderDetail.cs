using Northwind.Utilities;
using System;

namespace Northwind.Entities
{
    public class OrderDetail
    {
        // Fields
        protected int orderID;
        protected int productID;
        protected decimal unitPrice;
        protected short quantity;
        protected float discount;

        /// <summary>
        /// Initialises a new instance of the <see cref="OrderDetail"/> class.
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <param name="unitPrice"></param>
        /// <param name="quantity"></param>
        /// <param name="discount"></param>
        public OrderDetail(int orderID, int productID, decimal unitPrice, short quantity, float  discount)
        {
            OrderID = orderID;
            ProductID = productID;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = discount;
        }

        /// <summary>
        /// Represents the ID of the <see cref="OrderDetail"/>
        /// </summary>
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

        /// <summary>
        /// Represents the ID of the product
        /// </summary>
        public virtual int ProductID
        {
            get
            {
                return productID;
            }
            set
            {
                (bool isValid, string errorMessage) = Validations.ValidateIsIntNegative(value);
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

        /// <summary>
        /// Represents the unit price of the product
        /// </summary>
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

        /// <summary>
        /// Represents the quantity of the ordered products
        /// </summary>
        public virtual short Quantity
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

        /// <summary>
        /// Represents the discount of the order
        /// </summary>
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