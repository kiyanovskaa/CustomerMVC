using CustomerMVC.Models;
using CustomerMVC.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace CustomerMVC.Validators
{
    public class CustomerValidator
    {
        private readonly ICustomerRepository _repository;

        public CustomerValidator(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public void Validate(Customer customer)
        {

            var existingCustomer = _repository.GetByEmail(customer.Email);
            if (existingCustomer != null )
            {
                throw new ArgumentException("A customer with this email already exists.");
            }
            var existingCustomer2 = _repository.GetByPhone(customer.Phone);
            if (existingCustomer2 != null)
            {
                throw new ArgumentException("A customer with this phone already exists.");
            }
        }
    }
}
