﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebStore.Controllers
{
    [Route("users")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeeController(IEmployeesService employeesService)
        {
            this._employeesService = employeesService;
        }

        [Route("all")]
        [AllowAnonymous]//Доступ к этому методу доступен для всех!
        // GET: /users/all
        public IActionResult Index()
        {
            //return Content("Hello from home controller");
            return View(_employeesService.GetAll());
        }
        [Route("Details/{id}")]
        [Authorize(Roles = "Admins,Users")]
        // GET: /<users>/{id}
        public IActionResult Details(int id)
        {
            return View(_employeesService.GetById(id));
        }
        [Route("edit/{id?}")]
        [HttpGet]
        [Authorize(Roles ="Admins") ]
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
        [Authorize(Roles = "Admins")]
        // GET: /<users>/{id}
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model.Age < 18 || model.Age > 100)
            {
                ModelState.AddModelError("Age", "Ошибка возраста!");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

        [Route("delete/{id}")]
        [HttpPost]
        [Authorize(Roles = "Admins")]
        // GET: /<users>/{id}
        public IActionResult Delete(EmployeeViewModel model)
        {
            var dbItem = _employeesService.GetById(model.Id);

            if (ReferenceEquals(dbItem, null))
                return NotFound();// возвращаем результат 404 Not Found

            _employeesService.Delete(dbItem.Id);
            _employeesService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }
    }
}