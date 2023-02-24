using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class DashBoardController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());

        public IActionResult Index()
        {
            Context context = new Context();
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriteId).FirstOrDefault();

            ViewBag.totalnumberblogs = context.Blogs.Count().ToString();
            ViewBag.totalnumberusersblogs = context.Blogs.Where(x => x.WriterId == writerId).Count();
            ViewBag.totalnumbercategories = context.Categorys.Count();
            return View();
        }
    }
}
