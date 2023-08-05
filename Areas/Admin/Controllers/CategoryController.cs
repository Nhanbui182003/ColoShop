using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        // GET: Admin/Category
        ApplicationDbContext _dbConnect = new ApplicationDbContext();
        public ActionResult Index()
        {
            var item = _dbConnect.Categories;
            return View(item);
        }
        public ActionResult Add() 
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model) 
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias= WeBanHang.Models.Commons.Filter.FilterChar(model.Title);
                _dbConnect.Categories.Add(model);
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = _dbConnect.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var category = _dbConnect.Categories.FirstOrDefault(c => c.Id == model.Id);
                if (category != null)
                {
                    category.ModifiedDate = DateTime.Now;
                    category.Alias = WeBanHang.Models.Commons.Filter.FilterChar(model.Title);
                    category.Title = model.Title;
                    category.Link= model.Link;
                    category.Description = model.Description;
                    category.SeoKeywords= model.SeoKeywords;
                    category.SeoDescription= model.SeoDescription;
                    category.SeoTitle = model.SeoTitle;
                    category.Position= model.Position;
                    category.ModifiedBy=model.ModifiedBy;
                }
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbConnect.Categories.Find(id);
            if (item != null)
            {
                _dbConnect.Categories.Remove(item);
                _dbConnect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}