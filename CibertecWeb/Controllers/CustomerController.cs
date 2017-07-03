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

        
        public IActionResult Edit(int id)
        {

            return View(_unit.Customers.GetEntityById(id));
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var result = _unit.Customers.Update(customer);

            if (result) 
            {
                return RedirectToAction("Index"); 
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
        public IActionResult Create()
        {
            ViewBag.CustomerList = _unit.Customers.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            var result = _unit.Customers.Insert(customer);

            if(result>0)
            {
                return RedirectToAction("Index");

            }
            else
            {
                return View(customer);
            }
          

        }
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            var result = _unit.Customers.Delete(customer);
            if (result)
            {
                return RedirectToAction("Index");

            }
            else
            {
                return View(customer);
            }
         

        }

        public IActionResult Delete (int id)
        {

            return View(_unit.Customers.GetEntityById(id));
        }

      

       
    }
}