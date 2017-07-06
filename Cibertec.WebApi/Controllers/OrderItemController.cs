using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cibertec.UnitOfWork;
using Cibertec.Models;

namespace Cibertec.WebApi.Controllers
{
    [Route("orderitem")]
    public class OrderItemController : BaseController
    {
        public OrderItemController(IUnitOfWork unit) : base(unit)
        {
        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_unit.OrderItems.GetAll());
        }
        [HttpPost]

        public IActionResult Create([FromBody] OrderItem orderitem)
        {

            return Ok(_unit.OrderItems.Insert(orderitem));
        }
        [HttpPut]
        public IActionResult Update([FromBody] OrderItem orderitem)
        {
            return Ok(_unit.OrderItems.Update(orderitem));

        }
        [HttpDelete]
        public IActionResult Delete([FromBody] OrderItem orderitem)
        {
            return Ok(_unit.OrderItems.Delete(orderitem));

        }
    }
}
