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
        public IActionResult List()
        {
            return Ok(_unit.Products.GetAll());
        }
        [HttpPost]

        public IActionResult Create([FromBody] Product product)
        {

            return Ok(_unit.Products.Insert(product));
        }
        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            return Ok(_unit.Products.Update(product));

        }
        [HttpDelete]
        public IActionResult Delete([FromBody] Product product)
        {
            return Ok(_unit.Products.Delete(product));

        }
    }
}
