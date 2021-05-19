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

        public ActionResult GoiSanPham(int? id)
        {
            var model = new GoiDinhKyViewModel();
            if (id == null || id == 0)
            {
                model.products = db.Products.Where(q => q.IsProduct == false && q.IsActive == true).OrderBy(a=>a.ThuTu).ToList();
            }
            else
            {
                model.products = db.Products.Where(q => q.IsProduct == false && q.CategoryID == id && q.IsActive == true).OrderBy(a => a.ThuTu).ToList();
            }


            model.lstQAs = db.Q_A.Where(a => a.IsActive == true).OrderBy(a => a.ThuTu).ToList();
            model.lstCustomerFeed = db.CustomerFeedbacks.Where(c => c.IsActive == true).OrderBy(c => c.ThuTu).ToList();
            return View(model);
        }

        public ActionResult Detail(int? id)
        {
            var prod = db.Products.Find(id);

            var model = new DetailPageViewModel();

            model.product = db.Products.Find(id);
            if(prod.IsProduct == true)
            {
                model.products = db.Products.Where(q => q.IsProduct == true && q.IsActive == true && q.ProductID != id && q.CategoryID == prod.CategoryID).ToList();
            }    
            else
            {
                model.products = db.Products.Where(q => q.IsProduct == false && q.IsActive == true && q.ProductID != id).ToList();
            }    
            model.images = db.ProductImages.Where(q => q.ProductID == id).OrderBy(a=>a.ThuTu).ToList();
            model.tintuc = db.Blogs.Where(q => q.Category.TypeCate == 3 && q.IsActive == true).OrderByDescending(o => o.LastModify).Take(4).ToList();
            model.CategoryTitle = db.Categories.Find(model.product.CategoryID).CategoryName;
            return View(model);
        }

        public ActionResult SanPhamDon()
        {
            var model = db.Products.Where(q => q.IsProduct == true).OrderBy(a => a.ThuTu).ToList();
            return View(model);
        }

        public ActionResult SanPham(int? id)
        {
            var model = new SanPhamDonDM();
            model.spdon = db.Products.Where(q => q.IsProduct == true && q.CategoryID == id).Take(12).OrderBy(a => a.ThuTu).ToList();
            model.Title = db.Categories.Find(id).CategoryName;
            return View(model);
        }
    }
}