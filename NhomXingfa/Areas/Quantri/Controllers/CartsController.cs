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

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class CartsController : BaseController
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/Carts
        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.CustomerOrder);
            return View(carts.ToList());
        }

        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string MaSP, string SanPham, int? DanhMucCha, int? DanhMucCon,
                                          string SEOKeywords)
        {
            SanPham = SanPham ?? "";
            ViewBag.SanPham = SanPham;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Carts.ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.Carts.ToList();

            if (!string.IsNullOrEmpty(MaSP))
            {
                lstprod = lstprod.Where(s => s.CarCode.ToUpper().Contains(MaSP.ToUpper())).ToList();
            }
            ViewBag.MaSP = MaSP;

            if (!string.IsNullOrEmpty(SanPham))
            {
                lstprod = lstprod.Where(s => s.CustPhone.ToUpper().Contains(SanPham.ToUpper())).ToList();
            }
            ViewBag.SanPham = SanPham;

            //if (!string.IsNullOrEmpty(DanhMucCha.ToString()))
            //{
            //    lstprod = lstprod.Where(s => s.CategoryIDParent == DanhMucCha).ToList();
            //}
            //ViewBag.DanhMucCha = DanhMucCha;

            //if (!string.IsNullOrEmpty(DanhMucCon.ToString()))
            //{
            //    lstprod = lstprod.Where(s => s.CategoryID == DanhMucCon).ToList();
            //}
            //ViewBag.DanhMucCon = DanhMucCon;

            if (!string.IsNullOrEmpty(SEOKeywords))
            {
                lstprod = lstprod.Where(s => s.CustomerOrder.CustName.ToUpper().Contains(SEOKeywords.ToUpper())).ToList();
            }
            ViewBag.SEOKeywords = SEOKeywords;

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
