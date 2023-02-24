using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Category
{
    public class CategoryListDashBoard : ViewComponent
    {
        CategoryManager blogManager = new CategoryManager(new EfCategoryRepository());

        public IViewComponentResult Invoke()
        {

            var values = blogManager.GetList();
            return View(values);
        }
    }
}
