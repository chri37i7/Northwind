﻿using System;
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
        /// Extract all data relevant to an employee from a dat row object, and return an employee object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static Employee ExtractFrom(DataRow dataRow)
        {
            int id = (int)dataRow["EmployeeID"];
            string privatePhone = (string)dataRow["HomePhone"];

            ContactInformation contactInformation = new ContactInformation(String.Empty, String.Empty, privatePhone, String.Empty);
            Employee employee = new Employee() { Id = id, ContactInformation = contactInformation };
            return employee;
        }
        #endregion


        #region Repository Methods
        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>A list of all employees</returns>
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string sql = "SELECT * FROM Employees";
            DataSet resultSet;
            try
            {
                resultSet = Execute(sql);
            }
            catch(Exception)
            {
                throw;
            }

            if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                {
                    Employee employee = ExtractFrom(dataRow);
                    employees.Add(employee);
                }
            }
            return employees;
        }
        #endregion
    }
}