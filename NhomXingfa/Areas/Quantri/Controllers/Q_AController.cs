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
    [AuthorizeCustom(Roles = "Admin")]
    public class Q_AController : Controller
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/Q_A
        public ActionResult Index()
        {
            return View(db.Q_A.ToList().OrderBy(a=>a.ThuTu));
        }

        // GET: Quantri/Q_A/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Q_A q_A = db.Q_A.Find(id);
            if (q_A == null)
            {
                return HttpNotFound();
            }
            return View(q_A);
        }

        // GET: Quantri/Q_A/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quantri/Q_A/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Question,Answer,IsActive,ThuTu,Created,LastModify")] Q_A q_A)
        {
            if (ModelState.IsValid)
            {
                q_A.Created = DateTime.Now;
                q_A.LastModify = DateTime.Now;
                db.Q_A.Add(q_A);
                db.SaveChanges();
                return RedirectToAction("Index", "Q_A");
            }

            return View(q_A);
        }

        // GET: Quantri/Q_A/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Q_A q_A = db.Q_A.Find(id);
            if (q_A == null)
            {
                return HttpNotFound();
            }
            return View(q_A);
        }

        // POST: Quantri/Q_A/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Question,Answer,IsActive,ThuTu,Created,LastModify")] Q_A q_A)
        {
            if (ModelState.IsValid)
            {
                db.Entry(q_A).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(q_A);
        }

        // GET: Quantri/Q_A/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Q_A q_A = db.Q_A.Find(id);
            if (q_A == null)
            {
                return HttpNotFound();
            }
            return View(q_A);
        }

        // POST: Quantri/Q_A/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Q_A q_A = db.Q_A.Find(id);
            db.Q_A.Remove(q_A);
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
