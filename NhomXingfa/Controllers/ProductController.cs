﻿using NhomXingfa.Areas.Quantri.Models.DataModels;
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
                model.products = db.Products.Where(q => q.IsProduct == false).ToList();
            }
            else
            {
                model.products = db.Products.Where(q => q.IsProduct == false && q.CategoryID == id).ToList();
            }


            model.lstQAs = db.Q_A.Where(a => a.IsActive == true).OrderBy(a => a.ThuTu).ToList();
            model.lstCustomerFeed = db.CustomerFeedbacks.Where(c => c.IsActive == true).OrderBy(c => c.ThuTu).ToList();
            return View(model);
        }

        public ActionResult Detail(int? id)
        {
            var model = new DetailPageViewModel();
            model.product = db.Products.Find(id);
            model.products = db.Products.Where(q => q.IsProduct == true && q.IsActive == true && q.ProductID != id).ToList();
            model.images = db.ProductImages.Where(q => q.ProductID == id).ToList();
            model.tintuc = db.Blogs.Where(q => q.Category.TypeCate == 3 && q.IsActive == true).OrderByDescending(o => o.Created).Take(4).ToList();
            return View(model);
        }

        public ActionResult SanPhamDon()
        {
            var model = db.Products.Where(q => q.IsProduct == true).Take(12).ToList();
            return View(model);
        }

        public ActionResult SanPham(int? id)
        {
            var model = new SanPhamDonDM();
            model.spdon = db.Products.Where(q => q.IsProduct == true && q.CategoryID == id).Take(12).ToList();
            model.Title = db.Categories.Find(id).CategoryName;
            return View(model);
        }
    }
}