using CustomerMVC.Models;
using CustomerMVC.Repositories.Interfaces;
using CustomerMVC.Services.Interfaces;
using CustomerMVC.Validators;

namespace CustomerMVC.Services.Realization
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ICustomerValidator _validator;

        public CustomerService(ICustomerRepository repository, ICustomerValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public Customer AddCustomer(Customer customer)
        {
            _validator.Validate(customer);
            return _repository.Create(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }
    }
}
