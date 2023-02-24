using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Blog
{
    public class BlogListDashBoard : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            
            var values = blogManager.GetBlogListWithCategory().
                OrderByDescending(x => x.BlogId).Take(10).ToList();
            return View(values);
        }
    }
}
