using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;

namespace Cibertec.Web.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly IUnitOfWork _unit;
        public OrderItemsController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            return View(_unit.OrderItems.GetAll());
        }
    }
}