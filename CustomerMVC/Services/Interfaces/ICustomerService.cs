using CustomerMVC.Models;

namespace CustomerMVC.Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer AddCustomer(Customer customer);
    }
}
