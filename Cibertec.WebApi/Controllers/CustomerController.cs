using Cibertec.Models;
using Cibertec.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
namespace Cibertec.WebApi.Controllers
{
    [Route("customer")]
    public class CustomerController:Controller
    {
        private readonly IUnitOfWork _unit;
        public CustomerController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_unit.Customers.GetAll());
        }
        [HttpPost]
       
        public IActionResult Create(Customer customer)
       {

            return Ok(_unit.Customers.Insert(customer));
        }
    }
}
