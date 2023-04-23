using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index()
        {
            var items = _dbConnect.News.OrderByDescending(x => x.Id).ToList();
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(New model) 
        {
            if (ModelState.IsValid) 
            {
                model.CreatedDate= DateTime.Now;
                model.ModifiedDate= DateTime.Now;
                model.CategoryID= 1;
                model.Alias = WeBanHang.Models.Commons.Filter.FilterChar(model.Title);
                _dbConnect.News.Add(model);
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = _dbConnect.News.Where(p=>p.Id == id).FirstOrDefault();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(New model)
        {
            if (ModelState.IsValid)
            {
                var news = _dbConnect.News.FirstOrDefault(c => c.Id == model.Id);
                if (news != null)
                {
                    news.ModifiedDate = DateTime.Now;
                    news.Alias = WeBanHang.Models.Commons.Filter.FilterChar(model.Title);
                    news.Title = model.Title;
                    news.Detail= model.Detail;
                    news.IsActive = model.IsActive;
                    news.Description = model.Description;
                    news.SeoKeywords = model.SeoKeywords;
                    news.SeoDescription = model.SeoDescription;
                    news.SeoTitle = model.SeoTitle;
                    news.ModifiedBy = model.ModifiedBy;
                }
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbConnect.News.Find(id);
            if (item != null)
            {
                _dbConnect.News.Remove(item);
                _dbConnect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult  IsActive(int id)
        {
            var item = _dbConnect.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive; 
                _dbConnect.SaveChanges();
                return Json(new { success = true , isActive = item.IsActive});
            }
            return Json(new { success = false });
        }
    }
}