using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models;
using NhomXingfa.Areas.Quantri.Models.DataModels;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class HomePageController : Controller
    {
        private XingFaEntities db = new XingFaEntities();
        // GET: Quantri/HomePage
        [Authorize]
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.TongSP = db.Products.Count();
            model.KHContact = db.CustomerContacts.Where(a => a.StatusReply == 1).Count();
            model.TongThanhVien = db.Users.Where(a => a.CustomerID != null).Count();
            model.TongDonHang = db.Carts.Where(a => a.StatusOrder == 1 || a.StatusOrder == 2 || a.StatusOrder == 3).Count();
            model.lstCart = db.Carts.Where(a => a.StatusOrder == 1 || a.StatusOrder == 2 || a.StatusOrder == 3).OrderByDescending(a=>a.Id).Take(8).ToList();
            model.lstCusContact = db.CustomerContacts.Where(a => a.StatusReply == 1).ToList();
            return View(model);
        }
        public ActionResult testing()
        {
            return View();
        }
    }
}