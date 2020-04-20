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
    [Route("cars")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            this._carService = carService;
        }

        [Route("all")]
        public IActionResult Index()
        {
            //return Content("Hello from home controller");
            return View(_carService.GetAll());
        }
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            return View(_carService.GetById(id));
        }
        [Route("edit/{id?}")]
        [HttpGet]
        // GET: /<users>/{id}
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new CarViewModel());

            var model = _carService.GetById(id.Value);
            if (model == null)
                return NotFound();

            return View(model);
        }
        [Route("edit/{id?}")]
        [HttpPost]
        // GET: /<users>/{id}
        public IActionResult Edit(CarViewModel model2)
        {
            if (model2.Id > 0) // если есть Id, то редактируем модель
            {
                var dbItem = _carService.GetById(model2.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found

                dbItem.BodyType = model2.BodyType;
                dbItem.Color = model2.Color;
                dbItem.Drive = model2.Drive;
                dbItem.EngineVolume = model2.EngineVolume;
                dbItem.Mark = model2.Mark;
                dbItem.CarModel = model2.CarModel;
                dbItem.Transmission = model2.Transmission;
                dbItem.YearRelease = model2.YearRelease;

            }
            else // иначе добавляем модель в список
            {
                _carService.AddNew(model2);
            }
            _carService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }

        [Route("delete/{id}")]
        [HttpPost]
        public IActionResult Delete(CarViewModel model2)
        {
            var dbItem = _carService.GetById(model2.Id);

            if (ReferenceEquals(dbItem, null))
                return NotFound();// возвращаем результат 404 Not Found

            _carService.Delete(dbItem.Id);
            _carService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }
    }
}