using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
namespace WebStore.Controllers
{
    [Route("users")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeeController(IEmployeesService employeesService)
        {
            this._employeesService = employeesService;
        }

        [Route("all")]
        // GET: /users/all
        public IActionResult Index()
        {
            //return Content("Hello from home controller");
            return View(_employeesService.GetAll());
        }
        [Route("{id}")]
        // GET: /<users>/{id}
        public IActionResult Details(int id)
        {
            return View(_employeesService.GetById(id);
        }
    }
}