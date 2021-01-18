using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhomXingfa.Controllers
{
    public class HomeController : Controller
    {
        XingFaEntities db = new XingFaEntities();
        public ActionResult Index()
        {
            var model = new IndexPageViewModel();
            model.goidinhky = db.Products.Where(q => q.IsActive == true && q.IsProduct == false).ToList();
            model.products = db.Products.Where(q => q.IsActive == true && q.IsProduct == true).ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}