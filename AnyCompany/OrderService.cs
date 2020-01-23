using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();


        public List<CustomerOrder> GetAllCustomersWithOrders()
        {
            
            try
            {
                return CustomerRepository.GetCustomerWithOrders();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = null;
            try
            {

                if (order == null || customerId <= 0)
                    return false;

                customer = CustomerRepository.Load(customerId);

                if (customer == null)
                    return false;

                if (order.Amount == 0)
                    return false;

                if (customer.Country == "UK")
                    order.VAT = 0.2d;
                else
                    order.VAT = 0;

                order.CustomerId = customer.CustomerId;

                return orderRepository.Save(order);

            }
            catch (System.Exception ex)
            {
                //Write log here
                throw;
            }
            finally
            {
                customer = null;
            }
        }
    }
}
