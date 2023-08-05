using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;

namespace WeBanHang.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Post
        public ActionResult Index(string alias)
        {
            var item = db.Posts.FirstOrDefault(x=>x.Alias== alias);
            return View(item);
        }
    }
}