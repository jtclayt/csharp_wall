using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Session;
using Wall.Models;

namespace Wall.Controllers
{
    public class HomeController : Controller
    {
        private Context _db;

        public HomeController(Context context)
        {
            _db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            IndexWrapper data = new IndexWrapper();
            data.AllMessages = _db.Messages
                .Include(m => m.Creator)
                .Include(m => m.Comments)
                .ThenInclude(c => c.Creator)
                .OrderByDescending(m => m.CreatedAt)
                .ToArray();
            return View("Index", data);
        }

        [HttpPost("messages")]
        public IActionResult CreateMessage(IndexWrapper postData)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                postData.MessageForm.UserId = HttpContext.Session.Get<User>("user").UserId;
                _db.Add(postData.MessageForm);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Index();
        }

        [HttpPost("messages/{id}")]
        public IActionResult CreateComment(int id, IndexWrapper postData)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                postData.CommentForm.UserId = HttpContext.Session.Get<User>("user").UserId;
                postData.CommentForm.MessageId = id;
                _db.Add(postData.CommentForm);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Index();
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
