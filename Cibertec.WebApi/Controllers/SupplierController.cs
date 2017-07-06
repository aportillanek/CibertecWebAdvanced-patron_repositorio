using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cibertec.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{
    [Route("supplier")]
    public class SupplierController : BaseController
    {
        public SupplierController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_unit.Suppliers.GetAll());
        }
        [HttpPost]

        public IActionResult Create([FromBody] Supplier supplier)
        {

            return Ok(_unit.Suppliers.Insert(supplier));
        }
        [HttpPut]
        public IActionResult Update([FromBody] Supplier supplier)
        {
            return Ok(_unit.Suppliers.Update(supplier));

        }
        [HttpDelete]
        public IActionResult Delete([FromBody] Supplier supplier)
        {
            return Ok(_unit.Suppliers.Delete(supplier));

        }
    }
}
