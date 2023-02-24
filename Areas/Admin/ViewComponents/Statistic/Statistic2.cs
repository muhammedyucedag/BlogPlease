using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Context context = new Context();
            ViewBag.lastblog = context.Blogs.OrderByDescending(x=>x.BlogId).Select(x=>x.BlogTitle).Take(1).FirstOrDefault();
            ViewBag.totalcomment = context.Comments.Count();
            //TempData["totalnumberblogs"] = context.Blogs.Count();
            //blogManager.GetList().Count();

            return View();
        }
    }
}
