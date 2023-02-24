using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CoreDemo.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager writermanager = new WriterManager(new EfWriterRepository());

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CityViewModel();
            model.Sehirler = new List<SelectListItem>();
            model.Sehirler.Add(new SelectListItem() { Text = "İstanbul", Value = "1", Selected = false });
            model.Sehirler.Add(new SelectListItem() { Text = "Ankara", Value = "2", Selected = false });
            model.Sehirler.Add(new SelectListItem() { Text = "İzmir", Value = "3", Selected = false });
            model.Sehirler.Add(new SelectListItem() { Text = "Diğer", Value = "4", Selected = false });

            ViewBag.City = model;
            return View();
        }   

        [HttpPost]
        public IActionResult Index(Writer writer)
        {
            
            WriterValidator validationRules = new WriterValidator();
            ValidationResult results = validationRules.Validate(writer);

            if (results.IsValid)
            {
                writer.WriterStatus = true;
                writer.WriteAbout = "Deneme Test";
                writermanager.Add(writer);

                return RedirectToAction("Index", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }

    }
}
