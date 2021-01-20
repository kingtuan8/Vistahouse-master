using Newtonsoft.Json;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;
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
            model.lstBannerHomePage = db.Slides.Where(q => q.CategoryID == 0).OrderBy(q => q.Sort).ToList();
            model.lstLuaChon = db.Blogs.Where(b=>b.IsActive == true &&(b.BlogID == 6 || b.BlogID == 5 || b.BlogID == 4)).OrderBy(b => b.Sort).ToList();
            model.OurStory = db.Blogs.Where(b => b.IsActive == true && b.BlogID == 3).FirstOrDefault();
            model.lstCustomerFeedback = db.CustomerFeedbacks.Where(c => c.IsActive == true).OrderBy(c => c.ThuTu).ToList();
            //model.carts = GetCart();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Cart()
        {
            var model = GetCart();
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult AddCartData()
        {
            //addCart(productid, catespdon, quantity);

            var model = GetCart();
            return PartialView("_minicart", model);
        }

        public JsonResult AddCartJson(int? productid, int? catespdon, int? quantity)
        {
            decimal x = addCart(productid, catespdon, quantity);
            
            return Json("ok_" + String.Format("{0:#,0}", x), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveCart(int? productid, int? quantity)
        {
            decimal x = removeCart(productid, quantity);  
            
            if(x == 0)
            {
                Session["CartShop"] = null;
            }

            return Json("ok_" + String.Format("{0:#,0}", x), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeML(int? productid, int? catespdon)
        {
            List<CartViewModel> cart = GetCart();

            var prod = db.Products.Find(productid);


            var p = cart.FirstOrDefault(q => q.ID == productid);
            p.CateSPDon = catespdon;
            if(catespdon == 1)
            {
                p.Price = prod.PriceSale;
            }
            else
            {
                p.Price = prod.PriceSale1;
            }

            decimal total = 0;
            foreach (var q in cart)
            {
                total = total + (q.Price.Value * q.Quantity.Value);
            }

            Session["CartShop"] = cart;

            return Json("ok_" + String.Format("{0:#,0}", total) + "_" + String.Format("{0:#,0}", p.Price), JsonRequestBehavior.AllowGet);
        }
       

        private List<CartViewModel> GetCart()
        {
            List<CartViewModel> lstItemSession = new List<CartViewModel>();

            if(Session["CartShop"] != null)
            {
                lstItemSession = (List<CartViewModel>)Session["CartShop"];
            }


            return lstItemSession;
        }

        private decimal addCart(int? productid, int? catespdon, int? quantity)
        {
            List<CartViewModel> cart = GetCart();

            var x = cart.FirstOrDefault(q => q.ID == productid);
            if(x != null)
            {
                if(quantity <= 0)
                {
                    x.Quantity = x.Quantity + 1;
                }
                else
                {
                    x.Quantity = quantity;
                }
                
                x.CateSPDon = catespdon;
            }
            else
            {
                var prod = db.Products.Find(productid);
                CartViewModel child = new CartViewModel();
                child.CateSPDon = catespdon;
                child.Descript = prod.ShortDescription;
                child.ID = productid;
                child.Image = prod.Images;
                child.IsProduct = prod.IsProduct;
                child.Name = prod.ProductName;
                child.Price = prod.PriceSale;
                if(quantity <= 0)
                {
                    child.Quantity = 1;
                }
                else
                {
                    child.Quantity = quantity;
                }
                
                cart.Add(child);
            }

            decimal total = 0;
            foreach (var q in cart)
            {
                total = total + (q.Price.Value * q.Quantity.Value);
            }

            Session["CartShop"] = cart;

            return total;      
        }

        private decimal removeCart(int? productid, int? quantity)
        {
            List<CartViewModel> cart = GetCart();

            var x = cart.FirstOrDefault(q => q.ID == productid);

            if(quantity <= 0)
            {
                cart.Remove(x);
            }
            else
            {
                x.Quantity = quantity;
            }

            decimal total = 0;
            foreach (var q in cart)
            {
                total = total + (q.Price.Value * q.Quantity.Value);
            }

            Session["CartShop"] = cart;

            return total;


        }


    }
}