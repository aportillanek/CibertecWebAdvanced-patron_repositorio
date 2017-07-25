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
        [Route("")]
        public IActionResult GetList()
        {
            return Ok(_unit.Customers.GetAll());
        }
        [HttpGet]
        [Route("list/{page}/{pageNumber}")]
        public IActionResult GetListPaged(int page, int pageNumber)
        {
            int startRow = ((page - 1) * pageNumber) + 1;
            int endRow = page * pageNumber;

            return Ok(_unit.Customers.SearchByPage(startRow, endRow));
        }
        [HttpGet]
        [Route("count")]
        public IActionResult GetNumber()
        {


            return Ok(_unit.Customers.RowNumber());
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Customers.GetEntityById(id));
        }
      
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            var id = _unit.Customers.Insert(customer);
            return Ok(new
            {
                id = id
            });
        }

        [HttpPut]
        public IActionResult Put([FromBody]Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.Customers.Update(customer)) return BadRequest("Incorrect id");
            return Ok(new { status = true });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Customers.Delete(new Customer { Id = id });
            return Ok(new { delete = true });
        }
    }
}
