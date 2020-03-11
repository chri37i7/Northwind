using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Entities;

namespace Northwind.DataAccess
{
    /// <summary>
    /// Represents the data source.
    /// </summary>
    public class Repository
    {
        #region Fields and constants
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=5;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        #endregion


        #region Constructors
        /// <summary>
        /// Initializes a new instance of Repository. Attempts to establish a connection, and will throw an exception on connection error.
        /// </summary>
        public Repository()
        {
            try
            {
                SqlConnection connection = GetConnection(connectionString) as SqlConnection;
                (bool, Exception) connectionAttemptResult = TryConnectUsing(connection);
            }
            catch(Exception e)
            {
                throw new Exception("Data access error. See inner exception for details", e);
            }
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// Executes the provided SQL statement and returns data wrapped in a data set, if any.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>A <see cref="DataSet"/> wrapping any returned data.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref=""
        public DataSet Execute(string query)
        {
            if(string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Null or whitespace.");
            }
            DataSet resultSet = new DataSet();
            try
            {
                SqlConnection connection = GetConnection(connectionString) as SqlConnection;
                using(SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, connection)))
                {
                    adapter.Fill(resultSet);
                }
                return resultSet;
            }
            catch(Exception e)
            {
                throw new Exception("Data access error. See inner exception for details", e);
            }
        }

        /// <summary>
        /// Creates a connection based on the name of the input parameter connection string.
        /// </summary>
        /// <param name="connectionString">The name of the connection string.</param>
        /// <returns>A new connection.</returns>
        private static DbConnection GetConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        /// <summary>
        /// Attempts to connect to a data source using the provided connection.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <returns>True, if the connection could be established, false otherwise.</returns>
        public (bool, Exception) TryConnectUsing(DbConnection connection)
        {
            try
            {
                using(connection)
                {
                    connection.Open();
                    connection.Close();
                }
                return (true, null);
            }
            catch(Exception e)
            {
                return (false, e);
            }
        }

        /// <summary>
        /// Extract all data relevant to an order from a datarow object, and return an order object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static Order ExtractOrderFrom(DataRow dataRow)
        {
            // Repository object for querying
            Repository repository = new Repository();
            // List for OrderDetails related to the order
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            // Assign DataRows to variables
            int orderID = (int)dataRow["OrderID"];
            string customerID = (string)dataRow["CustomerID"];
            int employeeID = (int)dataRow["EmployeeID"];
            DateTime orderDate = (DateTime)dataRow["OrderDate"];
            DateTime requiredDate = (DateTime)dataRow["RequiredDate"];
            DateTime shippedDate = Convert.IsDBNull(dataRow["ShippedDate"]) ? DateTime.MinValue : (DateTime)dataRow["ShippedDate"];
            int shipVia = (int)dataRow["ShipVia"];
            decimal freight = (decimal)dataRow["Freight"];
            string shipName = Convert.IsDBNull(dataRow["ShipName"]) ? null : (string)dataRow["ShipName"];
            string shipAddress = Convert.IsDBNull(dataRow["ShipAddress"]) ? null : (string)dataRow["ShipAddress"];
            string shipCity = Convert.IsDBNull(dataRow["ShipCity"]) ? null : (string)dataRow["ShipCity"];
            string shipRegion = Convert.IsDBNull(dataRow["ShipRegion"]) ? null : (string)dataRow["ShipRegion"];
            string shipPostalCode = Convert.IsDBNull(dataRow["ShipPostalCode"]) ? null : (string)dataRow["ShipPostalCode"];
            string shipCountry = Convert.IsDBNull(dataRow["ShipCountry"]) ? null : (string)dataRow["ShipCountry"];

            // Query for getting Order details related to the order
            string query = $"SELECT * FROM [Order Details] WHERE OrderID = {orderID};";
            // Execute the query
            DataSet details = repository.Execute(query);

            // If the query returned any results
            if(details.Tables.Count > 0 && details.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow resultDataRow in details.Tables[0].Rows)
                {
                    OrderDetail detail = ExtractOrderDetailFrom(resultDataRow);
                    orderDetails.Add(detail);
                }
            }

