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
            return View(_employeesService.GetById(id));
        }
        [Route("edit/{id?}")]
        [HttpGet]
        // GET: /<users>/{id}
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeViewModel());

            var model = _employeesService.GetById(id.Value);
            if (model == null)
                return NotFound();

            return View(model);
        }
        [Route("edit/{id?}")]
        [HttpPost]
        // GET: /<users>/{id}
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model.Id > 0) // если есть Id, то редактируем модель
            {
                var dbItem = _employeesService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found

                dbItem.FirstName = model.FirstName;
                dbItem.SurName = model.SurName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
                dbItem.Position = model.Position;
            }
            else // иначе добавляем модель в список
            {
                _employeesService.AddNew(model);
            }
            _employeesService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        // GET: /<users>/{id}
        public IActionResult Delete(EmployeeViewModel model)
        {
            if (model.Id > 0) // если есть Id, то редактируем модель
            {
                var dbItem = _employeesService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found

                dbItem.FirstName = model.FirstName;
                dbItem.SurName = model.SurName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
                dbItem.Position = model.Position;
            }
            else // иначе добавляем модель в список
            {
                _employeesService.AddNew(model);
            }
            _employeesService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }
    }
}