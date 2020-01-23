using System.Data.SqlClient;

namespace AnyCompany
{
    internal class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public bool Save(Order order)
        {
            try
            {
                var result = 0;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", order.OrderId);
                        command.Parameters.AddWithValue("@Amount", order.Amount);
                        command.Parameters.AddWithValue("@VAT", order.VAT);
                        command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

                        result = command.ExecuteNonQuery();
                    }
                }

                return (result > 0);
            }
            catch (System.Exception)
            {
                //Write log here
                throw;
            }
        }
    }
}
