using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhomXingfa.Controllers
{
    public class ProductController : Controller
    {
        XingFaEntities db = new XingFaEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GoiSanPham()
        {
            var model = new GoiDinhKyViewModel();
            model.products = db.Products.Where(q => q.IsProduct == true).ToList();
            model.lstQAs= db.Q_A.Where(a => a.IsActive == true).OrderBy(a => a.ThuTu).ToList();
            model.lstCustomerFeed = db.CustomerFeedbacks.Where(c => c.IsActive == true).OrderBy(c => c.ThuTu).ToList();
            return View(model);
        }

        public ActionResult Detail(int? id)
        {
            var model = new DetailPageViewModel();
            model.product = db.Products.Find(id);
            model.products = db.Products.Where(q => q.IsProduct == false && q.IsActive == true).ToList();
            return View(model);
        }
    }
}