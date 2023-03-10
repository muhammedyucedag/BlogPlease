using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminMessageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        Context context = new Context();
        public IActionResult InBox()
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriteId).FirstOrDefault();
            var values = message2Manager.GetInBoxListByWriter(writerId);
            return View(values);
        }

        public IActionResult SendBox()
        {

            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriteId).FirstOrDefault();
            var values = message2Manager.GetSendBoxListByWriter(writerId);
            return View(values);
        }

        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ComposeMessage(Message2 message2)
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriteId).FirstOrDefault();
            message2.SenderId = writerId;
            message2.ReceiverId = 3;
            message2.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            message2.Status = true;
            message2Manager.Add(message2);
            return RedirectToAction("Sendbox");
        }
    }
}