            // Create the order object
            Order order = new Order(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate,
                shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, orderDetails);

            // Return the created order object
            return order;
        }

        private static OrderDetail ExtractOrderDetailFrom(DataRow dataRow)
        {
            // Assign DataRows to variables
            int orderID = (int)dataRow["OrderID"];
            int productID = (int)dataRow["ProductID"];
            decimal unitPrice = (decimal)dataRow["UnitPrice"];
            short quantity = (short)dataRow["Quantity"];
            float discount = (float)dataRow["Discount"];

            // Create OrderDetail object
            OrderDetail orderDetail = new OrderDetail(orderID, productID, unitPrice, quantity, discount);

            // Return the created object
            return orderDetail;
        }

        private static Customer ExtractCustomerFrom(DataRow dataRow)
        {
            // Assign DataRows to variables
            string customerID = (string)dataRow["CustomerID"];
            string companyName = (string)dataRow["CompanyName"];
            string contactName = (string)dataRow["ContactName"];
            string contactTitle = (string)dataRow["ContactTitle"];
            string address = (string)dataRow["Address"];
            string city = (string)dataRow["City"];
            string region = Convert.IsDBNull(dataRow["Region"]) ? null : (string)dataRow["Region"];
            string postalCode = Convert.IsDBNull(dataRow["PostalCode"]) ? null : (string)dataRow["PostalCode"];
            string country = (string)dataRow["Country"];
            string phone = (string)dataRow["Phone"];
            string fax = Convert.IsDBNull(dataRow["Fax"]) ? null : (string)dataRow["Fax"];

            // Create OrderDetail object
            Customer customer = new Customer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Return the created object
            return customer;
        }

        private static Employee ExtractEmployeeFrom(DataRow dataRow)
        {
            // Assign DataRows to variables
            int employeeID = (int)dataRow["EmployeeID"];
            string lastname = (string)dataRow["LastName"];
            string firstname = (string)dataRow["FirstName"];
            string title = (string)dataRow["Title"];
            string titleOfCourtesy = (string)dataRow["TitleOfCourtesy"];
            DateTime birthDate = (DateTime)dataRow["BirthDate"];
            DateTime hireDate = (DateTime)dataRow["HireDate"];
            string address = (string)dataRow["Address"];
            string city = (string)dataRow["City"];
            string region = Convert.IsDBNull(dataRow["Region"]) ? null : (string)dataRow["Region"];
            string postalCode = (string)dataRow["PostalCode"];
            string country = (string)dataRow["Country"];
            string homePhone = (string)dataRow["HomePhone"];
            string extension = (string)dataRow["Extension"];

            // Create OrderDetail object
            Employee employee = new Employee(employeeID, lastname, firstname, title, titleOfCourtesy, birthDate, hireDate, address, city, region, postalCode, country, homePhone, extension);

            // Return the created object
            return employee;
        }
        #endregion


        #region Repository Methods
        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders</returns>
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            string query = "SELECT * FROM Orders";
            DataSet resultSet;
            try
            {
                resultSet = Execute(query);
            }
            catch(Exception)
            {
                throw;
            }
            if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                {
                    Order order = ExtractOrderFrom(dataRow);
                    orders.Add(order);
                }
            }
            return orders;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders</returns>
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string query = "SELECT * FROM Customers";
            DataSet resultSet;
            try
            {
                resultSet = Execute(query);
            }
            catch(Exception)
            {
                throw;
            }
            if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                {
                    Customer customer = ExtractCustomerFrom(dataRow);
                    customers.Add(customer);
                }
            }
            return customers;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders</returns>
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT * FROM Employees";
            DataSet resultSet;
            try
            {
                resultSet = Execute(query);
            }
            catch(Exception)
            {
                throw;
            }
            if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                {
                    Employee employee = ExtractEmployeeFrom(dataRow);
                    employees.Add(employee);
                }
            }
            return employees;
        }
        #endregion
    }
}