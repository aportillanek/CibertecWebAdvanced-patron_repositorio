using Cibertec.Models;
using Cibertec.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Cibertec.WebApi.Controllers
{
   
    [Route("customer")]
   
    public class CustomerController:BaseController
    {
       
        public CustomerController(IUnitOfWork unit):base(unit)
        {
            
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_unit.Customers.GetAll());
        }
        [HttpPost]
       
        public IActionResult Create([FromBody] Customer customer)
       {
            
            return Ok(_unit.Customers.Insert(customer));

        }
    }
}
