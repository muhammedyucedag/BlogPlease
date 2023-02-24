using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{  
    public class WriterAboutOnDashBoard : ViewComponent
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        Context context = new Context();

        //private readonly UserManager<AppUser> _userManager;

        //public WriterAboutOnDashBoard(UserManager<AppUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;  
            ViewBag.veri = username;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriteId).FirstOrDefault();
            var values = writerManager.GetWriterById(writerId);
            return View(values);
        }
    }
}
