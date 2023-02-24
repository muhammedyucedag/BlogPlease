using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Bibliography;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Writer = EntityLayer.Concrete.Writer;

namespace CoreDemo.Controllers
{
    //Bu Controller içerisindeki action yani sayfaların hepsine yetkisiz erişimi engellemek istiyorsak
    //[Authorize] attributesini Controller seviyesine yani en yukarıya çıkararak yapabiliriz.
    //Böylece aşağısında kalan tüm actionlara yetkisiz erişimi engelleyeceğiz.
    //Kullanıcı bilgileri ile giriş yapmayan kimse aşağıdaki actionlara erişemeyecek.
    public class WriterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        UserManager userManager = new UserManager(new EfUserRepository());


        private readonly UserManager<AppUser> _UserManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _UserManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.usermail = usermail;
            Context context = new Context();
            var writername = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriteName).FirstOrDefault();
            ViewBag.writername = writername;
            return View();
        }

        public IActionResult WriterProfile()
        {
            return View();
        }

        public IActionResult WriterMail()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var values = await _UserManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.mail = values.Email;
            model.namesurname = values.NameSurname;
            model.imageurl = values.ImageUrl;
            model.username = values.UserName;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model)
        {
            var values = await _UserManager.FindByNameAsync(User.Identity.Name);
            values.NameSurname = model.namesurname;
            values.ImageUrl = model.imageurl;
            values.Email = model.mail;

            if (!model.changepassword)
            {
                values.PasswordHash = _UserManager.PasswordHasher.HashPassword(values, model.password);
            }

            var result = await _UserManager.UpdateAsync(values);
            return RedirectToAction("Index", "Dashboard");

            //var pas1 = Request.Form["pass1"];
            //var pas2 = Request.Form["pass2"];

            //if (pas1 == pas2)
            //{
            //    writer.WriterPassword = pas2;
            //    WriterValidator validationRules = new WriterValidator();
            //    ValidationResult result = validationRules.Validate(writer);

            //    if (result.IsValid)
            //    {
            //        userManager.Update(writer);
            //        return RedirectToAction("Index", "Dashboard");
            //    }
            //    else
            //    {
            //        foreach (var item in result.Errors)
            //        {
            //            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //        }
            //    }
            //}
            //else
            //{
            //    ViewBag.hata = "Girdiğiniz Parolalar Uyuşmuyor!";
            //}
            //return View();
        }


        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage writeradd)
        {
            Writer writer = new Writer();

            if (writeradd.WriterImage != null)
            {
                var extension = Path.GetExtension(writeradd.WriterImage.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/",
                    newImageName);
                var stream = new FileStream(location, FileMode.Create);
                writeradd.WriterImage.CopyTo(stream);
                writer.WriterImage = newImageName;
            }

            writer.WriterMail = writeradd.WriterMail;
            writer.WriteName = writeradd.WriteName;
            writer.WriterPassword = writeradd.WriterPassword;
            writer.WriterStatus = true;
            writer.WriteAbout = writeradd.WriteAbout;
            writerManager.Add(writer);
            return RedirectToAction("index", "dashboard");
        }

    }
}
