using System;
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
    }
}
