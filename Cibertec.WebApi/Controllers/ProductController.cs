using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cibertec.UnitOfWork;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{
    [Route("product")]

    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetList()
        {
            return Ok(_unit.Products.GetAll());
        }
        [HttpGet]
        [Route("list/{page}/{pageNumber}")]
        public IActionResult GetListPaged(int page,int pageNumber)
        {
            int startRow = ((page - 1) * pageNumber) + 1;
            int endRow = page * pageNumber;
          
            return Ok(_unit.Products.SearchByPage(startRow, endRow));
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetNumber()
        {
           

            return Ok(_unit.Products.RowNumber());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Products.GetEntityById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            var id = _unit.Products.Insert(product);
            return Ok(new
            {
                id = id
            });
        }

        [HttpPut]
        public IActionResult Put([FromBody]Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.Products.Update(product)) return BadRequest("Incorrect id");
            return Ok(new { status = true });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Products.Delete(new Product { Id = id });
            return Ok(new { delete = true });
        }
    }
}
