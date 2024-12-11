using CustomerMVC.Models;

namespace CustomerMVC.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Create(Customer customer);
        List<Customer> GetAll();
        Customer GetByEmail(string email);
        Customer GetByPhone(string phone);
    }
}
