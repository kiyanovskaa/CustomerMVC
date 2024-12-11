using CustomerMVC.Models;
using CustomerMVC.Repositories.Interfaces;
using CustomerMVC.Services.Realization;
using CustomerMVC.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.ServicesTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly Mock<ICustomerValidator> _mockValidator;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _mockValidator = new Mock<ICustomerValidator>();
            _service = new CustomerService(_mockRepository.Object, _mockValidator.Object);
        }

        [Fact]
        public void AddCustomer_ValidCustomer_CallsValidatorAndRepository()
        {
            var customer = new Customer
            {
                FirstName = "FName",
                LastName = "LName",
                Email = "mail@gmail.com",
                Phone = "+380123456789",
                Address = "Street"
            };

            _mockRepository.Setup(r => r.Create(It.IsAny<Customer>())).Returns(customer);

            var result = _service.AddCustomer(customer);

            _mockValidator.Verify(v => v.Validate(customer), Times.Once);
            _mockRepository.Verify(r => r.Create(customer), Times.Once);
            Assert.Equal(customer, result);
        }

        [Fact]
        public void GetAllCustomers_ReturnsListOfCustomers()
        {
            var mockCustomers = new List<Customer>
            {
                new Customer { FirstName = "F1", LastName = "L1", Email = "f1@gmail.com" },
                new Customer { FirstName = "F2", LastName = "L2", Email = "f2@gmail.com" }
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(mockCustomers);

            var result = _service.GetAllCustomers();

            var customers = Assert.IsType<List<Customer>>(result);
            Assert.Equal(2, customers.Count);
        }
    }
}
