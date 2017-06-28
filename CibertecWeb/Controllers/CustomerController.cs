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

        public IActionResult Create()
        {

            return View();
        }
        public IActionResult Edit(int id)
        {

            return View(_unit.Customers.GetEntityById(id));
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var result = _unit.Customers.Update(customer);

            if (result) //Si es verdadero
            {
                return RedirectToAction("Index"); //Redireccionamos al listado
            }
            else
            {
                return View(customer);
            }

        }

        public IActionResult Detail()
        {
          

            return View(_unit.Customers.SearchByName("Maria", "Anders"));
        }
    }
}