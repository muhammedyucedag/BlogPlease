using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic4 : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            Context context = new Context();
            ViewBag.name = context.Admins.Where(x=> x.Id==1).Select(x => x.Name).FirstOrDefault();
            ViewBag.imageurl = context.Admins.Where(x => x.Id == 1).Select(x => x.ImageUrl).FirstOrDefault();
            ViewBag.ShortDescription = context.Admins.Where(x => x.Id == 1).Select(x => x.ShortDescription).FirstOrDefault();
            return View();
        }
    }
}
