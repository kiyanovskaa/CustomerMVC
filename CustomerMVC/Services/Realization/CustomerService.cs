using CustomerMVC.Models;
using CustomerMVC.Repositories.Interfaces;
using CustomerMVC.Services.Interfaces;

namespace CustomerMVC.Services.Realization
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public Customer AddCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName) || string.IsNullOrWhiteSpace(customer.LastName))
            {
                throw new ArgumentException("First and Last names are required.");
            }
            return _repository.Create(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }
    }
}
