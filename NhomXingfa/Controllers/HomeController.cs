using Newtonsoft.Json;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;
using NhomXingfa.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NhomXingfa.Controllers
{
    public class HomeController : Controller
    {
        XingFaEntities db = new XingFaEntities();
        //Helpers h = new Helpers();
        public ActionResult Index()
        {
            var model = new IndexPageViewModel();
            model.goidinhky = db.Products.Where(q => q.IsActive == true && q.IsProduct == false).ToList();
            model.products = db.Products.Where(q => q.IsActive == true && q.IsProduct == true).ToList();
            model.lstBannerHomePage = db.Slides.Where(q => q.CategoryID == 0).OrderBy(q => q.Sort).ToList();
            model.lstLuaChon = db.Blogs.Where(b=>b.IsActive == true &&(b.BlogID == 6 || b.BlogID == 5 || b.BlogID == 4)).OrderBy(b => b.Sort).ToList();
            model.OurStory = db.Blogs.Where(b => b.IsActive == true && b.BlogID == 3).FirstOrDefault();
            model.lstCustomerFeedback = db.CustomerFeedbacks.Where(c => c.IsActive == true).OrderBy(c => c.ThuTu).ToList();
            model.lstCustomerParner = db.Customers.Where(a => a.IsActive == true).ToList();
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult OrderDetail()
        {
            var model = new List<Cart>();
            if(Request.IsAuthenticated)
            {
                string un = User.Identity.Name;
                var uname = db.Users.FirstOrDefault(q => q.UserName == un);
                if(uname != null)
                { 
                    if(uname.CustomerID != null)
                    {
                        model = db.Carts.Where(q => q.CustID == uname.CustomerID).OrderByDescending(o => o.Created).ToList();
                    }
                }
            }
            return View(model);
        }

        public ActionResult Cart()
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            //HttpCookie cookie = new HttpCookie("Lang");
            //cookie.Value = "en";
            //Response.Cookies.Add(cookie);

            var model = new CartDetailViewModel();
            model.cart = GetCart();
            if(User.Identity.Name != "")
            {
                var userid = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name);
                model.customer = db.CustomerOrders.Find(userid.CustomerID);
            }
            //model.customer = db.CustomerOrders.Find
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult CheckUserName(string uname)
        {
            string x = "0";

            var u = db.Users.FirstOrDefault(q => q.UserName == uname);

            if(u != null)
            {
                x = "1";
            }

            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertUser(string uname, string pword, string email, string mobile, string fname, string dchi)
        {
            bool flag = false;

            CustomerOrder o = new CustomerOrder();
            o.CustName = fname;
            o.CustPhone = mobile;
            o.CustEmail = email;
            o.CustAddress = dchi;
            db.CustomerOrders.Add(o);

            db.SaveChanges();

            User u = new User();
            u.UserName = uname;
            u.HashPass = GetMD5Hash(pword);
            u.Active = true;
            u.Created = DateTime.Now;
            u.CustomerID = o.Id;

            db.Users.Add(u);

            db.SaveChanges();

            flag = true;


            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertDonHang(string logged, string fname, string sdt, string ngaygiao, string dchi, string ghichu, int?  styleship)
        {
            string check = "null";

            if(logged == "0")
            {
                CustomerOrder o = new CustomerOrder();
                o.CustName = fname;
                o.CustPhone = sdt;
                o.CustEmail = "";
                o.CustAddress = dchi;
                db.CustomerOrders.Add(o);

                db.SaveChanges();

                var cart = GetCart();

                Cart c = new Cart();
                c.CarCode = "";
                c.CustID = o.Id;
                c.AddresDelivery = dchi;
                c.CustPhone = sdt;
                DateTimeFormatInfo usDtfi = new CultureInfo("vi-VN", false).DateTimeFormat;
                c.TimeDelivery = Convert.ToDateTime(ngaygiao, usDtfi);
                c.ShipDelivery = 0;
                c.Qty = cart.Sum(s => s.Quantity);
                c.Total = 0;
                c.TotalConLai = 0;
                c.Notes = ghichu;
                c.StatusOrder = 0;
                c.StyleShip = styleship;
                c.Created = DateTime.Now;
                c.Updated = DateTime.Now;

                db.Carts.Add(c);

                db.SaveChanges();

                decimal? total = 0;
                decimal? phiship = 0;
                

                foreach(var q in cart)
                {

                    var p = db.Products.Find(q.ID);
                    phiship = phiship + p.PhiShip;

                    total = total + (q.Quantity * q.Price);

                    CartDetail cd = new CartDetail();
                    cd.CarID = c.Id;
                    cd.ProductID = q.ID;
                    cd.Price = q.Price;
                    cd.Qty = q.Quantity;
                    cd.Total = q.Quantity * q.Price;
                    cd.IsGoiSanPham = q.IsProduct;
                    if(q.IsProduct == true)
                    {
                        if (q.CateSPDon == 1)
                        {
                            cd.ml = "350ml";
                        }
                        else
                        {
                            cd.ml = "550ml";
                        }
                    }

                    db.CartDetails.Add(cd);
                }

                c.Total = total;
                c.TotalConLai = total;
                c.ShipDelivery = phiship;

                db.SaveChanges();

                check = c.Id + "";

            }
            else
            {
                var unamex = User.Identity.Name;
                int? custid = 0;
                var u = db.Users.FirstOrDefault(q => q.UserName == unamex);
                if(u != null)
                {
                    if(u.CustomerID != null)
                    {
                        custid = u.CustomerID;
                        var co = db.CustomerOrders.Find(u.CustomerID);
                        co.CustName = fname;
                        co.CustPhone = sdt;
                        co.CustAddress = dchi;

                        db.SaveChanges();
                    }
                    else
                    {
                        CustomerOrder o = new CustomerOrder();
                        o.CustName = fname;
                        o.CustPhone = sdt;
                        o.CustEmail = "";
                        o.CustAddress = dchi;
                        db.CustomerOrders.Add(o);

                        db.SaveChanges();

                        custid = o.Id;
                    }

                    var cart = GetCart();

                    Cart c = new Cart();
                    c.CarCode = "";
                    c.CustID = custid;
                    c.AddresDelivery = dchi;
                    c.CustPhone = sdt;
                    DateTimeFormatInfo usDtfi = new CultureInfo("vi-VN", false).DateTimeFormat;
                    c.TimeDelivery = Convert.ToDateTime(ngaygiao, usDtfi);
                    c.ShipDelivery = 0;
                    c.Qty = cart.Sum(s => s.Quantity);
                    c.Total = 0;
                    c.TotalConLai = 0;
                    c.Notes = ghichu;
                    c.StatusOrder = 0;
                    c.StyleShip = styleship;
                    c.Created = DateTime.Now;
                    c.Updated = DateTime.Now;

                    db.Carts.Add(c);

                    db.SaveChanges();

                    decimal? total = 0;
                    decimal? phiship = 0;

                    foreach (var q in cart)
                    {

                        var p = db.Products.Find(q.ID);
                        phiship = phiship + p.PhiShip;

                        total = total + (q.Quantity * q.Price);

                        CartDetail cd = new CartDetail();
                        cd.CarID = c.Id;
                        cd.ProductID = q.ID;
                        cd.Price = q.Price;
                        cd.Qty = q.Quantity;
                        cd.Total = q.Quantity * q.Price;
                        cd.IsGoiSanPham = q.IsProduct;
                        if (q.IsProduct == true)
                        {
                            if (q.CateSPDon == 1)
                            {
                                cd.ml = "350ml";
                            }
                            else
                            {
                                cd.ml = "550ml";
                            }
                        }

                        db.CartDetails.Add(cd);
                    }

                    c.Total = total;
                    c.TotalConLai = total;
                    c.ShipDelivery = phiship;

                    db.SaveChanges();

                    check = c.Id + "";


                }  
            }

            //check = true;

            Session["CartShop"] = null;

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompleteCart(int? cartid)
        {
            bool check = false;

            var cart = db.Carts.Find(cartid);
            cart.StatusOrder = 1;

            db.SaveChanges();

            check = true;

            Session["CartShop"] = null;

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        private string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
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

        /// <summary>
        /// Danny Code
        /// </summary>
        /// Lưu comment Tư Vấn từ khách hàng
        /// <returns></returns>
        /// 

        #region Lưu Comment tư vấn từ khách hàng

        
        public JsonResult SavingTuVan(string txtName, string txtPhone, string txtComment, int ContactType)
        {
            CustomerContact cust = new CustomerContact();
            cust.CustomerName = txtName.Trim();
            cust.CustomerEmail = null;
            cust.CustomerPhone = txtPhone;
            cust.CustomerAddress = null;
            cust.CustomerContent = txtComment;
            cust.ContactTypeId = ContactType;
            cust.StatusReply = 1;
            cust.DateReply = null;
            cust.AdminNote = null;
            cust.Created = DateTime.Now;
            db.CustomerContacts.Add(cust);
            db.SaveChanges();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult loadFooter()
        {
            //var model = db.MENUs.Where(q => q.IdCha == 0).OrderBy(o => o.ThuTu);

            FooterViewModel model = new FooterViewModel();
            model.footerInfo = db.Blogs.Where(b => b.BlogID == 20 && (b.TypeBlog == WebConstants.BlogAboutUs || b.TypeBlog == WebConstants.BlogAboutUs_more)).FirstOrDefault();
            return PartialView("_footer", model);
        }
        #endregion
    }
}