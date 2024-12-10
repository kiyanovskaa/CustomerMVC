using CustomerMVC.Models;

namespace CustomerMVC.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Create(Customer customer);
        List<Customer> GetAll();
    }
}
