using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using Northwind.Entities;
using System.Threading.Tasks;

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
            Init();
        }

        public virtual async void Init()
        {
            try
            {
                SqlConnection connection = await Task.Factory.StartNew(() => GetConnection(connectionString) as SqlConnection);
                (bool, Exception) connectionAttemptResult = await TryConnectUsingAsync(connection);
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
        public virtual async Task<DataSet> ExecuteAsync(string query)
        {
            if(string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Null or whitespace.");
            }
            DataSet resultSet = new DataSet();
            try
            {
                SqlConnection connection = await Task.Factory.StartNew(() => GetConnection(connectionString)) as SqlConnection;
                using(SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, connection)))
                {
                    await Task.Factory.StartNew(() => adapter.Fill(resultSet));
                }
                return resultSet;
            }
            catch(Exception e)
            {
                throw new Exception("Data access error. See inner exception for details", e);
            }
        }

        #region Connection Methods

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
        public async Task<(bool, Exception)> TryConnectUsingAsync(DbConnection connection)
        {
            try
            {
                using(connection)
                {
                    await connection.OpenAsync();
                    await connection.CloseAsync();
                }
                return (true, null);
            }
            catch(Exception e)
            {
                return (false, e);
            }
        }
        #endregion

        #region Extraction Methods

        /// <summary>
        /// Extract all data relevant to an Order from a datarow object, and return an order object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static async Task<Order> ExtractOrderFromAsync(DataRow dataRow)
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
            DataSet details = await repository.ExecuteAsync(query);

            // If the query returned any results
            if(details.Tables.Count > 0 && details.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow resultDataRow in details.Tables[0].Rows)
                {
                    OrderDetail detail = await Task.Factory.StartNew(() => ExtractOrderDetailFrom(resultDataRow));
                    orderDetails.Add(detail);
                }
            }

            // Create the order object
            Order order = new Order(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate,
                shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, orderDetails);

            // Return the created order object
            return order;
        }

        /// <summary>
        /// Extract all data relevant to an OrderDetail from a datarow object, and return an OrderDetail object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Extract all data relevant to a Customer from a datarow object, and return an Customer object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Extract all data relevant to an Employee from a datarow object, and return an Employee object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
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

        #endregion

        #region Repository Methods

        #region Order Methods

        /// <summary>
        /// Inserts the order into the database, and returns it with its ID
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> InsertOrderAsync(Order order)
        {
            string query = $"INSERT INTO Orders(CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) " +
                $"VALUES('{order.CustomerID}','{order.EmployeeID}','{order.OrderDate}','{order.RequiredDate}'," +
                $"'{order.ShippedDate}','{order.ShipVia}','{order.Freight}','{order.ShipName}'," +
                $"'{order.ShipAddress}','{order.ShipCity}','{order.ShipRegion}','{order.ShipPostalCode}','{order.ShipCountry}') " +
                $"SELECT * FROM Orders WHERE OrderID = SCOPE_IDENTITY()";

            DataSet insertQuery = await ExecuteAsync(query);

            List<OrderDetail> orderDetails = new List<OrderDetail>();

            List<Order> inserted = insertQuery.Tables[0].AsEnumerable().Select(dataRow => new Order(
                dataRow.Field<int>("OrderID"),
                dataRow.Field<string>("CustomerID"),
                dataRow.Field<int>("EmployeeID"),
                dataRow.Field<DateTime>("OrderDate"),
                dataRow.Field<DateTime>("RequiredDate"),
                dataRow.Field<DateTime>("ShippedDate"),
                dataRow.Field<int>("ShipVia"),
                dataRow.Field<decimal>("Freight"),
                dataRow.Field<string>("ShipName"),
                dataRow.Field<string>("ShipAddress"),
                dataRow.Field<string>("ShipCity"),
                dataRow.Field<string>("ShipRegion"),
                dataRow.Field<string>("ShipPostalCode"),
                dataRow.Field<string>("ShipCountry"),
                orderDetails)).ToList<Order>();

            // Return the created order object
            return inserted[0];
        }

        /// <summary>
        /// Updates the order in the database
        /// </summary>
        /// <param name="order"></param>
        public async Task UpdateOrderAsync(Order order)
        {
            string query = $"UPDATE Orders SET CustomerID = '{order.CustomerID}', " +
                $"EmployeeID = '{order.EmployeeID}', " +
                $"OrderDate = '{order.OrderDate}', " +
                $"RequiredDate = '{order.RequiredDate}', " +
                $"ShippedDate = '{order.ShippedDate}', " +
                $"ShipVia = '{order.ShipVia}', " +
                $"Freight = '{order.Freight}', " +
                $"ShipName = '{order.ShipName}', " +
                $"ShipAddress = '{order.ShipAddress}', " +
                $"ShipCity = '{order.ShipCity}', " +
                $"ShipRegion = '{order.ShipRegion}', " +
                $"ShipPostalCode = '{order.ShipPostalCode}', " +
                $"ShipCountry = '{order.ShipCountry}' " +
                $"WHERE OrderID = '{order.OrderID}'";

            await ExecuteAsync(query);
        }
        #endregion

        #region Order Detail Methods

        /// <summary>
        /// Inserts an order detail into the database.
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public async Task InsertOrderDetailAsync(OrderDetail orderDetail)
        {
            string query = $"INSERT INTO [Order Details](OrderID, ProductID, UnitPrice, Quantity, Discount) " +
                $"VALUES('{orderDetail.OrderID}','{orderDetail.ProductID}','{orderDetail.UnitPrice}','{orderDetail.Quantity}','{orderDetail.Discount}')";

            await ExecuteAsync(query);
        }

        /// <summary>
        /// Updates an order detail in the database
        /// </summary>
        /// <param name="orderDetail"></param>
        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            string query = $"UPDATE [Order Details] SET UnitPrice = '{Math.Round(orderDetail.UnitPrice, 2)}', Quantity = '{orderDetail.Quantity}', Discount = '{orderDetail.Discount}' WHERE ProductID = '{orderDetail.ProductID}'";

            await ExecuteAsync(query);
        }

        /// <summary>
        /// Deletes an order detail from the database
        /// </summary>
        /// <param name="orderDetail"></param>
        public async Task DeleteOrderDetailAsync(OrderDetail orderDetail)
        {
            string query = $"DELETE FROM [Order Details] WHERE ProductID = '{orderDetail.ProductID}'";

            await ExecuteAsync(query);
        }

        #endregion

        #region Get Data Methods

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders</returns>
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Order> orders = new List<Order>();
            string query = "SELECT * FROM Orders";
            DataSet resultSet;
            try
            {
                resultSet = await ExecuteAsync(query);
            }
            catch(Exception)
            {
                throw;
            }
            if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                {
                    Order order = await ExtractOrderFromAsync(dataRow);
                    orders.Add(order);
                }
            }
            return orders;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders</returns>
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            List<Customer> customers = new List<Customer>();
            string query = "SELECT * FROM Customers";
            DataSet resultSet;
            try
            {
                resultSet = await ExecuteAsync(query);
            }
            catch(Exception)
            {
                throw;
            }
            if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                {
                    Customer customer = await Task.Factory.StartNew(() => ExtractCustomerFrom(dataRow));
                    customers.Add(customer);
                }
            }
            return customers;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders</returns>
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT * FROM Employees";
            DataSet resultSet;
            try
            {
                resultSet = await ExecuteAsync(query);
            }
            catch(Exception)
            {
                throw;
            }
            if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                {
                    Employee employee = await Task.Factory.StartNew(() => ExtractEmployeeFrom(dataRow));
                    employees.Add(employee);
                }
            }
            return employees;
        }
        #endregion

        #endregion
    }
}