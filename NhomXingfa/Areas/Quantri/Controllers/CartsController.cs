using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Models;
using PagedList;
using NhomXingfa.Areas.Quantri.Utilities;
using System.Globalization;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    [AuthorizeCustom(Roles = "Admin")]
    public class CartsController : BaseController
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/Carts
        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.CustomerOrder);
            return View(carts.ToList());
        }

        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string CarCode, string CustName, string CustPhone, int? OrderStatus, 
                                         string AddresDelivery, string DateFrom, string DateTo)       
        {
            CustName = CustName ?? "";
            ViewBag.CustName = CustName;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Carts.ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.Carts.ToList();

            if (!string.IsNullOrEmpty(CarCode))
            {
                lstprod = lstprod.Where(s => s.CarCode.ToUpper().Contains(CarCode.ToUpper())).ToList();
            }
            ViewBag.CarCode = CarCode;

            if (!string.IsNullOrEmpty(CustName))
            {
                lstprod = lstprod.Where(s => s.CustomerOrder.CustName.ToUpper().Contains(CustName.ToUpper())).ToList();
            }
            ViewBag.CustName = CustName;

            if (!string.IsNullOrEmpty(CustPhone))
            {
                lstprod = lstprod.Where(s => s.CustPhone.ToUpper().Contains(CustPhone.ToUpper())).ToList();
            }
            ViewBag.CustPhone = CustPhone;

            if (!string.IsNullOrEmpty(OrderStatus.ToString()))
            {
                lstprod = lstprod.Where(s => s.StatusOrder == OrderStatus).ToList();
            }
            ViewBag.OrderStatus = OrderStatus;

            if (!string.IsNullOrEmpty(CustPhone))
            {
                lstprod = lstprod.Where(s => s.CustPhone.ToUpper().Contains(CustPhone.ToUpper())).ToList();
            }
            ViewBag.CustPhone = CustPhone;

            if (!string.IsNullOrEmpty(AddresDelivery))
            {
                lstprod = lstprod.Where(s => s.AddresDelivery.ToUpper().Contains(AddresDelivery.ToUpper())).ToList();
            }
            ViewBag.AddresDelivery = AddresDelivery;

            if (!string.IsNullOrEmpty(DateFrom))
            {
                DateTime batdau = DateTime.ParseExact(DateFrom, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None);

                lstprod = lstprod.Where(s => s.Created >= batdau).ToList();
            }
            ViewBag.DateFrom = DateFrom;


            //Date To
            if (!string.IsNullOrEmpty(DateTo))
            {
                DateTime ketthuc = DateTime.ParseExact(DateTo, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None);
                DateTime kt1 = ketthuc.AddDays(1);
                lstprod = lstprod.Where(s => s.Created <= kt1).ToList();
            }
            ViewBag.DateTo = DateTo;

            lstprod = lstprod.OrderByDescending(s => s.Created).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstprod.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Quantri/Views/Carts/_PartialIndex.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }

        // GET: Quantri/Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartViewModel model = new CartViewModel();
            model.cart = db.Carts.Find(id);
            model.lstDetail = db.CartDetails.Where(a => a.CarID == id).ToList();
            model.lstHistory = db.CartHistories.Where(a => a.CartID == id).OrderByDescending(a=>a.Id).ToList();
            if(model.cart.CustID != null)
            {
                model.cust = db.CustomerOrders.Where(a => a.Id == model.cart.CustID).FirstOrDefault();
            }
            else
            {
                model.cust = null;
            }
            if (model.cart == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Quantri/Carts/Create
        public ActionResult Create()
        {
            ViewBag.CustID = new SelectList(db.CustomerOrders, "Id", "CustName");
            return View();
        }

        // POST: Quantri/Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarCode,CustID,AddresDelivery,CustPhone,TimeDelivery,ShipDelivery,Qty,Total,VouchersCode,VourcerMoney,TotalConLai,Notes,StatusOrder,StyleShip,Created,Updated")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustID = new SelectList(db.CustomerOrders, "Id", "CustName", cart.CustID);
            return View(cart);
        }

        // GET: Quantri/Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustID = new SelectList(db.CustomerOrders, "Id", "CustName", cart.CustID);
            return View(cart);
        }

        // POST: Quantri/Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarCode,CustID,AddresDelivery,CustPhone,TimeDelivery,ShipDelivery,Qty,Total,VouchersCode,VourcerMoney,TotalConLai,Notes,StatusOrder,StyleShip,Created,Updated")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustID = new SelectList(db.CustomerOrders, "Id", "CustName", cart.CustID);
            return View(cart);
        }

        // GET: Quantri/Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Quantri/Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var h = db.CartHistories.Where(a => a.CartID == id).ToList();
            foreach(var i in h)
            {
                CartHistory h1 = db.CartHistories.Find(i.Id);
                db.CartHistories.Remove(h1);
                db.SaveChanges();
            }

            var de = db.CartDetails.Where(a => a.CarID == id).ToList();
            foreach (var j in de)
            {
                CartDetail de1 = db.CartDetails.Find(j.Id);
                db.CartDetails.Remove(de1);
                db.SaveChanges();
            }
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            Success(string.Format("Xóa đơn hàng thành công."), true);
            return RedirectToAction("Index");
        }

        public ActionResult AddShipFee(FormCollection f)
        {
            int carid = Convert.ToInt32(f["CartID"]);
            var cart = db.Carts.Find(carid);
            cart.ShipDelivery = Convert.ToDecimal(f["ShipDelivery"]);
            db.Entry(cart).State = EntityState.Modified;
            db.SaveChanges();

            CartHistory h = new CartHistory();
            h.CartID = carid;
            h.TimeHistory = DateTime.Now;
            h.ContentHistory = "Cập nhật phí giao hàng";
            h.UserLogin = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
            db.CartHistories.Add(h);
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
