using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Models;
using Cibertec.Web.Filter;

namespace Cibertec.Controllers
{
    [ExceptionLoggerFilter]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unit;
        public CustomerController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {             
            return View(_unit.Customers.GetAll());
        }

        public IActionResult Detail()
        {
          

            return View(_unit.Customers.SearchByName("Maria", "Anders"));
        }
    }
}