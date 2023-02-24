using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        
        public IViewComponentResult Invoke()
        {
            Context context = new Context();
            ViewBag.totalnumberblogs = context.Blogs.Count();
            ViewBag.totalmessage = context.Contacts.Count();
            ViewBag.totalcomment = context.Comments.Count();
            //TempData["totalnumberblogs"] = context.Blogs.Count();
            //blogManager.GetList().Count();

            // WEP APİ İLE HAVA DURUMU İŞLEMİ
            string api = "80ed3374d44b118440fb1841f67a469c";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid="+api;
            XDocument document = XDocument.Load(connection);

            ViewBag.weather = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View();
        }   
    }
}
