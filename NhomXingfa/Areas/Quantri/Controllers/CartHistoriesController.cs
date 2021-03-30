using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models.DataModels;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class CartHistoriesController : Controller
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/CartHistories
        public ActionResult Index()
        {
            var cartHistories = db.CartHistories.Include(c => c.Cart).Include(c => c.User);
            return View(cartHistories.ToList());
        }

        // GET: Quantri/CartHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartHistory cartHistory = db.CartHistories.Find(id);
            if (cartHistory == null)
            {
                return HttpNotFound();
            }
            return View(cartHistory);
        }

        // GET: Quantri/CartHistories/Create
        public ActionResult Create()
        {
            ViewBag.CartID = new SelectList(db.Carts, "Id", "CarCode");
            ViewBag.UserLogin = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Quantri/CartHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CartID,TimeHistory,ContentHistory,UserLogin")] CartHistory cartHistory)
        {
            if (ModelState.IsValid)
            {
                cartHistory.TimeHistory = DateTime.Now;
                cartHistory.UserLogin = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                db.CartHistories.Add(cartHistory);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }

            ViewBag.CartID = new SelectList(db.Carts, "Id", "CarCode", cartHistory.CartID);
            ViewBag.UserLogin = new SelectList(db.Users, "UserID", "UserName", cartHistory.UserLogin);
            return View(cartHistory);
        }

        public ActionResult AddHistory(FormCollection f)
        {
            int carid = Convert.ToInt32(f["CartID"]);
            var cart = db.Carts.Find(carid);
            cart.StatusOrder = Convert.ToInt32(f["drCartStatus"]);
            db.Entry(cart).State = EntityState.Modified;
            db.SaveChanges();

            CartHistory h = new CartHistory();
            h.CartID = carid;
            h.TimeHistory = DateTime.Now;
            h.ContentHistory = f["ContentHistory"].ToString();
            h.UserLogin = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
            db.CartHistories.Add(h);
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        // GET: Quantri/CartHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartHistory cartHistory = db.CartHistories.Find(id);
            if (cartHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartID = new SelectList(db.Carts, "Id", "CarCode", cartHistory.CartID);
            ViewBag.UserLogin = new SelectList(db.Users, "UserID", "UserName", cartHistory.UserLogin);
            return View(cartHistory);
        }

        // POST: Quantri/CartHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CartID,TimeHistory,ContentHistory,UserLogin")] CartHistory cartHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartID = new SelectList(db.Carts, "Id", "CarCode", cartHistory.CartID);
            ViewBag.UserLogin = new SelectList(db.Users, "UserID", "UserName", cartHistory.UserLogin);
            return View(cartHistory);
        }

        // GET: Quantri/CartHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartHistory cartHistory = db.CartHistories.Find(id);
            if (cartHistory == null)
            {
                return HttpNotFound();
            }
            return View(cartHistory);
        }

        // POST: Quantri/CartHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartHistory cartHistory = db.CartHistories.Find(id);
            db.CartHistories.Remove(cartHistory);
            db.SaveChanges();
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
