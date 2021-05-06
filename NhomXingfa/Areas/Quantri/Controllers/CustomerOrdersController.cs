using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class CustomerOrdersController : Controller
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/CustomerOrders
        public ActionResult Index()
        {
            return View(db.CustomerOrders.ToList());
        }

        // GET: Quantri/CustomerOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return HttpNotFound();
            }
            return View(customerOrder);
        }

        // GET: Quantri/CustomerOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quantri/CustomerOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustName,CustPhone,CustEmail,CustAddress")] CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                db.CustomerOrders.Add(customerOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerOrder);
        }

        // GET: Quantri/CustomerOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return HttpNotFound();
            }
            return View(customerOrder);
        }

        // POST: Quantri/CustomerOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustName,CustPhone,CustEmail,CustAddress")] CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerOrder);
        }

        // GET: Quantri/CustomerOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return HttpNotFound();
            }
            return View(customerOrder);
        }

        // POST: Quantri/CustomerOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            var lstus = db.Users.Where(a => a.CustomerID == id && a.CustomerID != null).ToList();
            if (lstus == null)
            {
                db.CustomerOrders.Remove(customerOrder);
                db.SaveChanges();
            }
            else
            {
                foreach (var i in lstus)
                {
                    User u = db.Users.Find(i.UserID);
                    db.Users.Remove(u);
                }
                db.CustomerOrders.Remove(customerOrder);
                db.SaveChanges();
            }
            Danger(string.Format(" Đã xóa thành công.", ""), true);
            return RedirectToAction("Index", "CustomerOrders");
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
