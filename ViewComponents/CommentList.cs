using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents
{
    public class CommentList: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var commentValues = new List<UserComment>
            {
                new UserComment
                {
                    Id= 1,
                    Username="Muhammed"
                },
                new UserComment
                {
                    Id= 2,
                    Username="Emir"
                },
                new UserComment
                {
                    Id= 3,
                    Username="Barış"
                }
            };
            return View(commentValues);
        }
    }
}
