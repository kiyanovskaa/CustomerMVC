using CustomerMVC.Models;

namespace CustomerMVC.Validators
{
    public interface ICustomerValidator
    {
        void Validate(Customer customer);
    }
}
