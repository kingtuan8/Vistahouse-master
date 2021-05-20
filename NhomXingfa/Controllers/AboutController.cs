using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhomXingfa.Controllers
{
    public class AboutController : Controller
    {
        XingFaEntities db = new XingFaEntities();
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            Blog about = db.Blogs.Where(b => b.BlogID == 3 && (b.TypeBlog == WebConstants.BlogAboutUs || b.TypeBlog == WebConstants.BlogAboutUs_more)).FirstOrDefault();
            return View(about);
        }
        public ActionResult ThongTinDinhDuong()
        {
            Blog ThongTinDinhDuong = db.Blogs.Where(b => b.BlogID == 18 &&(b.TypeBlog == WebConstants.BlogAboutUs || b.TypeBlog == WebConstants.BlogAboutUs_more)).FirstOrDefault();
            return View(ThongTinDinhDuong);
        }
        public ActionResult ChinhSach()
        {
            Blog ChinhSach = db.Blogs.Where(b => b.BlogID == 16 && (b.TypeBlog == WebConstants.BlogAboutUs || b.TypeBlog == WebConstants.BlogAboutUs_more)).FirstOrDefault();
            return View(ChinhSach);
        }
        public ActionResult Delivery()
        {
            
            return View();
        }
    }
}