using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ActionResultsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFile()
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/15.jpg");
            string file_type = "image/jpeg";
            //Имя файла - необязательно
            string file_name = "My awesome ring.jpg";
            return File(file_path, file_type, file_name);
        }
        //Отправка массива байтов
        public FileResult GetBytes()
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/15.jpg");
            byte[] mas = System.IO.File.ReadAllBytes(file_path);
            string file_type = "inage/jpeg";
            string file_name = "My awesome ring.jpg";
            return File(mas, file_type, file_name);
        }
        //Отправка потока
        public FileResult GetStream()
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/15.jpg");
            FileStream fs = new FileStream(file_path,FileMode.Open);
            string file_type = "inage/jpeg";
            string file_name = "My awesome ring.jpg";
            return File(fs, file_type, file_name);
        }
        public JsonResult JsonObjectSerialized()
        {
            var employee = new EmployeeViewModel
            {
                Id = 1,
                FirstName = "Иван",
                SurName = "Иванов",
                Patronymic = "Иванович",
                Age = 22,
                Position = "Начальник"
            };
            return Json(employee);
        }
        public IActionResult GoGoogle()
        {
            return Redirect("https://google.com");
        }
        public IActionResult GoHomePage()
        {
            return LocalRedirect("~/Home/Index");
        }
        public IActionResult RedirectWithParameters()
        {
            return RedirectToAction("MereContentString", "ActionResults", new { name = "Dear user" });
        }
        public IActionResult ForbiddenResource()
        {
            //return Forbid();//the same
            return StatusCode(403);
        }
        public IActionResult NotFoundResource()
        {
            //return StatusCode(404, "Nothing found. Sorry.");
            return NotFound("Nothing found.Sorry.");
        }
        public IActionResult AgeChek(int age)
        {
            if (age < 18)
                return Unauthorized("Sorry. Abults only");
            return Content("You're welcome");
        }
        public IActionResult TellMeItskOk()
        {
            return Ok("Everything is gonna be fine!");
        }
        public IActionResult ReallyBadRequest(string s)
        {
            if (String.IsNullOrEmpty(s))
                return BadRequest("Some parameter was expected");
            return View("Index");
        }
        public IActionResult MereContentString(string name)
        {
            return Content($"Hi, {name}. I'm mere string content result");
        }
        /// <summary>
        /// Reurtn othing and status code = 200
        /// </summary>
        /// <returns></returns>
        public IActionResult Nothing()
        {
            return new EmptyResult();
        }
        /// <summary>
        ///  Reurtn othing and status code = 204
        /// </summary>
        /// <returns></returns>
        public IActionResult Nothing204()
        {
            return new NoContentResult();
        }
    }
}