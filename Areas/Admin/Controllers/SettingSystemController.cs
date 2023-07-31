using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBanHang.Models;
using WeBanHang.Models.EF;

namespace WeBanHang.Areas.Admin.Controllers
{
    public class SettingSystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/SettingSystem
        public ActionResult Index()
        {
            ViewBag.SettingTitle = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle")).SettingValue;
            ViewBag.SettingLogo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo")).SettingValue;
            ViewBag.SettingHotline = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline")).SettingValue;
            ViewBag.SettingEmail = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail")).SettingValue;
            ViewBag.SettingTitleSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo")).SettingValue;
            ViewBag.SettingDesSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo")).SettingValue;
            ViewBag.SettingKeySeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo")).SettingValue;
            return View();
        }
        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel model) 
        {
            // add or update Title
            SystemSetting set = null;
            var checkTitle = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = model.SettingTitle;
                db.SystemSettings.Add(set);
                db.SaveChanges();
            }
            else
            {
                checkTitle.SettingValue = model.SettingTitle;
            }

            // add or update Logo
            var checkLogo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = model.SettingLogo;
                db.SystemSettings.Add(set);
                db.SaveChanges();
            }
            else
            {
                checkLogo.SettingValue = model.SettingLogo;
            }

            // add or update Hotline
            var checkHotline = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (checkHotline == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = model.SettingHotline;
                db.SystemSettings.Add(set);
                db.SaveChanges();
            }
            else
            {
                checkHotline.SettingValue = model.SettingHotline;
            }

            // add or update Email
            var checkEmail = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (checkEmail == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = model.SettingEmail;
                db.SystemSettings.Add(set);
                db.SaveChanges();
            }
            else
            {
                checkEmail.SettingValue = model.SettingEmail;
            }

            // add or update TitleSeo
            var checkTitleSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (checkTitleSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = model.SettingTitleSeo;
                db.SystemSettings.Add(set);
                db.SaveChanges();
            }
            else
            {
                checkTitleSeo.SettingValue = model.SettingTitleSeo;
            }

            // add or update DesSeo
            var checkDesSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (checkDesSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = model.SettingDesSeo;
                db.SystemSettings.Add(set);
                db.SaveChanges();
            }
            else
            {
                checkDesSeo.SettingValue = model.SettingDesSeo;
            }

            // add or update KeySeo
            var checkKeySeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (checkKeySeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue = model.SettingKeySeo;
                db.SystemSettings.Add(set);
                db.SaveChanges();
            }
            else
            {
                checkKeySeo.SettingValue = model.SettingKeySeo;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}