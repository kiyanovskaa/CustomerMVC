using CustomerMVC.Models;
using CustomerMVC.Repositories.Interfaces;
using CustomerMVC.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests.ValidatorsTests
{
    public class CustomerValidatorTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerValidator _validator;

        public CustomerValidatorTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _validator = new CustomerValidator(_mockRepository.Object);
        }

        [Fact]
        public void Validate_ExistingEmail_ThrowsArgumentException()
        {
            var customer = new Customer { Email = "f@gmail.com", Phone = "+380123456789" };

            _mockRepository.Setup(r => r.GetByEmail(customer.Email)).Returns(new Customer());
            var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(customer));
            Assert.Equal("A customer with this email already exists.", exception.Message);
        }

        [Fact]
        public void Validate_ExistingPhone_ThrowsArgumentException()
        {
            var customer = new Customer { Email = "f@gmail.com", Phone = "+380123456789" };

            _mockRepository.Setup(r => r.GetByPhone(customer.Phone)).Returns(new Customer());

            var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(customer));
            Assert.Equal("A customer with this phone already exists.", exception.Message);
        }

        [Fact]
        public void Validate_NoExistingCustomer_DoesNotThrowException()
        {
            var customer = new Customer { Email = "new.email@example.com", Phone = "+380123456789" };

            _mockRepository.Setup(r => r.GetByEmail(customer.Email)).Returns((Customer)null);
            _mockRepository.Setup(r => r.GetByPhone(customer.Phone)).Returns((Customer)null);

            var exception = Record.Exception(() => _validator.Validate(customer));
            Assert.Null(exception);
        }
    }
}
