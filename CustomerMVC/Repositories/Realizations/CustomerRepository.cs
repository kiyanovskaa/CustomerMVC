using CustomerMVC.Models;
using CustomerMVC.Repositories.Interfaces;
using MongoDB.Driver;

namespace CustomerMVC.Repositories.Realizations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerRepository(IMongoDatabase database)
        {
            _customers = database.GetCollection<Customer>("Customers");
        }
        public Customer Create(Customer customer)
        {
            _customers.InsertOne(customer);
            return customer;
        }

        public List<Customer> GetAll()
        {
            return _customers.Find(customer => true).ToList();
        }
    }
}
