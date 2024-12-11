using CustomerMVC.Models;
using CustomerMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var customers = _service.GetAllCustomers();
            return View(customers);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _service.AddCustomer(customer); 
                    return RedirectToAction("Index"); 
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message); 
                }
            }

            return View(customer); 
        }


    }
}
