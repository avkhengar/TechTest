using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            if (customerId <= 0)
                return null;

            Customer customer;
            try
            {

                customer = new Customer();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    //Chances of SQL Injection
                    //SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                    //    connection);

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @CustomerId",
             connection))
                    {

                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                                customer.Name = reader["Name"].ToString();
                                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                                customer.Country = reader["Country"].ToString();
                            }

                            reader.Close();

                        }

                    }
                }

                return customer;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// List all Customers with Orders
        /// </summary>
        /// <returns>List Of Customer Orders</returns>
        public static List<CustomerOrder> GetCustomerWithOrders()
        {
           
           List<CustomerOrder> customer = new List<CustomerOrder>();
            try
            {
                
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Customer c INNER JOIN Order o On c.CustomerId = o.CustomerId",
             connection))
                    {
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                           var customerOrder = new CustomerOrder();
                            while (reader.Read())
                            {
                                customerOrder.customer =
                                    new Customer()
                                    {
                                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                        Name = reader["Name"].ToString(),
                                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                        Country = reader["Country"].ToString()
                                    };

                                customerOrder.order =
                                    new Order()
                                    {
                                        OrderId = Convert.ToInt32(reader["OrderId"]),
                                        Amount = Convert.ToDouble(reader["Amount"]),
                                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                        VAT = Convert.ToDouble(reader["VAT"])
                                    };

                                customer.Add(customerOrder);
                            }

                            reader.Close();

                        }

                    }
                }

                return customer;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
