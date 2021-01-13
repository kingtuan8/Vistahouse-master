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
using PagedList;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class CustomerContactsController : Controller
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/CustomerContacts
        public ActionResult Index()
        {
            ViewData["ListCate"] = db.CustomerContactTypes.ToList();
            return View();
        }

        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string TenKH, int? TypeContact)
        {
            TenKH = TenKH ?? "";
            ViewBag.TenKH = TenKH;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Blogs.Where(b => b.TypeBlog == WebConstants.BlogNews).ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.CustomerContacts.ToList();



            if (!string.IsNullOrEmpty(TenKH))
            {
                lstprod = lstprod.Where(s => s.CustomerName.ToUpper().Contains(TenKH.ToUpper())).ToList();
            }
            ViewBag.TenKH = TenKH;

            if (!string.IsNullOrEmpty(TypeContact.ToString()))
            {
                lstprod = lstprod.Where(s => s.ContactTypeId == TypeContact).ToList();
            }
            ViewBag.TypeContact = TypeContact;



            lstprod = lstprod.OrderByDescending(a => a.Created).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstprod.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Quantri/Views/CustomerContacts/_PartialIndex.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }

        // GET: Quantri/CustomerContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return HttpNotFound();
            }
            return View(customerContact);
        }

        // GET: Quantri/CustomerContacts/Create
        public ActionResult Create()
        {
            ViewBag.ContactTypeId = new SelectList(db.CustomerContactTypes, "Id", "TypeName");
            return View();
        }

        // POST: Quantri/CustomerContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerContactID,CustomerName,CustomerEmail,CustomerPhone,CustomerAddress,CustomerContent,ContactTypeId,Created")] CustomerContact customerContact)
        {
            if (ModelState.IsValid)
            {
                db.CustomerContacts.Add(customerContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactTypeId = new SelectList(db.CustomerContactTypes, "Id", "TypeName", customerContact.ContactTypeId);
            return View(customerContact);
        }

        // GET: Quantri/CustomerContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactTypeId = new SelectList(db.CustomerContactTypes, "Id", "TypeName", customerContact.ContactTypeId);
            return View(customerContact);
        }

        // POST: Quantri/CustomerContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerContactID,CustomerName,CustomerEmail,CustomerPhone,CustomerAddress,CustomerContent,ContactTypeId,StatusReply,DateReply,AdminNote,Created")] CustomerContact customerContact)
        {
            if (ModelState.IsValid)
            {
                customerContact.DateReply = DateTime.Now;
                db.Entry(customerContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactTypeId = new SelectList(db.CustomerContactTypes, "Id", "TypeName", customerContact.ContactTypeId);
            return View(customerContact);
        }

        // GET: Quantri/CustomerContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return HttpNotFound();
            }
            return View(customerContact);
        }

        // POST: Quantri/CustomerContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            db.CustomerContacts.Remove(customerContact);
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
