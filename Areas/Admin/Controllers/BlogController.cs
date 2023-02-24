using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class BlogController : Controller
    {
        //public IActionResult ExportStaticExcelBlogList()
        //{
        //    //verileri excele export etme 

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Blog Listesi");
        //        worksheet.Cell(1, 1).Value = "Blog Id";
        //        worksheet.Cell(1, 2).Value = "Blog Adı";

        //        int blogRowCount = 2;
        //        foreach (var item in GetBlogList())
        //        {
        //            worksheet.Cell(blogRowCount, 1).Value = item.Id;
        //            worksheet.Cell(blogRowCount, 2).Value = item.blogName;
        //            blogRowCount++;
        //        }
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Çalışma1.xlsx");
        //        }
        //    }

        //}
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> blogModels = new List<BlogModel>
            {
                new BlogModel {Id=1,blogName="C# Programlamaya Giriş"},
                new BlogModel {Id=2,blogName="Tesla Firmasının Araçları"},
                new BlogModel {Id=3,blogName="2023 Olimpiyatları"}
            };
            return blogModels;
        }

        public IActionResult BlogListExcel()
        {
            return View();
        }

        // Excel Dinamik Tablo Export

        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog Id";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int blogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(blogRowCount, 1).Value = item.Id;
                    worksheet.Cell(blogRowCount, 2).Value = item.BlogName;
                    blogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Çalışma1.xlsx");
                }
            }
        }
        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> blogModel2s= new List<BlogModel2>();
            using (var context = new Context())
            {
                blogModel2s=context.Blogs.Select(x => new BlogModel2
                {
                    Id=x.BlogId,
                    BlogName=x.BlogTitle

                }).ToList();
            }
            return blogModel2s;
        }
        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }    
}
