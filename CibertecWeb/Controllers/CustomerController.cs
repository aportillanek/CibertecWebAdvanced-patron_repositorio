using Microsoft.AspNetCore.Mvc;
using Cibertec.UnitOfWork;

namespace Cibertec.Controllers
{
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
    }
}